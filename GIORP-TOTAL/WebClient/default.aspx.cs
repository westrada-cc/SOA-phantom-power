using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GIORP_TOTAL;
using GIORP_TOTAL.Models;
using HL7Library;
using System.Text;
using System.Text.RegularExpressions;

namespace WebClient
{
    public partial class _default : System.Web.UI.Page
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
        private const string SOAOkElement = "OK";
        private const string SOANotOkElement = "NOT-OK";
        private const string ExecuteServiceElement = "EXEC-SERVICE";
        private const string amountPattern = @"^([1-9]+\.?[0-9]{1,2}|[0]{1}\.?[0-9]{2}|\.?[0-9]{1,2})$";

        private string teamName;
        private string teamID;
        private string teamServiceName;
        private string serviceName;
        private string numArgs;
        private string serviceDescription;
        private string argName1;
        private string argDataType1;
        private string argName2;
        private string argDataType2;
        private string IP;
        private int port;

        protected void Page_Load(object sender, EventArgs e)
        {
            results.Visible = false;

            try
            {
                // set all the session variables
                teamName = (string)Session["TeamName"];
                teamID = (string)Session["TeamID"];
                serviceName = (string)Session["ServiceName"];
                teamServiceName = (string)Session["ServiceTeamName"]; 
                numArgs = (string)Session["NumArgs"];
                serviceDescription = (string)Session["ServiceDescription"];
                argName1 = (string)Session["ArgName1"];
                argDataType1 = (string)Session["ArgDataType1"];
                argName2 = (string)Session["ArgName2"];
                argDataType2 = (string)Session["ArgDataType2"];
                IP = (string)Session["IPAddress"];
                port = (int)Session["portNumber"];
            }
            catch (Exception ex)
            {
                alert.Text = ex.Message;
                Logger.logException(ex);

                // redirect back to the start page if there's a session variable exception
                Response.Redirect("Start.aspx");
            }

            teamNameID.InnerText = "Team Name: " + teamServiceName; 
            serviceNameID.InnerText = "Service Name: " + serviceName;
        }

        protected void clearButton_Click(object sender, EventArgs e)
        {
            priceBox.Text = "";
            provinceList.SelectedIndex = 0;
        }

