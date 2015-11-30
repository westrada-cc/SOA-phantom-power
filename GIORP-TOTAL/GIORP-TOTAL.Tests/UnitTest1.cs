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

        #region TaxForProvincesWithSpecialCases

        [TestMethod]
        public void TaxForManitoba()
        {
            var tc = new TaxCalculator();
            double amount = 100.00;
            string provinceCode = "MB";
            var actual = tc.CalculateTax(provinceCode, amount);
            double expected = 112.00;
            Assert.IsTrue(actual.TotalAmount == expected);
        }

        [TestMethod]
        public void TaxForSaskatchewan()
        {
            var tc = new TaxCalculator();
            double amount = 100.00;
            string provinceCode = "SK";
            var actual = tc.CalculateTax(provinceCode, amount);
            double expected = 110.00;
            Assert.IsTrue(actual.TotalAmount == expected);
        }

        [TestMethod]
        public void TaxForAlberta()
        {
            var tc = new TaxCalculator();
            double amount = 100.00;
            string provinceCode = "AB";
            var actual = tc.CalculateTax(provinceCode, amount);
            double expected = 105.00;
            Assert.IsTrue(actual.TotalAmount == expected);
        }

        [TestMethod]
        public void TaxForYukonTerritories()
        {
            var tc = new TaxCalculator();
            double amount = 100.00;
            string provinceCode = "YT";
            var actual = tc.CalculateTax(provinceCode, amount);
            double expected = 105.00;
            Assert.IsTrue(actual.TotalAmount == expected);
        }

        [TestMethod]
        public void TaxForNortwestTerritories()
        {
            var tc = new TaxCalculator();
            double amount = 100.00;
            string provinceCode = "NT";
            var actual = tc.CalculateTax(provinceCode, amount);
            double expected = 105.00;
            Assert.IsTrue(actual.TotalAmount == expected);
        }


        [TestMethod]
        public void TaxForNunavut()
        {
            var tc = new TaxCalculator();
            double amount = 100.00;
            string provinceCode = "NU";
            var actual = tc.CalculateTax(provinceCode, amount);
            double expected = 105.00;
            Assert.IsTrue(actual.TotalAmount == expected);
        }
        #endregion

        #region TaxForSpecialProvinces
        [TestMethod]
        public void TaxForPrinceEdwardIsland()
        {
            var tc = new TaxCalculator();
            double amount = 100.00;
            string provinceCode = "PE";
            var actual = tc.CalculateTax(provinceCode, amount);
            double expected = 115.50;
            Assert.IsTrue(actual.TotalAmount == expected);
        }

        [TestMethod]
        public void TaxForQuebec()
        {
            var tc = new TaxCalculator();
            double amount = 100.00;
            string provinceCode = "QC";
            var actual = tc.CalculateTax(provinceCode, amount);
            double expected = 114.98;
            Assert.IsTrue(Math.Round(actual.TotalAmount, 2) == expected);
        }
        #endregion

        #region HSTTaxCalculation
        [TestMethod]
        public void HstTaxForNewFoundLand()
        {
            var tc = new TaxCalculator();
            double amount = 100.00;
            string provinceCode = "NL";
            var actual = tc.CalculateTax(provinceCode, amount);
            double expected = 13.00;
            Assert.IsTrue(actual.HstAmount == expected);
        }

        public void HstTaxForNovaScotia()
        {
            var tc = new TaxCalculator();
            double amount = 100.00;
            string provinceCode = "NS";
            var actual = tc.CalculateTax(provinceCode, amount);
            double expected = 15.00;
            Assert.IsTrue(actual.HstAmount == expected);
        }

        public void HstTaxForNewBrunswick()
        {
            var tc = new TaxCalculator();
            double amount = 100.00;
            string provinceCode = "NB";
            var actual = tc.CalculateTax(provinceCode, amount);
            double expected = 13.00;
            Assert.IsTrue(actual.HstAmount == expected);
        }

        public void HstTaxForOntario()
        {
            var tc = new TaxCalculator();
            double amount = 100.00;
            string provinceCode = "ON";
            var actual = tc.CalculateTax(provinceCode, amount);
            double expected = 13.00;
            Assert.IsTrue(actual.HstAmount == expected);
        }

        public void HstTaxForBritishColumbia()
        {
            var tc = new TaxCalculator();
            double amount = 100.00;
            string provinceCode = "BC";
            var actual = tc.CalculateTax(provinceCode, amount);
            double expected = 12.00;
            Assert.IsTrue(actual.HstAmount == expected);
        }
        #endregion

        #region GSTTaxCalculation
        [TestMethod]
        public void GstTaxForPrinceEdwardIsland()
        {
            var tc = new TaxCalculator();
            double amount = 100.00;
            string provinceCode = "PE";
            var actual = tc.CalculateTax(provinceCode, amount);
            double expected = 5.00;
            Assert.IsTrue(actual.GstAmount == expected);
        }

        [TestMethod]
        public void GstTaxForQuebec()
        {
            var tc = new TaxCalculator();
            double amount = 100.00;
            string provinceCode = "QC";
            var actual = tc.CalculateTax(provinceCode, amount);
            double expected = 5.00;
            Assert.IsTrue(actual.GstAmount == expected);
        }

        [TestMethod]
        public void GstTaxForManitoba()
        {
            var tc = new TaxCalculator();
            double amount = 100.00;
            string provinceCode = "MB";
            var actual = tc.CalculateTax(provinceCode, amount);
            double expected = 5.00;
            Assert.IsTrue(actual.GstAmount == expected);
        }

        [TestMethod]
        public void GstTaxForSaskatchewan()
        {
            var tc = new TaxCalculator();
            double amount = 100.00;
            string provinceCode = "SK";
            var actual = tc.CalculateTax(provinceCode, amount);
            double expected = 5.00;
            Assert.IsTrue(actual.GstAmount == expected);
        }

        [TestMethod]
        public void GstTaxForAlberta()
        {
            var tc = new TaxCalculator();
            double amount = 100.00;
            string provinceCode = "AB";
            var actual = tc.CalculateTax(provinceCode, amount);
            double expected = 5.00;
            Assert.IsTrue(actual.GstAmount == expected);
        }

        [TestMethod]
        public void GstTaxForYukonTerritories()
        {
            var tc = new TaxCalculator();
            double amount = 100.00;
            string provinceCode = "YT";
            var actual = tc.CalculateTax(provinceCode, amount);
            double expected = 5.00;
            Assert.IsTrue(actual.GstAmount == expected);
        }

        [TestMethod]
        public void GstTaxForNortwestTerritories()
        {
            var tc = new TaxCalculator();
            double amount = 100.00;
            string provinceCode = "NT";
            var actual = tc.CalculateTax(provinceCode, amount);
            double expected = 5.00;
            Assert.IsTrue(actual.GstAmount == expected);
        }

        [TestMethod]
        public void GstTaxForNunavut()
        {
            var tc = new TaxCalculator();
            double amount = 100.00;
            string provinceCode = "NU";
            var actual = tc.CalculateTax(provinceCode, amount);
            double expected = 5.00;
            Assert.IsTrue(actual.GstAmount == expected);
        }

        #endregion

        #region PSTTaxCalculation
        [TestMethod]
        public void PstTaxForPrinceEdwardIsland()
        {
            var tc = new TaxCalculator();
            double amount = 100.00;
            string provinceCode = "PE";
            var actual = tc.CalculateTax(provinceCode, amount);
            double expected = 10.50;
            Assert.IsTrue(actual.PstAmount == expected);
        }

        [TestMethod]
        public void PstTaxForQuebec()
        {
            var tc = new TaxCalculator();
            double amount = 100.00;
            string provinceCode = "QC";
            var actual = tc.CalculateTax(provinceCode, amount);
            double expected = 9.98;
            Assert.IsTrue(Math.Round(actual.PstAmount, 2) == expected);
        }

        [TestMethod]
        public void PstTaxForManitoba()
        {
            var tc = new TaxCalculator();
            double amount = 100.00;
            string provinceCode = "MB";
            var actual = tc.CalculateTax(provinceCode, amount);
            double expected = 7.00;
            Assert.IsTrue(actual.PstAmount == expected);
        }

        [TestMethod]
        public void PstTaxForSaskatchewan()
        {
            var tc = new TaxCalculator();
            double amount = 100.00;
            string provinceCode = "SK";
            var actual = tc.CalculateTax(provinceCode, amount);
            double expected = 5.00;
            Assert.IsTrue(actual.PstAmount == expected);
        }

        [TestMethod]
        public void PstTaxForAlberta()
        {
            var tc = new TaxCalculator();
            double amount = 100.00;
            string provinceCode = "AB";
            var actual = tc.CalculateTax(provinceCode, amount);
            double expected = 0.00;
            Assert.IsTrue(actual.PstAmount == expected);
        }

        [TestMethod]
        public void PstTaxForYukonTerritories()
        {
            var tc = new TaxCalculator();
            double amount = 100.00;
            string provinceCode = "YT";
            var actual = tc.CalculateTax(provinceCode, amount);
            double expected = 0.00;
            Assert.IsTrue(actual.PstAmount == expected);
        }

        [TestMethod]
        public void PstTaxForNortwestTerritories()
        {
            var tc = new TaxCalculator();
            double amount = 100.00;
            string provinceCode = "NT";
            var actual = tc.CalculateTax(provinceCode, amount);
            double expected = 0.00;
            Assert.IsTrue(actual.PstAmount == expected);
        }

        [TestMethod]
        public void PstTaxForNunavut()
        {
            var tc = new TaxCalculator();
            double amount = 100.00;
            string provinceCode = "NU";
            var actual = tc.CalculateTax(provinceCode, amount);
            double expected = 0.00;
            Assert.IsTrue(actual.PstAmount == expected);
        }

        #endregion
    }
}
