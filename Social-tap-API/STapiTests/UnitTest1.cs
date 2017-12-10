using Microsoft.VisualStudio.TestTools.UnitTesting;
using Social_tap_API;
using Social_tap_API.Controllers;
using SocialtapAPI;
using Social_Tap_Api;
using System;

using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Data.Sqlite;
using SocialtapAPI.Migrations;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Z.EntityFramework.Plus;

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
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            Calculations calc = new Calculations();

            try
            {
                var options = new DbContextOptionsBuilder<DatabaseContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new DatabaseContext())
                {
                    context.Database.EnsureCreated();
                }
                using (var db = new DatabaseContext())
                {
                    db.ReviewSet.Add(new Review(5, "abscdefghijk", 5));
                    db.ReviewSet.Add(new Review(5, "abscdefghijk", 5));
                    Assert.IsTrue(calc.Average(300));
                    db.ReviewSet.Where(name => name.Bar.Name == "abscdefghijk")
                        .Delete();
                    db.SaveChanges();
                    
                }  
            }
            finally
            {
                connection.Close();
            }
           }
        [TestMethod]
        public void Average_Test1()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            Calculations calc = new Calculations();

            try
            {
                var options = new DbContextOptionsBuilder<DatabaseContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new DatabaseContext())
                {
                    context.Database.EnsureCreated();
                }
                using (var db = new DatabaseContext())
                {
                    db.ReviewSet.Add(new Review(5, "aa", 5));
                    db.ReviewSet.Add(new Review(5, "aa", 5));
                    
                    Assert.IsTrue(calc.Average(10));
                    db.ReviewSet.Where(name => name.Bar.Name == "aa")
                        .Delete();
                    db.SaveChanges();
                }
            }
            finally
            {
                connection.Close();
            }
        }
           [TestMethod]
           public void Average_Test2()
           {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            Calculations calc = new Calculations();

            try
            {
                var options = new DbContextOptionsBuilder<DatabaseContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new DatabaseContext())
                {
                    context.Database.EnsureCreated();
                }
                using (var db = new DatabaseContext())
                {
                    db.ReviewSet.Add(new Review(5, "aa", 5));
                    db.ReviewSet.Add(new Review(5, "aa", 5));
                    
                    Assert.IsFalse(calc.Average(4));
                    db.ReviewSet.Where(name => name.Bar.Name == "aa")
                        .Delete();
                    db.SaveChanges();
                }
            }
            finally
            {
                connection.Close();
            }
        } 

         [TestMethod]
         public void HashtagsFinder_Test1()
         {
             var calc = new Calculations();
             TagsTest = calc.HashtagsFinder("");
             CollectionAssert.AreEqual(Tags, TagsTest);
         }
         [TestMethod]
         public void HashtagsFinder_Test2()
         {
             var calc = new Calculations();
             TagsTest = calc.HashtagsFinder("Ęgeras Ębaras visai");
             Tags.Add("geras");
             Tags.Add("baras");
             CollectionAssert.AreEqual(Tags, TagsTest);
         }

         [TestMethod]
         public void HashtagsFinder_Test3()
         {
             var calc = new Calculations();
             TagsTest = calc.HashtagsFinder("Ęgeras baras visai");
             Tags.Add("geras");
             Tags.Add("baras");
             CollectionAssert.AreNotEqual(Tags, TagsTest);
         }

         [TestMethod]
         public void HashtagsFinder_Test4()
         {
             var calc = new Calculations();
             TagsTest = calc.HashtagsFinder("");
             Tags.Add("geras");
             Tags.Add("baras");
             CollectionAssert.AreNotEqual(Tags, TagsTest);
         }

         [TestMethod]
         public void BarRateAverage_Test1()
         {

            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            Calculations calc = new Calculations();

            try
            {
                var options = new DbContextOptionsBuilder<DatabaseContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new DatabaseContext())
                {
                    context.Database.EnsureCreated();
                }
                using (var db = new DatabaseContext())
                {
                    db.ReviewSet.Add(new Review(5, "aaaaa", 5));
                    db.ReviewSet.Add(new Review(5, "aaaaa", 5));
                    db.ReviewSet.Add(new Review(5, "aaaaa", 5));
                    db.SaveChanges();
                    Assert.AreEqual(5, calc.BarRateAverage("aaaaa"));
                }
            }
            finally
            {
                connection.Close();
            }
        }
        [TestMethod]
        public void BarRateAverage_Test2()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            Calculations calc = new Calculations();

            try
            {
                var options = new DbContextOptionsBuilder<DatabaseContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new DatabaseContext())
                {
                    context.Database.EnsureCreated();
                }
                using (var db = new DatabaseContext())
                {
                    db.ReviewSet.Add(new Review(5, "bb", 5));
                    db.ReviewSet.Add(new Review(5, "bb", 5));
                    db.ReviewSet.Add(new Review(2, "bb", 5));
                    
                    Assert.AreEqual(4, calc.BarRateAverage("bb"));
                    db.ReviewSet.Where(name => name.Bar.Name == "bb")
                        .Delete();
                    db.SaveChanges();
                }
            }
            finally
            {
                connection.Close();
            }
        }
        [TestMethod]
        public void BarRateAverage_Test3()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            Calculations calc = new Calculations();

            try
            {
                var options = new DbContextOptionsBuilder<DatabaseContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new DatabaseContext())
                {
                    context.Database.EnsureCreated();
                }
                using (var db = new DatabaseContext())
                {
                    db.ReviewSet.Add(new Review(5, "ccc", 5));
                    db.ReviewSet.Add(new Review(5, "ccc", 5));
                    db.ReviewSet.Add(new Review(0, "ccc", 5));
                    
                    Assert.AreNotEqual(0, calc.BarRateAverage("ccc"));
                    db.ReviewSet.Where(name => name.Bar.Name == "ccc")
                        .Delete();
                    db.SaveChanges();
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [TestMethod]
        public void BarRateAverage_Test4()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            Calculations calc = new Calculations();

            try
            {
                var options = new DbContextOptionsBuilder<DatabaseContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new DatabaseContext())
                {
                    context.Database.EnsureCreated();
                }
                using (var db = new DatabaseContext())
                {
                    db.ReviewSet.Add(new Review(5, "e", 5));
                    db.ReviewSet.Add(new Review(5, "e", 5));
                    db.ReviewSet.Add(new Review(2, "e", 5));
                    db.ReviewSet.Add(new Review(5, "d", 5));
                    db.ReviewSet.Add(new Review(5, "d", 5));
                    db.ReviewSet.Add(new Review(2, "d", 5));
                    
                    Assert.AreEqual(calc.BarRateAverage("d"), calc.BarRateAverage("e"));
                    db.ReviewSet.Where(name => name.Bar.Name == "d" || name.Bar.Name=="e")
                        .Delete();
                    db.SaveChanges();
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [TestMethod] 
        public void AddBarReview_Test1 ()
        {
            var valuesController = new ValuesController();
            bool test = valuesController.AddBarReview("", "", 4, 10);
            Assert.IsFalse(test);
        }
        [TestMethod]
        public void AddBarReview_Test2 ()
        {
            var valuesController = new ValuesController();
            bool test = valuesController.AddBarReview("busi3", "", 6, 10);
            Assert.IsFalse(test);
        }
        [TestMethod]
        public void AddBarReview_Test3()
        {
            var valuesController = new ValuesController();
            bool test = valuesController.AddBarReview("busi3", "", 4, 11);
            Assert.IsFalse(test);
        }
        [TestMethod]
        public void AddBarReview_Test4()
        {
            var valuesController = new ValuesController();
            bool test = valuesController.AddBarReview("busi3", "", 4, 10);
            Assert.IsTrue(test);
        }

        [TestMethod]
        public void AddBarReview_Test5()
        {
            var valuesController = new ValuesController();
            bool test = valuesController.AddBarReview("busi3", "", -1,6);
            Assert.IsFalse(test);
        }
        [TestMethod] 
        public void barNameAdaptation_Test1()
        {
            var calc = new Calculations();
            string barName = calc.BarNameAdaptation("Busi 3");
            Assert.AreEqual("Busi3", barName);
        }

        [TestMethod]
        public void barNameAdaptation_Test2()
        {
            var calc = new Calculations();
            string barName = calc.BarNameAdaptation("BUSI 3");
            Assert.AreEqual("Busi3", barName);
        }

        [TestMethod]
        public void barNameAdaptation_Test3()
        {
            var calc = new Calculations();
            string barName = calc.BarNameAdaptation("BUSI-3");
            Assert.AreEqual("Busi3", barName);
        }

        [TestMethod]
        public void barNameAdaptation_Test4()
        {
            var calc = new Calculations();
            string barName = calc.BarNameAdaptation("busi.3");
            Assert.AreEqual("Busi3", barName);
        }

        [TestMethod]
        public void barNameAdaptation_Test5()
        {
            var calc = new Calculations();
            string barName = calc.BarNameAdaptation("7Fridays");
            Assert.AreNotEqual("7Fridays", barName);
        }


        [TestMethod]
        public void Validation_Test1()
        {
            var calc = new Calculations();
            bool test = calc.Validation(5, 6, "");
            Assert.IsFalse(test);
        }

        [TestMethod]
        public void Validation_Test2()
        {
            var calc = new Calculations();
            bool test = calc.Validation(-2, 6, "");
            Assert.IsFalse(test);
        }

        [TestMethod]
        public void Validation_Test3()
        {
            var calc = new Calculations();
            bool test = calc.Validation(0, 0, "a");
            Assert.IsTrue(test);
        }

        [TestMethod]
        public void IsBarNew_Test1()
        {
            var calc = new Calculations();
            bool test = calc.IsBarNew("aaa");
            Assert.IsFalse(test);
        }

        [TestMethod]
        public void IsBarNew_Test2()
        {
            var calc = new Calculations();
            bool test = calc.IsBarNew("123");
            test = calc.IsBarNew("123");
            Assert.IsTrue(test);
        }

        [TestMethod]
        public void BestBarRate_Test1 ()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            Calculations calc = new Calculations();

            try
            {
                var options = new DbContextOptionsBuilder<DatabaseContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new DatabaseContext())
                {
                    context.Database.EnsureCreated();
                }
                using (var db = new DatabaseContext())
                {
                    db.ReviewSet.Add(new Review(5, "y", 5));
                    db.ReviewSet.Add(new Review(5, "y", 5));
                    db.ReviewSet.Add(new Review(2, "y", 5));
                    db.ReviewSet.Add(new Review(5, "55", 5));                    
                    Assert.AreNotEqual("55", calc.BestBarRate());
                    db.ReviewSet.Where(name => name.Bar.Name == "55" || name.Bar.Name == "y")
                        .Delete();
                    db.SaveChanges();
                }
            }
            finally
            {             
                connection.Close();
            }
        }

        [TestMethod]
        public void BestBarRate_Test2()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            Calculations calc = new Calculations();

            try
            {
                var options = new DbContextOptionsBuilder<DatabaseContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new DatabaseContext())
                {
                    context.Database.EnsureCreated();
                }
                using (var db = new DatabaseContext())
                {
                    db.ReviewSet.Add(new Review(7, "xd", 5));
                    db.ReviewSet.Add(new Review(8, "xdd", 5));
                    db.ReviewSet.Add(new Review(10, "xd", 5));
                    db.ReviewSet.Add(new Review(30, "xd", 5));
                    
                    string test = calc.BestBarRate();
                    Assert.AreEqual("xd", test);
                    db.ReviewSet.Where(name => name.Bar.Name == "xd" || name.Bar.Name == "xdd")
                        .Delete();
                    db.SaveChanges();
                }
            }
            finally
            {
                connection.Close();
            }
            
        }
        [TestMethod]
        public void BestBarRate_Test3()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            Calculations calc = new Calculations();

            try
            {
                var options = new DbContextOptionsBuilder<DatabaseContext>()
                    .UseSqlite(connection)
                    .Options;

                // Create the schema in the database
                using (var context = new DatabaseContext())
                {
                    context.Database.EnsureCreated();
                }
                using (var db = new DatabaseContext())
                {
                    db.ReviewSet.Add(new Review(7, "xd", 5));
                    db.ReviewSet.Add(new Review(8, "xdd", 5));
                    db.ReviewSet.Add(new Review(10, "xd", 5));
                    db.ReviewSet.Add(new Review(10, "xd", 5));
                    
                    string test = calc.BestBarRate();
                    Assert.AreNotEqual("xdd", test);
                    db.ReviewSet.Where(name => name.Bar.Name == "xd" || name.Bar.Name == "xdd")
                        .Delete();
                    db.SaveChanges();
                }
            }
            finally
            {
                connection.Close();
            }
        }

    }
}
