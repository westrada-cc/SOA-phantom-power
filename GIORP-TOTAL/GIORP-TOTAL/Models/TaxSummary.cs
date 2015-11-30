using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIORP_TOTAL.Models
{
    public class TaxSummary
    {
        public double NetAmount { get; set; }

        public double PstAmount { get; set; }

        public double HstAmount { get; set; }

        public double GstAmount { get; set; }

        public double TotalAmount { get; set; }
    }
}
