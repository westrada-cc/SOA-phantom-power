﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIORP_TOTAL
{
    public class Enums
    {
        public enum Regions
        {
            NL = 1,
            NS = 2,
            NB = 3,
            PE = 4,
            QC = 5,
            ON = 6,
            MB = 7,
            SK = 8,
            AB = 9,
            BC = 10,
            YT = 11,
            NT = 12,
            NU = 13
        }


        public enum Taxes
        {
            PST = 1,
            HST = 2,
            GST = 3
        }

        public enum HSTRates
        {
            HSTHigh = 15,
            HSTMedium = 13,
            HSTLow = 12
        }

        public enum PSTRates
        {
            PSTZero = 0,
            PSTLow = 5,
            PSTMedium = 7,
            PSTHigh = 10,
        }

        public enum GSTRate
        {
            GSTRate = 5
        }
    }
}
