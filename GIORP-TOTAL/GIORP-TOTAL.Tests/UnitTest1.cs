using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GIORP_TOTAL.Tests
{
    [TestClass]
    public class UnitTest1
    {
        #region TaxForProvincesWithOnlyHSTRates
        [TestMethod]
        public void TaxForNewFoundLand()
        {
            var tc = new TaxCalculator();
            double amount = 100.00;
            string provinceCode = "NL";
            var actual = tc.CalculateTax(provinceCode, amount);
            double expected = 113.00;
            Assert.IsTrue(actual.TotalAmount == expected);

            #region uncommented

            //string regionName = string.Empty;
            //regionName = ChooseRegion("NL");
            //string mytest = "NL";
            //string reg = "QC";
            //Console.WriteLine(regionName);

            //CalculateTaxByRegion(reg, 100);

           // Enum myRegions = Enums.Regions.NL;
           // List<float> Pst = new List<float>();
           // List<float> Hst = new List<float>();
           // List<float> Gst = new List<float>();
            //Pst.Add(0.1f);
            //Pst.Add(0.095f);
            //Pst.Add(0.07f);
            //Pst.Add(0.05f);
            //Pst.Add(0.0f);
            //Hst.Add(0.13f);
            //Hst.Add(0.15f);
            //Hst.Add(0.12f);
            //Gst.Add(0.05f);

            //string test = Enum.Parse(typeof(Enums.Regions), mytest).ToString();
            ////string test = Enum.GetName(typeof(Enums.Regions), mytest);
            //if (mytest == Enum.Parse(typeof(Enums.Regions), reg).ToString())
            //{
            //    Console.WriteLine("this is newfoin");
            //}

            //Console.ReadKey();
            #endregion
        }
        [TestMethod]
        public void TaxForNovaScotia()
        {
            var tc = new TaxCalculator();
            double amount = 100.00;
            string provinceCode = "NS";
            var actual = tc.CalculateTax(provinceCode, amount);
            double expected = 115.00;
            Assert.IsTrue(actual.TotalAmount == expected);
        }

        [TestMethod]
        public void TaxForNewBrunswick()
        {
            var tc = new TaxCalculator();
            double amount = 100.00;
            string provinceCode = "NB";
            var actual = tc.CalculateTax(provinceCode, amount);
            double expected = 113.00;
            Assert.IsTrue(actual.TotalAmount == expected);
        }

        [TestMethod]
        public void TaxForOntario()
        {
            var tc = new TaxCalculator();
            double amount = 100.00;
            string provinceCode = "NB";
            var actual = tc.CalculateTax(provinceCode, amount);
            double expected = 113.00;
            Assert.IsTrue(actual.TotalAmount == expected);
        }

        [TestMethod]
        public void TaxForBritishColumbia()
        {
            var tc = new TaxCalculator();
            double amount = 100.00;
            string provinceCode = "BC";
            var actual = tc.CalculateTax(provinceCode, amount);
            double expected = 112.00;
            Assert.IsTrue(actual.TotalAmount == expected);
        }
        #endregion

        #region TaxForRpovincesWithSpecialCases


        #endregion
    }
}
