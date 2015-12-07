using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GIORP_TOTAL;
using GIORP_TOTAL.Models;
using HL7Library;
using System.Diagnostics;

namespace WebClient
{
    public partial class Start : System.Web.UI.Page
    {
        private const string CommandDirectiveElement = "DRC";
        private const string SOADirectiveElement = "SOA";
        private const string ArgumentDirectiveElement = "ARG";
        private const string ResponseDirectiveElement = "RSP";
        private const string MCHDirectiveElement = "MCH";
        private const string InfoDirectiveElement = "INF";
        private const string ServiceDirectiveElement = "SRV";
        private const string PublishedServiceDirectiveElement = "PUB";

        private const string RegisterTeamElement = "REG-TEAM";
        private const string PublishServiceElement = "PUB-SERVICE";
        private const string QueryTeamElement = "QUERY-TEAM";
        private const string QueryServiceElement = "QUERY-SERVICE";
        private const string SOAOkElement = "OK";
        private const string SOANotOkElement = "NOT-OK";
        private const string ExecuteServiceElement = "EXEC-SERVICE";

        protected void Page_Load(object sender, EventArgs e)
        {
            // set default div visibilities
            errorServiceDiv.Visible = false;
            noServiceDiv.Visible = false;
        }

        protected void submitButton_Click(object sender, EventArgs e)
        {
            Message msg = new Message();
            string request = "";
            string response = "";
            int idVerification = 0;

            try
            {
                //  DRC|QUERY-SERVICE|<team name>|<team ID>|
                msg.AddSegment(CommandDirectiveElement, QueryServiceElement, nameTextBox.Text, idTextBox.Text);
                //  SRV|GIORP-TOTAL||||||
                msg.AddSegment(ServiceDirectiveElement, "GIORP-TOTAL", "", "", "", "", "");

                //Serialize the message
                request = HL7Utility.Serialize(msg);
                //Send the request
                response = ServiceClient.SendRequest(request, "10.113.21.151", 3128);
                //Deserialize the response
                msg = HL7Utility.Deserialize(response.TrimEnd('\n'));

                if (msg.Segments[0].Elements[1] == SOAOkElement)
                {
                    errorServiceDiv.Visible = false;
                    noServiceDiv.Visible = false;

                    // double check if the idTextBox value is valid
                    if (Int32.TryParse(idTextBox.Text, out idVerification) == false)
                    {
                        throw new Exception("Team ID was invalid");
                    } 

                    // load up the session variables
                    Session["TeamName"] = nameTextBox.Text;
                    Session["TeamID"] = idTextBox.Text;
                    Session["ServiceName"] = msg.Segments[1].Elements[2];
                    Session["NumArgs"] = msg.Segments[1].Elements[4];
                    Session["ServiceDescription"] = msg.Segments[1].Elements[6];
                    Session["ArgName1"] = GetArgument(1, msg)[0];
                    Session["ArgDataType1"] = GetArgument(1, msg)[1];
                    Session["ArgName2"] =  GetArgument(2, msg)[0];
                    Session["ArgDataType2"] = GetArgument(2, msg)[1];
                    Session["IPAddress"] = GetIPAddress(msg);
                    Session["portNumber"] = GetPortNumber(msg);

                    // transfer to the next page
                    Server.Transfer("default.aspx", true);

                }
                else if (msg.Segments[0].Elements[1] == SOANotOkElement)
                {
                    ErrorMessageID.InnerText = msg.Segments[0].Elements[3];
                    errorServiceDiv.Visible = true;
                    noServiceDiv.Visible = false;
                }
                else
                {
                    errorServiceDiv.Visible = false;
                    noServiceDiv.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        string GetIPAddress(Message msg)
        {
            string result = "";
            int segPos = 0;

            //Search the segments
            foreach (Segment seg in msg.Segments)
            {
                foreach (string s in seg.Elements)
                {
                    if (s.Contains("MCH"))
                    {
                        result = msg.Segments[segPos].Elements[1];
                    }
                }

                ++segPos;
            }

            return result;
        }

        int GetPortNumber(Message msg)
        {
            int result = 0;
            int segPos = 0;

            //Search the segments
            foreach (Segment seg in msg.Segments)
            {
                foreach (string s in seg.Elements)
                {
                    if (s.Contains("MCH"))
                    {
                        Int32.TryParse(msg.Segments[segPos].Elements[2], out result);
                    }
                }

                ++segPos;
            }

            return result;
        }

        List<string> GetArgument(int argumentPos, Message msg)
        {
            List<string> result = new List<string>();
            int segPos = 0;
            bool pass = true;

            //Search the segments
            foreach (Segment seg in msg.Segments)
            {
                foreach (string s in seg.Elements)
                {
                    if (s.Contains("ARG"))
                    {
                        if (seg.Elements[4].Contains("mandatory"))
                        {
                            if (argumentPos == 1)
                            {
                                result.Add(msg.Segments[segPos].Elements[2]);
                                result.Add(msg.Segments[segPos].Elements[3]);
                                break;
                            }
                            else if (argumentPos == 2)
                            {
                                if (pass)
                                {
                                    pass = false;
                                }
                                else
                                {
                                    result.Add(msg.Segments[segPos].Elements[2]);
                                    result.Add(msg.Segments[segPos].Elements[3]);
                                    break;
                                }
                            }
                        }
                    }
                }

                ++segPos;
            }

            return result;
        }
    }
}