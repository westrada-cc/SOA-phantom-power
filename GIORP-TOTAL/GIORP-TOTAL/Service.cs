using GIORP_TOTAL.Helpers;
using HL7Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace GIORP_TOTAL
{
    public static class Service
    {
        const string CommandDirectiveElement = "DRC";
        const string SOADirectiveElement = "SOA";
        const string ArgumentDirectiveElement = "ARG";
        const string ResponseDirectiveElement = "RSP";
        const string MCHDirectiveElement = "MCH";
        const string InfoDirectiveElement = "INF";
        const string ServiceDirectiveElement = "SRV";
        const string PublishedServiceDirectiveElement = "PUB";

        const string RegisterTeamElement = "REG-TEAM";
        const string PublishServiceElement = "PUB-SERVICE";
        const string QueryTeamElement = "QUERY-TEAM";
        const string SOAOkElement = "OK";
        const string SOANotOkElement = "NOT-OK";
        const string ExecuteServiceElement = "EXEC-SERVICE";

        static Properties.Settings _settings = Properties.Settings.Default;

        static string _teamId = null;
        static ITaxCalculator _taxCalculator = new TaxCalculator();

        public static void Start()
        {
            Console.WriteLine(">>>>>>>>>>>> GIORP-TOTAL <<<<<<<<<<<<<");
            Console.WriteLine("");
            Console.WriteLine("| Current Service Settings");
            Console.WriteLine("| Service Publish IP: " + _settings.ServicePublishIp);
            Console.WriteLine("| Service Publish Port: " + _settings.ServicePublishPort);
            Console.WriteLine("| SOA Registry IP: " + _settings.SOARegistryIp);
            Console.WriteLine("| SOA Registry Port: " + _settings.SOARegistryPort);
            Console.WriteLine("| Team Name: " + _settings.TeamName);
            Console.WriteLine("You can change the above settings in the App.config file.");
            Console.WriteLine("");
            Console.WriteLine("");

            Console.Write("Do you want to register with SOA Register? Y/N ");
            if (ConsoleHelper.ReadConfirm())
            {
                bool shouldTryAgain = false;
                do
                {
                    if (RegisterServiceWithSoaRegistry())
                    {
                        Console.WriteLine("Registration successful.");
                    }
                    else
                    {
                        Console.Write("Registration failed. Try Again? Y/N ");
                        shouldTryAgain = ConsoleHelper.ReadConfirm();
                    }
                } while (shouldTryAgain);
            }

            // SERVER SETUP //

            SocketListener.RequestReceived += Server_RequestReceived;
            SocketListener.StartListeningAsync(Properties.Settings.Default.ServicePublishIp, Properties.Settings.Default.ServicePublishPort, HL7Utility.EndOfMessage.ToString());
            Console.WriteLine("Service started. Press 0 to exit.");

            while (Console.ReadKey().Key != ConsoleKey.D0)
            {
                Thread.Sleep(300); // Sleep for 0.3 seconds, and then check again if user wants to exit.
            }
        }

        static bool RegisterServiceWithSoaRegistry()
        {
            try
            {
                Console.WriteLine(string.Format("Registering service to SOA Registry with team name {0}...", _settings.TeamName));
                // Register team
                string teamRegistractionResponse = null;
                var registerTeamMessage = CreateRegisterTeamRequest(_settings.TeamName);
                try
                {
                    logServerMessage("Calling SOA-Registry with message :", registerTeamMessage);
                    teamRegistractionResponse = SocketClient.SendRequest(registerTeamMessage, _settings.SOARegistryIp, _settings.SOARegistryPort);
                }
                catch (Exception ex)
                {
                    Logger.logException(ex);

                    throw new Exception("Could not register the team.", ex);
                }
                if (teamRegistractionResponse != null)
                {
                    logServerMessage("Response from SOA-Registry :", teamRegistractionResponse);

                    var teamRegistractionResponseMessage = HL7Utility.Deserialize(teamRegistractionResponse);
                    if (teamRegistractionResponseMessage.Segments.Count > 0 &&
                        teamRegistractionResponseMessage.Segments[0].Elements.Count > 2 &&
                        teamRegistractionResponseMessage.Segments[0].Elements[0] == SOADirectiveElement &&
                        teamRegistractionResponseMessage.Segments[0].Elements[1] == "OK")
                    {
                        _teamId = teamRegistractionResponseMessage.Segments[0].Elements[2];
                        Console.WriteLine("Team Registered with teamID " + _teamId);
                    }
                    else
                    {
                        throw new Exception("Team registration response is not in valid format.");
                    }

                    // Register service
                    var registerServiceMessage = CreateRegisterServiceRequest();
                    string registerServiceResponse = null;
                    try
                    {
                        logServerMessage("Calling SOA-Registry with message :", registerServiceMessage);

                        registerServiceResponse = SocketClient.SendRequest(registerServiceMessage, _settings.SOARegistryIp, _settings.SOARegistryPort);
                        logServerMessage("Response from SOA-Registry :", registerServiceResponse);
                        Console.WriteLine("Response from SOA-Registry :" + registerServiceResponse);

                        var registerServiceResponseMessage = HL7Utility.Deserialize(registerServiceResponse);
                        if (registerServiceResponseMessage.Segments.Count > 0 &&
                            registerServiceResponseMessage.Segments[0].Elements.Count > 2 &&
                            registerServiceResponseMessage.Segments[0].Elements[0] == SOADirectiveElement &&
                            registerServiceResponseMessage.Segments[0].Elements[1] == "OK")
                        {
                            return true;
                        }
                        else
                        {
                            throw new Exception("Registration failed.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.logException(ex);
                        throw new Exception("Could not register the service.", ex);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e + "InnerException: " + e.InnerException);

                Logger.logException(e);
            }


            return false;
        }

        public static string CreateRegisterTeamRequest(string teamName)
        {
            var message = new Message();
            message.AddSegment(CommandDirectiveElement, RegisterTeamElement, "", "");
            message.AddSegment(InfoDirectiveElement, teamName,"","");
            return HL7Utility.Serialize(message);
        }

        public static string CreateRegisterServiceRequest()
        {
            var message = new Message();
            // DRC|PUB-SERVICE|<team name>|<teamID>|
            message.AddSegment(CommandDirectiveElement, PublishServiceElement, _settings.TeamName, _teamId);
            // SRV|<tag name>|<service name>|<security level>|<num args>|<num responses>|<description>|
            message.AddSegment(ServiceDirectiveElement, _settings.ServiceTagName, _settings.ServiceName, _settings.ServiceSecurityLevel, "2", "5", _settings.ServiceDescription);
            // ARG|<arg position>|<arg name>|<arg data type>|[mandatory | optional]||
            message.AddSegment(ArgumentDirectiveElement, "1", "province", "string", "mandatory","");
            // ARG|<arg position>|<arg name>|<arg data type>|[mandatory | optional]||
            message.AddSegment(ArgumentDirectiveElement, "2", "amount", "double", "mandatory","");
            // RSP|<resp position>|<resp name>|<resp data type>||
            message.AddSegment(ResponseDirectiveElement, "1", "NetAmount", "double", "");
            // RSP|<resp position>|<resp name>|<resp data type>||
            message.AddSegment(ResponseDirectiveElement, "2", "PstAmount", "double", "");
            // RSP|<resp position>|<resp name>|<resp data type>||
            message.AddSegment(ResponseDirectiveElement, "3", "HstAmount", "double", "");
            // RSP|<resp position>|<resp name>|<resp data type>||
            message.AddSegment(ResponseDirectiveElement, "4", "GstAmount", "double", "");
            // RSP|<resp position>|<resp name>|<resp data type>||
            message.AddSegment(ResponseDirectiveElement, "5", "TotalAmount", "double", "");
            // MCH|<published server IP>|<published port>| 
            message.AddSegment(MCHDirectiveElement, _settings.ServicePublishIp, _settings.ServicePublishPort.ToString());
            return HL7Utility.Serialize(message);
        }

        static void Server_RequestReceived(object sender, RequestReceivedEventArgs e)
        {
            Console.WriteLine("Service received a request:");
            Console.WriteLine(e.Request);

            logServerMessage("Recieving service request :", e.Request);

            var response = HandleRequest(e.Request);
            Console.WriteLine("Service responding with:");
            Console.WriteLine(response);
            e.Response = response;

            logServerMessage("Responding to service request  :", e.Response);
        }

        static string HandleRequest(string request)
        {
            Message requestMessage = null;
            try
            {
                requestMessage = HL7Utility.Deserialize(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);

                Logger.logException(ex);
            }
            if (requestMessage != null)
            {
                // Check if incoming request is in valid format.
                if (requestMessage.Segments != null &&
                    requestMessage.Segments.Count == 4 && // DRC, SRV, ARG, ARG = 4
                    requestMessage.Segments[0].Elements != null && // DRC|EXEC-SERVICE|<team name>|<teamID>|
                    requestMessage.Segments[0].Elements.Count == 4 && 
                    requestMessage.Segments[0].Elements[0] == CommandDirectiveElement &&
                    requestMessage.Segments[0].Elements[1] == ExecuteServiceElement &&
                    requestMessage.Segments[1].Elements != null && // SRV||<service name>||<num args>|||
                    requestMessage.Segments[1].Elements.Count == 7 && 
                    requestMessage.Segments[1].Elements[0] == ServiceDirectiveElement &&
                    requestMessage.Segments[1].Elements[1] == "" &&
                    requestMessage.Segments[1].Elements[3] == "" &&
                    requestMessage.Segments[1].Elements[5] == "" &&
                    requestMessage.Segments[1].Elements[6] == "" &&
                    requestMessage.Segments[2].Elements != null && // ARG|<arg position>|<arg name>|<arg data type>||<arg value>|
                    requestMessage.Segments[2].Elements.Count == 6 &&
                    requestMessage.Segments[2].Elements[0] == ArgumentDirectiveElement &&
                    requestMessage.Segments[2].Elements[2] ==  "province" &&
                    requestMessage.Segments[2].Elements[3] == "string" &&
                    requestMessage.Segments[3].Elements != null && // ARG|<arg position>|<arg name>|<arg data type>||<arg value>|
                    requestMessage.Segments[3].Elements.Count == 6 &&
                    requestMessage.Segments[3].Elements[0] == ArgumentDirectiveElement &&
                    requestMessage.Segments[3].Elements[2] ==  "amount" &&
                    requestMessage.Segments[3].Elements[3] == "double")
                {
                    // Now we parse out the team requesting to execute.
                    var teamName = requestMessage.Segments[0].Elements[2];
                    var teamId = requestMessage.Segments[0].Elements[3];
                    
                    if (ValidateTeam(teamName, teamId))
                    {
                        // Now we parse out the arguments province and amount
                        string provinceArg = requestMessage.Segments[2].Elements[5];
                        double amountArg = 0;
                        if (double.TryParse(requestMessage.Segments[3].Elements[5], out amountArg))
                        {
                            Models.TaxSummary taxSummary = null;
                            try
                            {
                                taxSummary = _taxCalculator.CalculateTax(provinceArg, amountArg);
                            }
                            catch (ArgumentException ae)
                            {
                                Logger.logException(ae);

                                var errorResponseMessage = new Message();
                                errorResponseMessage.AddSegment(PublishedServiceDirectiveElement, SOANotOkElement, "Arguments not valid.", ae.Message);
                                return HL7Utility.Serialize(errorResponseMessage);
                            }
                            catch (Exception ex)
                            {
                                Logger.logException(ex);

                                var errorResponseMessage = new Message();
                                errorResponseMessage.AddSegment(PublishedServiceDirectiveElement, SOANotOkElement, "There was a problem with calculating the tax summary.", "");
                                return HL7Utility.Serialize(errorResponseMessage);
                            }
                            if (taxSummary != null)
                            {
                                // SUCCESS //
                                var responseMessage = new Message();
                                // PUB|OK|||<num segments>| 
                                responseMessage.AddSegment(PublishedServiceDirectiveElement, SOAOkElement, "", "", "5");
                                // RSP|<resp position>|<resp name>|<resp data type>|<resp value>|
                                responseMessage.AddSegment(ResponseDirectiveElement, "1", "NetAmount", "double", taxSummary.NetAmount.ToString());
                                // RSP|<resp position>|<resp name>|<resp data type>|<resp value>|
                                responseMessage.AddSegment(ResponseDirectiveElement, "2", "PstAmount", "double", taxSummary.PstAmount.ToString());
                                // RSP|<resp position>|<resp name>|<resp data type>|<resp value>|
                                responseMessage.AddSegment(ResponseDirectiveElement, "3", "HstAmount", "double", taxSummary.HstAmount.ToString());
                                // RSP|<resp position>|<resp name>|<resp data type>|<resp value>|
                                responseMessage.AddSegment(ResponseDirectiveElement, "4", "GstAmount", "double", taxSummary.GstAmount.ToString());
                                // RSP|<resp position>|<resp name>|<resp data type>|<resp value>|
                                responseMessage.AddSegment(ResponseDirectiveElement, "5", "TotalAmount", "double", taxSummary.TotalAmount.ToString());
                                return HL7Utility.Serialize(responseMessage);
                            }
                            else
                            {
                                var errorResponseMessage = new Message();
                                errorResponseMessage.AddSegment(PublishedServiceDirectiveElement, SOANotOkElement, "There was a problem with calculating the tax summary.", "");
                                return HL7Utility.Serialize(errorResponseMessage);
                            }
                        }
                        else
                        {
                            var errorResponseMessage = new Message();
                            errorResponseMessage.AddSegment(PublishedServiceDirectiveElement, SOANotOkElement, "Could not parse amount argument.", "Make sure that amount argument is of type double.");
                            return HL7Utility.Serialize(errorResponseMessage);
                        } 
                    }
                    else
                    {
                        var errorResponseMessage = new Message();
                        errorResponseMessage.AddSegment(PublishedServiceDirectiveElement, SOANotOkElement, "Assess denied.", string.Format("Team {0} with ID {1} is not allowed to execute the service.", teamName, teamId));
                        return HL7Utility.Serialize(errorResponseMessage);
                    }
                }
                else
                {
                    var errorResponseMessage = new Message();
                    errorResponseMessage.AddSegment(PublishedServiceDirectiveElement, SOANotOkElement, "Request not in valid format.", "Make sure that you comply to SOA Registry message format.");
                    return HL7Utility.Serialize(errorResponseMessage);
                }
            }
            else
            {
                var errorResponseMessage = new Message();
                errorResponseMessage.AddSegment(PublishedServiceDirectiveElement, SOANotOkElement, "There was a problem with calculating the tax summary.", "");
                return HL7Utility.Serialize(errorResponseMessage);
            }
                
        }

        private static bool ValidateTeam(string teamName, string teamId)
        {
            if (!_settings.ShouldValidateTeam)
            {
                Console.WriteLine("Team authorisation disabled. Change app.config file to enable.");
                return true;
            }
            var validateTeamRequest = CreateValidateTeamRequest(teamName, teamId);
            string validateTeamResponse = null;
            try
            {
                logServerMessage("Calling SOA-Registry with message :", validateTeamRequest);

                validateTeamResponse = SocketClient.SendRequest(validateTeamRequest, _settings.SOARegistryIp, _settings.SOARegistryPort);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Logger.logException(ex);
            }
            if (validateTeamResponse != null)
            {
                Message message = null;
                try
                {
                    logServerMessage("Response from SOA-Registry :", validateTeamResponse);

                    message = HL7Utility.Deserialize(validateTeamResponse);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);

                    Logger.logException(ex);
                }
                if (message != null && 
                    message.Segments != null &&
                    message.Segments.Count > 0 &&
                    message.Segments[0].Elements != null &&
                    message.Segments[0].Elements.Count >= 2 &&
                    message.Segments[0].Elements[0] == SOADirectiveElement &&
                    message.Segments[0].Elements[1] == SOAOkElement)
                {
                    // The team is authorised to use the service.
                    Console.WriteLine(string.Format("Team {0} with ID {1} is authorised to use the service.", teamName, teamId));
                    return true;
                }
            }

            Console.WriteLine(string.Format("Team {0} with ID {1} is NOT authorised to use the service.", teamName, teamId));
            return false;
        }

        public static string CreateValidateTeamRequest(string teamName, string teamId)
        {
            var message = new Message();
            // DRC|QUERY-TEAM|<team name>|<teamID>| 
            message.AddSegment(CommandDirectiveElement, QueryTeamElement, _settings.TeamName, _teamId);
            // INF|<team name>|<teamID>|<service tag name>| 
            message.AddSegment(InfoDirectiveElement, teamName, teamId, _settings.ServiceTagName);
            return HL7Utility.Serialize(message);
        }

        private static void logServerMessage(string messageHeader, string HL7Message)
        {
            //Deserialize the message
            Message tempMsg = HL7Utility.Deserialize(HL7Message);
            StringBuilder sb = new StringBuilder();

            //Format the segments
            foreach (Segment seg in tempMsg.Segments)
            {
                sb.Append("\t\t>>");
                foreach(string s in seg.Elements)
                {
                    sb.AppendFormat("{0}|", s);
                }
                sb.Append(Environment.NewLine);
            }

            //Write to log file
            Logger.logMessage(messageHeader +
                              Environment.NewLine +
                              sb.ToString());
        }
    }
}
