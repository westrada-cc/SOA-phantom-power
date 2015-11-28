using GIORP_TOTAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIORP_TOTAL
{
    public class TaxCalculator : ITaxCalculator
    {
        public Models.TaxSummary CalculateTax(string provinceCode, double amount)
        {
            TaxSummary ts = new TaxSummary();
            ts = CalculateTaxByRegion(provinceCode, amount);
            return ts;
        }

        /// <summary>
        /// Determines the name of the province according to the province code
        /// </summary>
        /// <param name="regions"> code of the province</param>
        /// <returns>name of the province</returns>
        private static string ChooseRegion(string regions)
        {
            #region name of the province
            string whichRegion = string.Empty;

            switch (regions.ToUpper())
            {
                case "NL":
                    whichRegion = "Newfoundland";
                    break;
                case "NS":
                    whichRegion = "Nova Scotia";
                    break;
                case "NB":
                    whichRegion = "New Brunswick";
                    break;
                case "PE":
                    whichRegion = "Prince Edward Island";
                    break;
                case "QC":
                    whichRegion = "Quebec";
                    break;
                case "ON":
                    whichRegion = "Ontario";
                    break;
                case "MB":
                    whichRegion = "Manitoba";
                    break;
                case "SK":
                    whichRegion = "Saskatchewan";
                    break;
                case "AB":
                    whichRegion = "Alberta";
                    break;
                case "BC":
                    whichRegion = "British Columbia";
                    break;
                case "YT":
                    whichRegion = "Yukon Territories";
                    break;
                case "NT":
                    whichRegion = "Northwest Territories";
                    break;
                case "NU":
                    whichRegion = "Nunavut";
                    break;
                default:
                    whichRegion = "This is not a valid province";
                    break;
            }

            return string.Format(whichRegion.ToUpper());
            #endregion
        }

        /// <summary>
        ///  gets the tax code that correspond to the province where the purchase is being held
        /// </summary>
        /// <param name="taxRate"> percetage of the tax</param>
        /// <returns>the tax code that belongs to the province</returns>
        private static string GetSaleTaxByRegion(string regionCode)
        {
            #region tax code by province
            string taxCode = string.Empty;
            string pst = "PST";
            string hst = "HST";
            string gst = "GST";
            string separetor = "-";
            switch (regionCode.ToUpper())
            {
                case "NL":
                    taxCode = Enum.Parse(typeof(Enums.Taxes), hst).ToString();
                    break;
                case "NS":
                    taxCode = Enum.Parse(typeof(Enums.Taxes), hst).ToString();
                    break;
                case "NB":
                    taxCode = Enum.Parse(typeof(Enums.Taxes), hst).ToString();
                    break;
                case "PE":
                    taxCode = Enum.Parse(typeof(Enums.Taxes), pst).ToString() + separetor + Enum.Parse(typeof(Enums.Taxes), gst).ToString();
                    break;
                case "QC":
                    taxCode = Enum.Parse(typeof(Enums.Taxes), pst).ToString() + separetor + Enum.Parse(typeof(Enums.Taxes), gst).ToString();
                    break;
                case "ON":
                    taxCode = Enum.Parse(typeof(Enums.Taxes), hst).ToString();
                    break;
                case "MB":
                    taxCode = Enum.Parse(typeof(Enums.Taxes), pst).ToString() + separetor + Enum.Parse(typeof(Enums.Taxes), gst).ToString();
                    break;
                case "SK":
                    taxCode = Enum.Parse(typeof(Enums.Taxes), pst).ToString() + separetor + Enum.Parse(typeof(Enums.Taxes), gst).ToString();
                    break;
                case "AB":
                    taxCode = Enum.Parse(typeof(Enums.Taxes), pst).ToString() + separetor + Enum.Parse(typeof(Enums.Taxes), gst).ToString();
                    break;
                case "BC":
                    taxCode = Enum.Parse(typeof(Enums.Taxes), hst).ToString();
                    break;
                case "YT":
                    taxCode = Enum.Parse(typeof(Enums.Taxes), pst).ToString() + separetor + Enum.Parse(typeof(Enums.Taxes), gst).ToString();
                    break;
                case "NT":
                    taxCode = Enum.Parse(typeof(Enums.Taxes), pst).ToString() + separetor + Enum.Parse(typeof(Enums.Taxes), gst).ToString();
                    break;
                case "NU":
                    taxCode = Enum.Parse(typeof(Enums.Taxes), pst).ToString() + separetor + Enum.Parse(typeof(Enums.Taxes), gst).ToString();
                    break;
                default:
                    break;
            }

            return taxCode;
            #endregion
        }



        /// <summary>
        /// This method calculates all taxes for each region 
        /// </summary>
        /// <param name="region">code of the province in the format of "ON"</param>
        /// <param name="amount">the value to calculate taxes on</param>
        /// <returns>an TaxSummary objec, which contains the NetAmount, PST, HST, GST and total after taxes</returns>
        private Models.TaxSummary CalculateTaxByRegion(string region, double amount)
        {
            #region method Initializers
            TaxSummary ts = new TaxSummary();
            ts.NetAmount = amount;
            string getRegionName = ChooseRegion(region);
            string taxType = GetSaleTaxByRegion(region);
            string pst = string.Empty;
            string gst = string.Empty;
            const double qcPstTaxRate = 9.5d;

            //get tax type pst and gst to be applied where available
            if (taxType.Contains('-'))
            {
                pst = GetPst(taxType);
                gst = GetGst(taxType);
            }
            #endregion
            #region Hst
            //calculates HST tax for regions that have this tax 
            if (taxType.Equals("HST"))
            {
                switch (region.ToUpper())
                {
                    case "NL":
                        ts.HstAmount = CalculateTaxAmount(amount, ConvertToPercent((double)Enums.HSTRates.HSTMedium));
                        ts.TotalAmount = CalculateTax(ConvertToPercent((double)Enums.HSTRates.HSTMedium), amount);
                        break;
                    case "NS":
                        ts.HstAmount = CalculateTaxAmount(amount, ConvertToPercent((double)Enums.HSTRates.HSTHigh));
                        ts.TotalAmount = CalculateTax(ConvertToPercent((double)Enums.HSTRates.HSTHigh), amount);
                        break;
                    case "NB":
                        ts.HstAmount = CalculateTaxAmount(amount, ConvertToPercent((double)Enums.HSTRates.HSTMedium));
                        ts.TotalAmount = CalculateTax(ConvertToPercent((double)Enums.HSTRates.HSTMedium), amount);
                        break;
                    case "ON":
                        ts.HstAmount = CalculateTaxAmount(amount, ConvertToPercent((double)Enums.HSTRates.HSTMedium));
                        ts.TotalAmount = CalculateTax(ConvertToPercent((double)Enums.HSTRates.HSTMedium), amount);
                        break;
                    case "BC":
                        ts.HstAmount = CalculateTaxAmount(amount, ConvertToPercent((double)Enums.HSTRates.HSTLow));
                        ts.TotalAmount = CalculateTax(ConvertToPercent((double)Enums.HSTRates.HSTLow), amount);
                        break;
                    default:
                        break;
                }
            }
            #endregion
            #region Pst-Gst
            //calculates the PST and GST for the regions that have these types of taxes
            if (taxType.Equals("PST-GST"))
            {
                switch (region.ToUpper())
                {
                    case "PE":
                        ts.GstAmount = CalculateTaxAmount(amount, ConvertToPercent((double)Enums.GSTRate.GSTRate));
                        ts.PstAmount = CalculateTaxAmount(CalculateTax(ConvertToPercent((double)Enums.GSTRate.GSTRate), amount), ConvertToPercent((double)Enums.PSTRates.PSTHigh));
                        ts.TotalAmount = CalculateTax(ConvertToPercent((double)Enums.PSTRates.PSTHigh), CalculateTax(ConvertToPercent((double)Enums.GSTRate.GSTRate), amount));
                        break;
                    case "QC":
                        ts.GstAmount = CalculateTaxAmount(amount, ConvertToPercent((double)Enums.GSTRate.GSTRate));
                        ts.PstAmount = CalculateTaxAmount(CalculateTax(ConvertToPercent((double)Enums.GSTRate.GSTRate), amount), ConvertToPercent(qcPstTaxRate));
                        ts.TotalAmount = CalculateTax(ConvertToPercent(qcPstTaxRate), CalculateTax(ConvertToPercent((double)Enums.GSTRate.GSTRate), amount));
                        break;
                    case "MB":
                        ts.GstAmount = CalculateTaxAmount(amount, ConvertToPercent((double)Enums.GSTRate.GSTRate));
                        ts.PstAmount = Math.Round(CalculateTaxAmount(amount, ConvertToPercent((double)Enums.PSTRates.PSTMedium)), 2);
                        ts.TotalAmount = CalculateTax((ConvertToPercent((double)Enums.GSTRate.GSTRate) + ConvertToPercent((double)Enums.PSTRates.PSTMedium)), amount);
                        break;
                    case "SK":
                        ts.GstAmount = CalculateTaxAmount(amount, ConvertToPercent((double)Enums.GSTRate.GSTRate));
                        ts.PstAmount = CalculateTaxAmount(amount, ConvertToPercent((double)Enums.PSTRates.PSTLow));
                        ts.TotalAmount = CalculateTax((ConvertToPercent((double)Enums.GSTRate.GSTRate) + ConvertToPercent((double)Enums.PSTRates.PSTLow)), amount);
                        break;
                    case "AB":
                        ts.GstAmount = CalculateTaxAmount(amount, ConvertToPercent((double)Enums.GSTRate.GSTRate));
                        ts.PstAmount = CalculateTaxAmount(amount, ConvertToPercent((double)Enums.PSTRates.PSTZero));
                        ts.TotalAmount = CalculateTax((ConvertToPercent((double)Enums.GSTRate.GSTRate) + ConvertToPercent((double)Enums.PSTRates.PSTZero)), amount);
                        break;
                    case "YT":
                        ts.GstAmount = CalculateTaxAmount(amount, ConvertToPercent((double)Enums.GSTRate.GSTRate));
                        ts.PstAmount = CalculateTaxAmount(amount, ConvertToPercent((double)Enums.PSTRates.PSTZero));
                        ts.TotalAmount = CalculateTax((ConvertToPercent((double)Enums.GSTRate.GSTRate) + ConvertToPercent((double)Enums.PSTRates.PSTZero)), amount);
                        break;
                    case "NT":
                        ts.GstAmount = CalculateTaxAmount(amount, ConvertToPercent((double)Enums.GSTRate.GSTRate));
                        ts.PstAmount = CalculateTaxAmount(amount, ConvertToPercent((double)Enums.PSTRates.PSTZero));
                        ts.TotalAmount = CalculateTax((ConvertToPercent((double)Enums.GSTRate.GSTRate) + ConvertToPercent((double)Enums.PSTRates.PSTZero)), amount);
                        break;
                    case "NU":
                        ts.GstAmount = CalculateTaxAmount(amount, ConvertToPercent((double)Enums.GSTRate.GSTRate));
                        ts.PstAmount = CalculateTaxAmount(amount, ConvertToPercent((double)Enums.PSTRates.PSTZero));
                        ts.TotalAmount = CalculateTax((ConvertToPercent((double)Enums.GSTRate.GSTRate) + ConvertToPercent((double)Enums.PSTRates.PSTZero)), amount);
                        break;
                    default:
                        break;
                }
            }
            return ts;
            #endregion
        }

        /// <summary>
        /// gets the PST tax for a specific province
        /// </summary>
        /// <param name="taxType">PST</param>
        /// <returns>PST for a province</returns>
        private static string GetPst(string taxType)
        {
            string pst = string.Empty;
            string[] separator = taxType.Split('-');
            pst = separator[0];
            return pst;
        }



        /// <summary>
        /// gets the GST tax for a specific province
        /// </summary>
        /// <param name="taxType">GST</param>
        /// <returns>GST for a province</returns>
        private static string GetGst(string taxType)
        {
            string gst = string.Empty;
            string[] separator = taxType.Split('-');
            gst = separator[1];
            return gst;
        }

        //private static double CalculateTax(double gst, double pst, double value)
        //{
        //    double total = 0.0d;
        //    double subGst = 0.0d;
        //    double subPst = 0.0d;
        //    subGst = value * gst;
        //    value = value + subGst;
        //    subPst = value * pst;
        //    total = value + subPst;
        //    return total;
        //}


        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tax"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        private static double CalculateTax(double tax, double amount)
        {
            double total = 0.0d;
            double subTotal = tax * amount;
            total = subTotal + amount;
            return total;
        }

     

        /// <summary>
        /// convert to percentage each tax amount
        /// </summary>
        /// <param name="PercentAmount"> and integer number repesented the tax percentage</param>
        /// <returns>a number in decimal format to represent the percentage of the tax type</returns>
        private static double ConvertToPercent(double PercentAmount)
        {
            const double RATE = 100.00D;
            return (PercentAmount / RATE);
        }

     


        /// <summary>
        /// Calculates individual tax amount for each type of tax
        /// </summary>
        /// <param name="amount">the value that the tax is applied to</param>
        /// <param name="rate">the percentage of the tax rate</param>
        /// <returns>tax amount</returns>
        private static double CalculateTaxAmount(double amount, double rate)
        {
            return amount * rate;
        }
    }
}
