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

        private static double CalculateTaxByRegion(string r, double value)
        {
            string region = ChooseRegion(r);
            string taxType = GetSaleTaxByRegion(r);
            double total = 0.0d;
            string pst = string.Empty;
            string gst = string.Empty;
            double gstRate = 0.05d;
            double subPst = 0.0d;
            double rate = 0.0d;

            if (taxType.Contains('-'))
            {
                pst = GetPst(taxType);
                gst = GetGst(taxType);

                Console.WriteLine(pst + " " + gst);
            }
            //calculates HST tax for regions that have this tax 
            if (taxType.Equals("HST"))
            {
                switch (r.ToUpper())
                {
                    case "NL":
                        rate = ConvertToPercent((int)Enums.HSTRates.HSTMedium);
                        total = CalculateHstTax(rate, value);
                        break;
                    case "NS":
                        rate = ConvertToPercent((int)Enums.HSTRates.HSTHigh);
                        total = CalculateHstTax(rate, value);
                        break;
                    case "NB":
                        rate = ConvertToPercent((int)Enums.HSTRates.HSTMedium);
                        total = CalculateHstTax(rate, value);
                        break;
                    case "ON":
                        rate = ConvertToPercent((int)Enums.HSTRates.HSTMedium);
                        total = CalculateHstTax(rate, value);
                        break;
                    case "BC":
                        rate =  ConvertToPercent((int)Enums.HSTRates.HSTLow);
                        total = CalculateHstTax(rate, value);
                        break;
                    default:
                        break;
                }
            }
            //calculates the PST and GST for the regions that have these types of taxes
            if (taxType.Equals("PST-GST"))
            {
                switch (r.ToUpper())
                {
                    case "PE":
                        rate = gstRate + 0.1d;
                        total = CalculateTax(gstRate, 0.1d, value);
                        break;
                    case "QC":
                        rate = gstRate + 0.095d;
                        total = CalculateTax(gstRate, 0.095d, value);
                        break;
                    case "MB":
                        rate = gstRate + 0.07d;
                        total = CalculateTax(gstRate, 0.07d, value);
                        break;
                    case "SK":
                        rate = gstRate + 0.05d;
                        total = CalculateTax(gstRate, 0.05d, value);
                        break;
                    case "AB":
                        rate = gstRate + 0.0d;
                        total = CalculateTax(gstRate, 0.0d, value);
                        break;
                    case "YT":
                        rate = gstRate + 0.0d;
                        total = CalculateTax(gstRate, 0.0d, value);
                        break;
                    case "NT":
                        rate = gstRate + 0.0d;
                        total = CalculateTax(gstRate, 0.0d, value);
                        break;
                    case "NU":
                        rate = gstRate + 0.0d;
                        total = CalculateTax(gstRate, 0.0d, value);
                        break;
                    default:
                        break;
                }
            }
            return total;
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

        private static double CalculateHstTax(double hst, double value)
        {
            double total = 0.0d;
            double subTotal = hst * value;
            total = subTotal + value;
            return total;
        }

        private static double ConvertToPercent(int PercentAmount)
        {
            const double RATE = 100.00D;
            double result = (PercentAmount / RATE);
            return (PercentAmount / RATE);
        }
    }
}
