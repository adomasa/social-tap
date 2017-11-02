using Microsoft.VisualStudio.TestTools.UnitTesting;
using Social_tap_API.Controllers;
using System;
using System.Collections.Generic;

namespace APItests
{
    [TestClass]
    public class TestValuesController
    {
        public List<string> TagsTest = new List<string>();
        public List<string> Tags = new List<string>();
        public Dictionary<string, List<string>> barInfo = new Dictionary<string, List<string>>();
        public Dictionary<string, List<string>> barInfoTests = new Dictionary<string, List<string>>();
        [TestMethod]
        public void Average_Test1()
        {
            ValuesController valuesController = new ValuesController();
            bool test;
            test= valuesController.Average(5);
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
            TagsTest = valuesController.HashtagsFinder("Ęgeras Ębaras visai");
            Tags.Add("geras");
            Tags.Add("baras");
            CollectionAssert.AreEqual(Tags, TagsTest);
        }

        [TestMethod]
        public void HashtagsFinder_Test2()
        {
            ValuesController valuesController = new ValuesController();
            TagsTest = valuesController.HashtagsFinder("Ęgeras baras visai");
            Tags.Add("geras");
            Tags.Add("baras");
            CollectionAssert.AreNotEqual(Tags, TagsTest);
        }

        [TestMethod]
        public void HashtagsFinder_Test3()
        {
            ValuesController valuesController = new ValuesController();
            TagsTest = valuesController.HashtagsFinder("");
            Tags.Add("geras");
            Tags.Add("baras");
            CollectionAssert.AreNotEqual(Tags, TagsTest);
        }

        [TestMethod]
        public void HashtagsFinder_Test4()
        {
            ValuesController valuesController = new ValuesController();
            TagsTest = valuesController.HashtagsFinder("");
            CollectionAssert.AreEqual(Tags, TagsTest);
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

    }
}
