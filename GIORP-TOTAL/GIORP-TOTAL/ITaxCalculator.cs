using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIORP_TOTAL
{
    public interface ITaxCalculator
    {
        Models.TaxSummary CalculateTax(string provinceCode, double amount);
    }
}
