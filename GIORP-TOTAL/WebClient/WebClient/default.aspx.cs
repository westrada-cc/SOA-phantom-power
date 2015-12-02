using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GIORP_TOTAL;

namespace WebClient
{
    public partial class _default : System.Web.UI.Page
    {
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
            TaxCalculator tc = new TaxCalculator();

            // call the service
            province = provinceList.SelectedValue;
            double.TryParse(priceBox.Text, out amount);

            GIORP_TOTAL.Models.TaxSummary taxes = tc.CalculateTax(province, amount);

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