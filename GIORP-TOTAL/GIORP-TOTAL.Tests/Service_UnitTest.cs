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
            var message = Service.CreateRegisterServiceMessage();
            Assert.IsTrue(message != null);
        }
    }
}
