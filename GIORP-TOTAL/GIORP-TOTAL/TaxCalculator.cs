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
            ts.TotalAmount = CalculateTaxByRegion(provinceCode, amount);
            //throw new NotImplementedException();
            return ts;
        }

        public Models.TaxSummary BreakDownTax(string code, double amount)
        {
            TaxSummary ts = new TaxSummary();
            ts.NetAmount = amount;
            string region = ChooseRegion(code);
            string taxType = GetSaleTaxByRegion(code);
            double total = 0.0d;
            double totalGst = 0.0d;
            string pst = string.Empty;
            string gst = string.Empty;
            double rate = 0.0d;
            double HstAmount = 0.0d;

            if (taxType.Contains('-'))
            {
                pst = GetPst(taxType);
                gst = GetGst(taxType);

                Console.WriteLine(pst + " " + gst);
            }
            #region Hst
            //calculates HST tax for regions that have this tax 
            if (taxType.Equals("HST"))
            {
                switch (code.ToUpper())
                {
                    case "NL":
                        rate = ConvertToPercent((double)Enums.HSTRates.HSTMedium);
                        ts.HstAmount = CalculateTaxAmount(amount, rate);
                        //total = CalculateTax(rate, amount);
                        break;
                    case "NS":
                        rate = ConvertToPercent((double)Enums.HSTRates.HSTHigh);
                        ts.HstAmount = CalculateTaxAmount(amount, rate);
                        //total = CalculateTax(rate, amount);
                        break;
                    case "NB":
                        rate = ConvertToPercent((double)Enums.HSTRates.HSTMedium);
                        ts.HstAmount = CalculateTaxAmount(amount, rate);
                        //total = CalculateTax(rate, amount);
                        break;
                    case "ON":
                        rate = ConvertToPercent((double)Enums.HSTRates.HSTMedium);
                        ts.HstAmount = CalculateTaxAmount(amount, rate);
                        //total = CalculateTax(rate, amount);
                        break;
                    case "BC":
                        rate = ConvertToPercent((double)Enums.HSTRates.HSTLow);
                        ts.HstAmount = CalculateTaxAmount(amount, rate);
                        //total = CalculateTax(rate, amount);
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
                switch (code.ToUpper())
                {
                    case "PE":
                        rate = ConvertToPercent((double)Enums.GSTRate.GSTRate);
                        ts.GstAmount = CalculateTaxAmount(amount, rate);
                        rate = ConvertToPercent((double)Enums.PSTRates.PSTHigh);
                        ts.PstAmount = CalculateTaxAmount(amount, rate);
                        //rate = ConvertToPercent((double)Enums.GSTRate.GSTRate);
                        //totalGst = CalculateTax(rate, amount);
                        //rate = ConvertToPercent((double)Enums.PSTRates.PSTHigh);
                        //total = CalculateTax(rate, totalGst);
                        break;
                    case "QC":
                        rate = ConvertToPercent((double)Enums.GSTRate.GSTRate);
                        ts.GstAmount = CalculateTaxAmount(amount, rate);
                        rate = ConvertToPercent(9.5);
                        ts.PstAmount = CalculateTaxAmount(amount, rate);
                        //rate = ConvertToPercent((double)Enums.GSTRate.GSTRate);
                        //totalGst = CalculateTax(rate, amount);
                        //rate = ConvertToPercent(9.5);
                        //total = CalculateTax(rate, totalGst);
                        break;
                    case "MB":
                        rate = ConvertToPercent((double)Enums.GSTRate.GSTRate);
                        ts.GstAmount = CalculateTaxAmount(amount, rate);
                        rate = ConvertToPercent((double)Enums.PSTRates.PSTMedium);
                        ts.PstAmount = Math.Round(CalculateTaxAmount(amount, rate), 2);
                        //rate = ConvertToPercent((double)Enums.PSTRates.PSTMedium) + ConvertToPercent((double)Enums.GSTRate.GSTRate);
                        //total = CalculateTax(rate, amount);
                        break;
                    case "SK":
                        rate = ConvertToPercent((double)Enums.GSTRate.GSTRate);
                        ts.GstAmount = CalculateTaxAmount(amount, rate);
                        rate = ConvertToPercent((double)Enums.PSTRates.PSTLow);
                        ts.PstAmount = CalculateTaxAmount(amount, rate);
                        //rate = ConvertToPercent((double)Enums.PSTRates.PSTLow) + ConvertToPercent((double)Enums.GSTRate.GSTRate);
                        //total = CalculateTax(rate, amount);
                        break;
                    case "AB":
                        rate = ConvertToPercent((double)Enums.GSTRate.GSTRate);
                        ts.GstAmount = CalculateTaxAmount(amount, rate);
                        rate = ConvertToPercent((double)Enums.PSTRates.PSTZero);
                        ts.PstAmount = CalculateTaxAmount(amount, rate);
                        //rate = ConvertToPercent((double)Enums.PSTRates.PSTZero) + ConvertToPercent((double)Enums.GSTRate.GSTRate);
                        //total = CalculateTax(rate, amount);
                        break;
                    case "YT":
                        rate = ConvertToPercent((double)Enums.GSTRate.GSTRate);
                        ts.GstAmount = CalculateTaxAmount(amount, rate);
                        rate = ConvertToPercent((double)Enums.PSTRates.PSTZero);
                        ts.PstAmount = CalculateTaxAmount(amount, rate);
                        //rate = ConvertToPercent((double)Enums.PSTRates.PSTZero) + ConvertToPercent((double)Enums.GSTRate.GSTRate);
                        //total = CalculateTax(rate, amount);
                        break;
                    case "NT":
                        rate = ConvertToPercent((double)Enums.GSTRate.GSTRate);
                        ts.GstAmount = CalculateTaxAmount(amount, rate);
                        rate = ConvertToPercent((double)Enums.PSTRates.PSTZero);
                        ts.PstAmount = CalculateTaxAmount(amount, rate);
                        //rate = ConvertToPercent((double)Enums.PSTRates.PSTZero) + ConvertToPercent((double)Enums.GSTRate.GSTRate);
                        //total = CalculateTax(rate, amount);
                        break;
                    case "NU":
                        rate = ConvertToPercent((double)Enums.GSTRate.GSTRate);
                        ts.GstAmount = CalculateTaxAmount(amount, rate);
                        rate = ConvertToPercent((double)Enums.PSTRates.PSTZero);
                        ts.PstAmount = CalculateTaxAmount(amount, rate);
                        //rate = ConvertToPercent((double)Enums.PSTRates.PSTZero) + ConvertToPercent((double)Enums.GSTRate.GSTRate);
                        //total = CalculateTax(rate, amount);
                        break;
                    default:
                        break;
                }
            }
            #endregion


            return ts;
        }

        private static string ChooseRegion(string regions)
        {
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

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taxRate"></param>
        /// <returns></returns>
        private static string GetSaleTaxByRegion(string regionCode)
        {
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

        }

        private static double CalculateTaxByRegion(string r, double amount)
        {
            string region = ChooseRegion(r);
            string taxType = GetSaleTaxByRegion(r);
            double total = 0.0d;
            double totalGst = 0.0d;
            string pst = string.Empty;
            string gst = string.Empty;
            double rate = 0.0d;

            if (taxType.Contains('-'))
            {
                pst = GetPst(taxType);
                gst = GetGst(taxType);

                Console.WriteLine(pst + " " + gst);
            }
            #region Hst
            //calculates HST tax for regions that have this tax 
            if (taxType.Equals("HST"))
            {
                switch (r.ToUpper())
                {
                    case "NL":
                        rate = ConvertToPercent((double)Enums.HSTRates.HSTMedium);
                        total = CalculateTax(rate, amount);
                        break;
                    case "NS":
                        rate = ConvertToPercent((double)Enums.HSTRates.HSTHigh);
                        total = CalculateTax(rate, amount);
                        break;
                    case "NB":
                        rate = ConvertToPercent((double)Enums.HSTRates.HSTMedium);
                        total = CalculateTax(rate, amount);
                        break;
                    case "ON":
                        rate = ConvertToPercent((double)Enums.HSTRates.HSTMedium);
                        total = CalculateTax(rate, amount);
                        break;
                    case "BC":
                        rate =  ConvertToPercent((double)Enums.HSTRates.HSTLow);
                        total = CalculateTax(rate, amount);
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
                switch (r.ToUpper())
                {
                    case "PE":
                        rate = ConvertToPercent((double)Enums.GSTRate.GSTRate);
                        totalGst = CalculateTax(rate, amount);
                        rate = ConvertToPercent((double)Enums.PSTRates.PSTHigh);
                        total = CalculateTax(rate, totalGst);
                        break;
                    case "QC":
                        rate = ConvertToPercent((double)Enums.GSTRate.GSTRate);
                        totalGst = CalculateTax(rate, amount);
                        rate = ConvertToPercent(9.5);
                        total = CalculateTax(rate, totalGst);
                        break;
                    case "MB":
                        rate = ConvertToPercent((double)Enums.PSTRates.PSTMedium) + ConvertToPercent((double)Enums.GSTRate.GSTRate);
                        total = CalculateTax(rate, amount);
                        break;
                    case "SK":
                        rate = ConvertToPercent((double)Enums.PSTRates.PSTLow) + ConvertToPercent((double)Enums.GSTRate.GSTRate);
                        total = CalculateTax(rate, amount);
                        break;
                    case "AB":
                        rate = ConvertToPercent((double)Enums.PSTRates.PSTZero) + ConvertToPercent((double)Enums.GSTRate.GSTRate);
                        total = CalculateTax(rate, amount);
                        break;
                    case "YT":
                        rate = ConvertToPercent((double)Enums.PSTRates.PSTZero) + ConvertToPercent((double)Enums.GSTRate.GSTRate);
                        total = CalculateTax(rate, amount);
                        break;
                    case "NT":
                        rate = ConvertToPercent((double)Enums.PSTRates.PSTZero) + ConvertToPercent((double)Enums.GSTRate.GSTRate);
                        total = CalculateTax(rate, amount);
                        break;
                    case "NU":
                        rate = ConvertToPercent((double)Enums.PSTRates.PSTZero) + ConvertToPercent((double)Enums.GSTRate.GSTRate);
                        total = CalculateTax(rate, amount);
                        break;
                    default:
                        break;
                }
            }
            #endregion
            return Math.Round(total, 2);
            //Console.WriteLine(string.Format("Thank you for purchasing in {1} \nNet total ${5}\n{2} {3:N2} %  ${4:N2} \nTotal ${0:N2}", total, ChooseRegion(r), taxType, rate * 100, value * rate, value));
            //Console.ReadKey();
        }

        private static string GetPst(string taxType)
        {
            string pst = string.Empty;
            string[] separator = taxType.Split('-');
            pst = separator[0];
            return pst;
        }

        private static string GetGst(string taxType)
        {
            string gst = string.Empty;
            string[] separator = taxType.Split('-');
            gst = separator[1];
            return gst;
        }

        private static double CalculateTax(double gst, double pst, double value)
        {
            double total = 0.0d;
            double subGst = 0.0d;
            double subPst = 0.0d;
            subGst = value * gst;
            value = value + subGst;
            subPst = value * pst;
            total = value + subPst;
            return total;
        }

        private static double CalculateTax(double tax, double amount)
        {
            double total = 0.0d;
            double subTotal = tax * amount;
            total = subTotal + amount;
            return total;
        }

        private static double ConvertToPercent(double PercentAmount)
        {
            const double RATE = 100.00D;
            double result = (PercentAmount / RATE);
            return (PercentAmount / RATE);
        }


        private static double CalculateTaxAmount(double amount, double rate)
        {
            return amount * rate;
        }
    }
}
