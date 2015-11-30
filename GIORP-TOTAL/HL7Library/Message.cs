using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL7Library
{
    /// <summary>
    /// Represents a HL7 message.
    /// </summary>
    public class Message
    {
        public List<Segment> Segments { get; set; }

        public void AddSegment(List<string> elements)
        {
            if (this.Segments == null)
            {
                this.Segments = new List<Segment>();
            }

            this.Segments.Add(new Segment() { Elements = elements });
        }

        public void AddSegment(string element1, string element2, string element3 = null, string element4 = null, string element5 = null, string element6 = null, string element7 = null, string element8 = null, string element9 = null)
        {
            if (element1 == null) { throw new ArgumentNullException("element1"); }
            if (element2 == null) { throw new ArgumentNullException("element2"); }

            var elementList = new List<string>() { element1, element2 };
            if (element3 != null)
            {
                elementList.Add(element3);
            }
            if (element4 != null)
            {
                elementList.Add(element4);
            }
            if (element5 != null)
            {
                elementList.Add(element5);
            }
            if (element6 != null)
            {
                elementList.Add(element6);
            }
            if (element7 != null)
            {
                elementList.Add(element7);
            }
            if (element8 != null)
            {
                elementList.Add(element8);
            }
            if (element9 != null)
            {
                elementList.Add(element9);
            }

            this.AddSegment(elementList);
        }
    }
}
