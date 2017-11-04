using Microsoft.VisualStudio.TestTools.UnitTesting;
using Social_tap_API.Controllers;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace STapiTests
{
    [TestClass]
    public class TestValuesController
    {
        public List<string> TagsTest = new List<string>();
        public List<string> Tags = new List<string>();
        public Dictionary<string, List<string>> barInfo = new Dictionary<string, List<string>>();
        public Dictionary<string, List<string>> barInfoTests = new Dictionary<string, List<string>>();

        [TestMethod]
        public void Average_Test3()
        {
            ValuesController valuesController = new ValuesController();
            bool test;
            test = valuesController.Average(10);
            test = valuesController.Average(2);
            test = valuesController.Average(4);
            test = valuesController.Average(4);
            test = valuesController.Average(5);
            Assert.IsTrue(test);
        }
        [TestMethod]
        public void Average_Test1()
        {
            ValuesController valuesController = new ValuesController();
            bool test;
            test = valuesController.Average(5);
            test = valuesController.Average(6);
            Assert.IsTrue(test);
        }

        [TestMethod]
        public void Average_Test2()
        {
            ValuesController valuesController = new ValuesController();
            bool test;
            test = valuesController.Average(6);
            test = valuesController.Average(4);

            Assert.IsFalse(test);
        }
        
        [TestMethod]
        public void HashtagsFinder_Test1()
        {
            ValuesController valuesController = new ValuesController();
            TagsTest = valuesController.HashtagsFinder("");
            CollectionAssert.AreEqual(Tags, TagsTest);
        }
        [TestMethod]
        public void HashtagsFinder_Test2()
        {
            ValuesController valuesController = new ValuesController();
            TagsTest = valuesController.HashtagsFinder("Ęgeras Ębaras visai");
            Tags.Add("geras");
            Tags.Add("baras");
            CollectionAssert.AreEqual(Tags, TagsTest);
        }

        [TestMethod]
        public void HashtagsFinder_Test3 ()
        {
            ValuesController valuesController = new ValuesController();
            TagsTest = valuesController.HashtagsFinder("Ęgeras baras visai");
            Tags.Add("geras");
            Tags.Add("baras");
            CollectionAssert.AreNotEqual(Tags, TagsTest);
        }

        [TestMethod]
        public void HashtagsFinder_Test4 ()
        {
            ValuesController valuesController = new ValuesController();
            TagsTest = valuesController.HashtagsFinder("");
            Tags.Add("geras");
            Tags.Add("baras");
            CollectionAssert.AreNotEqual(Tags, TagsTest);
        }


        [TestMethod]
        public void CountBars_Test1()
        {
            ValuesController valuesController = new ValuesController();
            barInfoTests = valuesController.CountBars("busi3", "Ęgeras Ębaras visai");
            Tags.Add("geras");
            Tags.Add("baras");
            barInfo.Add("BUSI3", Tags);

            CollectionAssert.AreEqual(barInfo.Keys, barInfoTests.Keys);
        }

        [TestMethod]
        public void CountBars_Test2()
        {
            ValuesController valuesController = new ValuesController();
            barInfoTests = valuesController.CountBars("busi3", "Ęgeras Ębaras visai");
            Tags.Add("geras"); 
            Tags.Add("baras");
            barInfo.Add("BUSI3", Tags);

            Assert.AreEqual(barInfo.Values.Count, barInfoTests.Values.Count);

        }

        [TestMethod]
        public void CountBars_Test3()
        {
            ValuesController valuesController = new ValuesController();
            barInfoTests = valuesController.CountBars("busi3", "Ęgeras Ębaras visai");
            Tags.Add("geras");
            Tags.Add("baras");
            barInfo.Add("BUSi3", Tags);

            CollectionAssert.AreNotEqual(barInfo.Keys, barInfoTests.Keys);
        }
        [TestMethod]
        public void BarRateAverage_Test1()
        {
            ValuesController valuesController = new ValuesController();
            double test = valuesController.BarRateAverage("busi3", 5);
            test = valuesController.BarRateAverage("busi3", 4);
            test = valuesController.BarRateAverage("snekutis", 4);
            test = valuesController.BarRateAverage("busi3", 3);
            Assert.AreEqual(test, 4);
        }
        [TestMethod]
        public void BarRateAverage_Test2()
        {
            ValuesController valuesController = new ValuesController();
            double test = valuesController.BarRateAverage("busi3", 5);
            test = valuesController.BarRateAverage("busi3", 4);
            double test2 = valuesController.BarRateAverage("snekutis", 4);
            test = valuesController.BarRateAverage("busi3", 3);
            Assert.AreEqual(test2, 4);
        }
        [TestMethod]
        public void BarRateAverage_Test3()
        {
            ValuesController valuesController = new ValuesController();
            double test = valuesController.BarRateAverage("busi3", 5);
            test = valuesController.BarRateAverage("busi3", 4);
            double test2 = valuesController.BarRateAverage("snekutis", 4);
            test = valuesController.BarRateAverage("busi3", 4);
            Assert.AreNotEqual(test, 4);
        }

        [TestMethod]
        public void BarRateAverage_Test4()
        {
            ValuesController valuesController = new ValuesController();
            double test = valuesController.BarRateAverage("7.fridays", 5);
            test = valuesController.BarRateAverage("7-FRIDAYS", 4);
            double test2 = valuesController.BarRateAverage("snekutis", 4);
            test = valuesController.BarRateAverage("7fRi DaYs", 4);
            test = valuesController.BarRateAverage("7 FriDAYS", 3);
            Assert.AreEqual(test, 4);
        }

    }
}
