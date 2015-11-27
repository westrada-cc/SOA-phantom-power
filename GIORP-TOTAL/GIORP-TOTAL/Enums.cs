using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIORP_TOTAL
{
    public class Enums
    {
        //defines codes for each of the provinces 
        public enum Regions
        {
            NL = 1,
            NS,
            NB,
            PE,
            QC,
            ON,
            MB,
            SK,
            AB,
            BC,
            YT,
            NT,
            NU
        }

        //defines types of taxes we need to calculate
        public enum Taxes
        {
            PST = 1,
            HST = 2,
            GST = 3
        }

        //defines rates for HST
        public enum HSTRates
        {
            HSTHigh = 15,
            HSTMedium = 13,
            HSTLow = 12
        }

        //defines rates for PST
        public enum PSTRates
        {
            PSTZero = 0,
            PSTLow = 5,
            PSTMedium = 7,
            PSTHigh = 10,
        }

        //defines rates for GST
        public enum GSTRate
        {
            GSTRate = 5
        }
    }
}
