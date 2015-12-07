using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GIORP_TOTAL.Tests
{
    [TestClass]
    public class Service_UnitTest
    {
        [TestMethod]
        public void CreateRegisterServiceMessage_Normal1()
        {
            var message = Service.CreateRegisterServiceRequest();
            Assert.IsTrue(message != null);
        }
    }
}
