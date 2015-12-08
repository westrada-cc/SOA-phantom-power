﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GIORP_TOTAL;
using GIORP_TOTAL.Models;
using HL7Library;

namespace WebClient
{
    public partial class Start : System.Web.UI.Page
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
        const string QueryServiceElement = "QUERY-SERVICE";
        const string SOAOkElement = "OK";
        const string SOANotOkElement = "NOT-OK";
        const string ExecuteServiceElement = "EXEC-SERVICE";

        protected void Page_Load(object sender, EventArgs e)
        {
            Message msg = new Message();
            string request = "";
            string response = "";

            try
            {
                //  DRC|QUERY-SERVICE|<team name>|<team ID>|
                msg.AddSegment(CommandDirectiveElement, QueryServiceElement, "PhantomPower", "0");
                //  SRV|GIORP-TOTAL||||||
                msg.AddSegment(ServiceDirectiveElement, "GIORP-TOTAL", "", "", "");

                //Serialize the message
                request = HL7Utility.Serialize(msg);
                //Send the request
                response = ServiceClient.SendRequest(request, "localhost", 15000);
                //Deserialize the response
                msg = HL7Utility.Deserialize(response);   

            }
            catch(Exception ex)
            {

            }
        }

        protected void submitButton_Click(object sender, EventArgs e)
        {
        }
    }
}