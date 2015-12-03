using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GIORP_TOTAL;
using HL7Library;

namespace WebClient
{
    public partial class _default : System.Web.UI.Page
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

        protected void Page_Load(object sender, EventArgs e)
        {
            results.Visible = false;
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
            //TaxCalculator tc = new TaxCalculator();
            string request = "";
            string response = "";
            GIORP_TOTAL.Models.TaxSummary taxes = new GIORP_TOTAL.Models.TaxSummary();
            Message msg = new Message();

            // call the service
            province = provinceList.SelectedValue;
            double.TryParse(priceBox.Text, out amount);

            //GIORP_TOTAL.Models.TaxSummary taxes = tc.CalculateTax(province, amount);
            try
            {
                //  DRC|EXEC-SERVICE|PhantomPower|0|
                msg.AddSegment(CommandDirectiveElement, ExecuteServiceElement, "PhantomPower", "0");
                //  SRV||PP-GIORP-TOTAL||2|||
                msg.AddSegment(ServiceDirectiveElement, "", "PP-GIORP-TOTAL", "", "2", "", "");
                //  ARG|1|province|string||PROVINCE CODE|
                msg.AddSegment(ArgumentDirectiveElement, "1", "province", "string", "", province);
                //  ARG|2|amount|double||AMOUNT|
                msg.AddSegment(ArgumentDirectiveElement, "2", "amount", "double", "", amount.ToString());

                //Serialize the message
                request = HL7Utility.Serialize(msg);
                //Send the request
                response = ServiceClient.SendRequest(request, "localhost", 15000);
                //Deserialize the response
                msg =  HL7Utility.Deserialize(response);   
             
                if (msg.Segments[0].Elements[1] == SOAOkElement)
                {
                    //Do something with it...
                }
            }
            catch (System.Exception ex)
            {                
                Logger.logException(ex);
            }

            if (taxes != null)
            {
                // format the table
                subtotalAmount.Text = taxes.NetAmount.ToString();
                pstAmount.Text = taxes.PstAmount.ToString();
                hstAmount.Text = taxes.HstAmount.ToString();
                gstAmount.Text = taxes.GstAmount.ToString();
                totalPurchaseAmount.Text = taxes.TotalAmount.ToString();

                // show the results div with the results
                results.Visible = true;
            }
        }
    }
}