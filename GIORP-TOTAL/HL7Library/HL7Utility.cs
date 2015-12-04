using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HL7Library
{
    /// <summary>
    /// This class helps serialization and de-serialization of HL7 messages.
    /// </summary>
    public static class HL7Utility
    {
        public const char BeginOfMessage = (char)11;
        public const char EndOfSegment = (char)13;
        public static string EndOfMessage = ((char)28).ToString() +  ((char)13).ToString();
        public const char ElementDelimiter = '|';
        public const uint MinimumElementDilimiterCount = 2;

        public static string Serialize(Message message)
        {
            StringBuilder serializedMessage = new StringBuilder();
            serializedMessage.Append(BeginOfMessage);

            foreach (var segment in message.Segments)
            {
                serializedMessage.Append(CreateSegment(segment.Elements));
            }
            serializedMessage.Append(EndOfMessage);

            return serializedMessage.ToString();
        }

        public static Message Deserialize(string message)
        {
            if (message == null) { throw new ArgumentNullException(); }
            if (message == "") { throw new ArgumentException("Message cannot be empty."); }
            if (!message.StartsWith(HL7Utility.BeginOfMessage.ToString())) { throw new ArgumentException("Message not in valid format. Begin of message char '" + HL7Utility.BeginOfMessage.ToString() + "' not found."); }
            if (!message.EndsWith(HL7Utility.EndOfMessage.ToString())) { throw new ArgumentException("Message not in valid format. End of message char '" + HL7Utility.EndOfMessage.ToString() + "' not found."); }

            // Get rid of BeginOfMessage and EndOfMessage chars because they are just used by us to be able to parse out the message.
            message = message.Substring(1, message.Length - 3); // -1 (since we are sub-stringing from 1) + -1 (since we do not want to grab last char) = -2.
            // Now we are only left with segments of the message that are separated by EndOfSegment chars. 
            var segments = message.Split(HL7Utility.EndOfSegment).ToList<string>();
            // usually as a side-effect of the Split we end up with extra element that is an empty string.
            if (segments[segments.Count - 1] == "") { segments.RemoveAt(segments.Count - 1); }
            if (segments == null || segments.Count() == 0)
            {
                throw new ArgumentException("Message has to contain at least 1 segment.");
            }
            // remove last if it is empty.
            
            var deserializedMessage = new Message() { Segments = new List<Segment>()};
            try
            {
                foreach (var segment in segments)
                {
                    var segmetnElements = HL7Utility.ParseSegment(segment);
                    deserializedMessage.Segments.Add(new Segment() { Elements = segmetnElements });
                }
            }
            catch (Exception ex)
            {   
                throw new Exception("Segment not in valid format. Check inner exception for details.", ex);
            }

            return deserializedMessage;
        }

        /// <summary>
        /// Creates a segment of a message where each element is separated by ElementDelimiter char, 
        /// and has a EndOfSegemnt constant appended at the end.
        /// </summary>
        /// <param name="elements">elements of a segment</param>
        /// <returns></returns>
        private static string CreateSegment(List<string> elements)
        {
            StringBuilder segemntAsString = new StringBuilder();
            foreach (var element in elements)
            {
                segemntAsString.Append(element);
                segemntAsString.Append(ElementDelimiter);
            }
            segemntAsString.Append(EndOfSegment);

            return segemntAsString.ToString();
        }

        /// <summary>
        /// Creates a segment of a message where each element is separated by pipe char '|' and
        /// has a EndOfSegemnt constant appended at the end.
        /// </summary>
        /// <param name="element1">First element. Cannot be null.</param>
        /// <param name="element2">Second element. Cannot be null.</param>
        /// <param name="element3">Third element.</param>
        /// <param name="element4">Fourth element.</param>
        /// <param name="element5">Fifth element.</param>
        /// <param name="element6">Sixth element.</param>
        /// <param name="element7">Seventh element.</param>
        /// <param name="element8">Eighth element.</param>
        /// <returns></returns>
        private static string CreateSegment(string element1, string element2, string element3 = null, string element4 = null, string element5 = null, string element6 = null, string element7 = null, string element8 = null)
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

            return CreateSegment(elementList);
        }
    
        /// <summary>
        /// Parses a segment into elements. Segment can, but it does not have to contain EndOfSegment.
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        private static List<string> ParseSegment(string segment)
        {
            if (segment == null) { throw new ArgumentNullException(); }
            if (!segment.EndsWith(ElementDelimiter.ToString())) { throw new ArgumentException(string.Format("Segment not in valid format. Valid segment ends with ElementDelimiter '{0}' char.", ElementDelimiter)); }
            if (segment.Where(character => character == ElementDelimiter).Count() < MinimumElementDilimiterCount)
            {
                throw new ArgumentException(string.Format("Segment has to have at least {0} '{1}' delimiters.", MinimumElementDilimiterCount, ElementDelimiter));
            }

            var elements = segment.Split(ElementDelimiter).ToList<string>();

            if (elements[elements.Count - 1] == EndOfSegment.ToString())
            {
                // Last element is our EndOfSegment char, so we remove it.
                elements.RemoveAt(elements.Count - 1);
            }
            if (elements[elements.Count - 1] == "") // usually as a side-effect of the Split we end up with extra element that is an empty string.
            {
                elements.RemoveAt(elements.Count - 1);
            }
            return elements;
        }
    }
}
