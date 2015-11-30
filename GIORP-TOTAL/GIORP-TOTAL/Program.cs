using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HL7Library;
using GIORP_TOTAL.Helpers;

namespace GIORP_TOTAL
{
    class Program
    {
        const string CommandDirectiveElement = "DRC";
        const string SOADirectiveElement = "SOA";
        const string ArgumentDirectiveElement = "ARG";
        const string ResponseDirectiveElement = "RSP";
        const string MCHDirectiveElement = "MCH";
        const string InfoDirectiveElement = "INF";
        const string ServiceDirectiveElement = "SRV";

        const string RegisterTeamElement = "REG-TEAM";
        const string PublishServiceElement = "PUB-SERVICE";

        static Properties.Settings _settings = Properties.Settings.Default;

        static string _teamId = null;

        static void Main(string[] args)
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
                try
                {
                    var registerTeamMessage = CreateRegisterTeamMessage(_settings.TeamName);
                    teamRegistractionResponse = SocketClient.SendRequest(registerTeamMessage, _settings.SOARegistryIp, _settings.SOARegistryPort);
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not register the team.", ex);
                }
                if (teamRegistractionResponse != null)
                {
                    var teamRegistractionResponseMessage = HL7Utility.Deserialize(teamRegistractionResponse);
                    if (teamRegistractionResponseMessage.Segments.Count > 0 &&
                        teamRegistractionResponseMessage.Segments[0].Elements.Count > 2 &&
                        teamRegistractionResponseMessage.Segments[0].Elements[0] == SOADirectiveElement &&
                        teamRegistractionResponseMessage.Segments[0].Elements[0] == "OK")
                    {
                        Console.WriteLine("Team Registered.");
                    }

                    // Register service

                    return true;
                }
	        }
	        catch (Exception e)
	        {
		        Console.WriteLine("Exception: " + e + "InnerException: " + e.InnerException);
	        }
            

            return false;
        }

        static string CreateRegisterTeamMessage(string teamName)
        {
            var message = new Message();
            message.AddSegment(CommandDirectiveElement, RegisterTeamElement, "", "");
            message.AddSegment(InfoDirectiveElement, teamName);
            return HL7Utility.Serialize(message);
        }

        static string CreateRegisterServiceMessage()
        {
            var message = new Message();
            // DRC|PUB-SERVICE|<team name>|<teamID>|
            message.AddSegment(CommandDirectiveElement, PublishServiceElement, _settings.TeamName, _teamId); 
            // SRV|<tag name>|<service name>|<security level>|<num args>|<num responses>|<description>|
            message.AddSegment(ServiceDirectiveElement, _settings.ServiceTagName, _settings.ServiceName, _settings.ServiceSecurityLevel, "2", "5", _settings.ServiceDescription);
            // ARG|<arg position>|<arg name>|<arg data type>|[mandatory | optional]||
            message.AddSegment(ArgumentDirectiveElement, "1", "province", "string", "mandatory");
            // ARG|<arg position>|<arg name>|<arg data type>|[mandatory | optional]||
            message.AddSegment(ArgumentDirectiveElement, "2", "amount", "double", "mandatory");
            //  RSP|<resp position>|<resp name>|<resp data type>||
            message.AddSegment(ResponseDirectiveElement, "1", "");
            //  MCH|<published server IP>|<published port>| 
        }

        static void Server_RequestReceived(object sender, RequestReceivedEventArgs e)
        {
            Console.WriteLine("Service received a request:");
            Console.WriteLine(e.Request);
            var response = HandleRequest(e.Request);
            Console.WriteLine("Service responding with:");
            Console.WriteLine(response);
        }

        static string HandleRequest(string request)
        {
            return null;
        }
    }
}
