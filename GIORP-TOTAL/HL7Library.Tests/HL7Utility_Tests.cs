using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace HL7Library.Tests
{
    [TestClass]
    public class HL7Utility_Tests
    {
        [TestMethod]
        public void Serialize_Normal1()
        {
            // SETUP //
            var message = new Message()
            {
                Segments = new List<Segment>()
                {
                    new Segment()
                    {
                        Elements = new List<string>()
                        {
                            "SOA", "OK", "", "", "<num segments>"
                        }
                    }
                }
            };
            // TEST //
            var actualResult = HL7Utility.Serialize(message);

            // ASSES //
            string expectedSegmentAsString = HL7Utility.BeginOfMessage + "SOA|OK|||<num segments>|" + HL7Utility.EndOfSegment + HL7Utility.EndOfMessage;
            Assert.IsTrue(actualResult == expectedSegmentAsString);
        }

        [TestMethod]
        public void Serialize_Normal2()
        {
            // SETUP //
            var message = new Message()
            {
                Segments = new List<Segment>()
                {
                    new Segment()
                    {
                        Elements = new List<string>()
                        {
                            "SOA", "OK", "", "", "<num segments>"
                        }
                    },
                    new Segment()
                    {
                        Elements = new List<string>()
                        {
                            "SOA", "OK", "", "", "<num segments>"
                        }
                    }
                }
            };
            // TEST //
            var actualResult = HL7Utility.Serialize(message);

            // ASSES //
            string expectedSegmentAsString = HL7Utility.BeginOfMessage + 
                "SOA|OK|||<num segments>|" + HL7Utility.EndOfSegment +
                "SOA|OK|||<num segments>|" + HL7Utility.EndOfSegment + 
                HL7Utility.EndOfMessage;
            Assert.IsTrue(actualResult == expectedSegmentAsString);
        }

        [TestMethod]
        public void Deserialize_Normal1()
        {
            // SETUP //
            string segmentAsString = HL7Utility.BeginOfMessage + "SOA|OK|||<num segments>|" + HL7Utility.EndOfSegment + HL7Utility.EndOfMessage;
            // TEST //
            var actualResult = HL7Utility.Deserialize(segmentAsString);

            // ASSES //
            
            Assert.IsTrue(actualResult != null && 
                actualResult.Segments != null && 
                actualResult.Segments.Count == 1 && 
                actualResult.Segments[0].Elements.Count == 5 &&
                actualResult.Segments[0].Elements[0] == "SOA" &&
                actualResult.Segments[0].Elements[1] == "OK" &&
                actualResult.Segments[0].Elements[2] == "" &&
                actualResult.Segments[0].Elements[3] == "" &&
                actualResult.Segments[0].Elements[4] == "<num segments>");
        }

        [TestMethod]
        public void Deserialize_Normal2()
        {
            // SETUP //
            string segmentAsString = 
                HL7Utility.BeginOfMessage + 
                "SOA|OK|||<num segments>|" + HL7Utility.EndOfSegment +
                "SOA|OK|OK1|OK2|<num segments>|OK3|" + HL7Utility.EndOfSegment +
                HL7Utility.EndOfMessage;
            // TEST //
            var actualResult = HL7Utility.Deserialize(segmentAsString);

            // ASSES //

            Assert.IsTrue(actualResult != null &&
                actualResult.Segments != null &&
                actualResult.Segments.Count == 2 &&
                actualResult.Segments[0].Elements.Count == 5 &&
                actualResult.Segments[0].Elements[0] == "SOA" &&
                actualResult.Segments[0].Elements[1] == "OK" &&
                actualResult.Segments[0].Elements[2] == "" &&
                actualResult.Segments[0].Elements[3] == "" &&
                actualResult.Segments[0].Elements[4] == "<num segments>" &&
                actualResult.Segments[1].Elements.Count == 6 &&
                actualResult.Segments[1].Elements[0] == "SOA" &&
                actualResult.Segments[1].Elements[1] == "OK" &&
                actualResult.Segments[1].Elements[2] == "OK1" &&
                actualResult.Segments[1].Elements[3] == "OK2" &&
                actualResult.Segments[1].Elements[4] == "<num segments>" &&
                actualResult.Segments[1].Elements[5] == "OK3");
        }
    }
}
