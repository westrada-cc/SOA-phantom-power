using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL7Library
{
    public enum DataType
    {
        [Description("char")]
        Char,
        [Description("short")]
        Short,
        [Description("int")]
        Int,
        [Description("long")]
        Long,
        [Description("float")]
        Float,
        [Description("double")]
        Double,
        [Description("string")]
        String 
    }
}