        protected void submitButton_Click(object sender, EventArgs e)
        {
            string province;
            double amount = 0;
            string request = "";
            string response = "";
            TaxSummary taxes = new GIORP_TOTAL.Models.TaxSummary();
            Message msg = new Message();

            province = provinceList.SelectedValue;
            double.TryParse(priceBox.Text, out amount);

            try
            {
                Regex priceRegex = new Regex(amountPattern);

                if (!priceRegex.IsMatch(amount.ToString()))
                {
                    throw new Exception("The amount value was invalid");
                }

                //  DRC|EXEC-SERVICE|PhantomPower|0|
                msg.AddSegment(CommandDirectiveElement, ExecuteServiceElement, teamName, teamID);
                //  SRV||PP-GIORP-TOTAL||2|||
                msg.AddSegment(ServiceDirectiveElement, "", serviceName, "", numArgs, "", "");
                //  ARG|1|province|string||PROVINCE CODE|
                if (argDataType1.IndexOf("string", 0, StringComparison.CurrentCultureIgnoreCase) != -1)
                {
                    msg.AddSegment(ArgumentDirectiveElement, "1", argName1, argDataType1, "", province);
                }
                else
                {
                    msg.AddSegment(ArgumentDirectiveElement, "1", argName1, argDataType1, "", amount.ToString());
                }
                //  ARG|2|amount|double||AMOUNT|
                if (argDataType2.IndexOf("double", 0, StringComparison.CurrentCultureIgnoreCase) != -1 ||
                    argDataType2.IndexOf("float", 0, StringComparison.CurrentCultureIgnoreCase) != -1)
                {
                    msg.AddSegment(ArgumentDirectiveElement, "2", argName2, argDataType2, "", amount.ToString());
                }
                else
                {
                    msg.AddSegment(ArgumentDirectiveElement, "2", argName2, argDataType2, "", province);
                }

                //Serialize the message
                request = HL7Utility.Serialize(msg);
                //Send the request
                response = ServiceClient.SendRequest(request, IP, port);
                //Deserialize the response
                msg = HL7Utility.Deserialize(response);   
             
                if (msg.Segments[0].Elements[1] == SOAOkElement)
                {
                    taxes = null; //MessageToTaxSummary(msg);
                    response1.Text = msg.Segments[1].Elements[2] + ": " + msg.Segments[1].Elements[4];
                    response2.Text = msg.Segments[2].Elements[2] + ": " + msg.Segments[2].Elements[4];
                    response3.Text = msg.Segments[3].Elements[2] + ": " + msg.Segments[3].Elements[4];
                    response4.Text = msg.Segments[4].Elements[2] + ": " + msg.Segments[4].Elements[4];
                    response5.Text = msg.Segments[5].Elements[2] + ": " + msg.Segments[5].Elements[4];

                    // show the results div with the results
                    results.Visible = true;
                    alertDiv.Visible = false;
                }
                else if(msg.Segments[0].Elements[1] == SOANotOkElement)
                {
                    taxes = null;
                    alert.Text = msg.Segments[0].Elements[3];
                    alertDiv.Visible = true;
                    results.Visible = false;
                }
            }
            catch (Exception ex)
            {
                alert.Text = ex.Message;
                Logger.logException(ex);
            }

            //if (taxes != null)
            //{
            //    // fill in the labels with the responses
            //    try
            //    {
            //        // go through each segment and get the second element (name of field) and forth element (value)
            //        response1.Text = msg.Segments[1].Elements[2] + ": " + msg.Segments[1].Elements[4];
            //        response2.Text = msg.Segments[2].Elements[2] + ": " + msg.Segments[2].Elements[4];
            //        response3.Text = msg.Segments[3].Elements[2] + ": " + msg.Segments[3].Elements[4];
            //        response4.Text = msg.Segments[4].Elements[2] + ": " + msg.Segments[4].Elements[4];
            //        response5.Text = msg.Segments[5].Elements[2] + ": " + msg.Segments[5].Elements[4];
            //    }
            //    catch (Exception ex)
            //    {
            //        alert.Text = ex.Message;
            //        Logger.logException(ex);
            //    }

            //    // format the table
            //    //subtotalAmount.Text = taxes.NetAmount.ToString("C2");
            //    //pstAmount.Text = taxes.PstAmount.ToString("C2");
            //    //hstAmount.Text = taxes.HstAmount.ToString("C2");
            //    //gstAmount.Text = taxes.GstAmount.ToString("C2");
            //    //totalPurchaseAmount.Text = taxes.TotalAmount.ToString("C2");

            //    // show the results div with the results
            //    results.Visible = true;
            //    alertDiv.Visible = false;
            //}
            //else
            //{
            //    alertDiv.Visible = true;
            //    results.Visible = false;
            //}
        }

        private static TaxSummary MessageToTaxSummary(Message msg)
        {
            TaxSummary ts = new TaxSummary();
            double temp = 0.0;
            
            //For sanity...
            //If its ok to parse...
            if (msg != null)
            {
                if (msg.Segments[0].Elements[1] == SOAOkElement)
                {
                    if (msg.Segments[1].Elements[2].Equals("NetAmount") &&
                        msg.Segments[2].Elements[2].Equals("PstAmount") &&
                        msg.Segments[3].Elements[2].Equals("HstAmount") &&
                        msg.Segments[4].Elements[2].Equals("GstAmount") &&
                        msg.Segments[5].Elements[2].Equals("TotalAmount"))
                    {
                        double.TryParse(msg.Segments[1].Elements[4], out temp);
                        ts.NetAmount = temp;

                        double.TryParse(msg.Segments[2].Elements[4], out temp);
                        ts.PstAmount = temp;

                        double.TryParse(msg.Segments[3].Elements[4], out temp);
                        ts.HstAmount = temp;

                        double.TryParse(msg.Segments[4].Elements[4], out temp);
                        ts.GstAmount = temp;

                        double.TryParse(msg.Segments[5].Elements[4], out temp);
                        ts.TotalAmount = temp;
                    }
                }
            }

            return ts;
        }
    }
}