using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL7Library
{
    /// <summary>
    /// Represents a HL7 segment in a message.
    /// </summary>
    public class Segment
    {
        public List<string> Elements { get; set; }
    }
}
