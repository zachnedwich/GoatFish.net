using GoatFish.net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace GoatFishTests
{
    
    
    /// <summary>
    ///This is a test class for ModelsTest and is intended
    ///to contain all ModelsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ModelsTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Initialize
        ///</summary>
        [TestMethod()]
        [DeploymentItem("GoatFish.net.exe")]
        public void InitializeTest()
        {
            Models_Accessor.Initialize();
            Models model = new Models();
            Assert.AreEqual(true, Models.IsOpen());
        }

        /// <summary>
        ///A test for IsOpen
        ///</summary>
        [TestMethod()]
        public void IsOpenTest()
        {
            const bool expected = true; // TODO: Initialize to an appropriate value
            bool actual = Models.IsOpen();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for OpenConnection
        ///</summary>
        [TestMethod()]
        [DeploymentItem("GoatFish.net.exe")]
        public void OpenConnectionTest()
        {
           
           Assert.AreEqual(true, Models.IsOpen() );
        }

        /// <summary>
        ///A test for Save
        ///</summary>
        [TestMethod()]
        public void SaveTest()
        {
            var entity = new KeyValuePair<string, string>("key", "value"); // TODO: Initialize to an appropriate value
            Models.Save(entity);
            Assert.AreEqual(entity, Models.Find("key"));
        }

        /// <summary>
        ///A test for Save
        ///</summary>
        [TestMethod()]
        public void SaveTest1()
        {
            const string key = "key";
            const string value = "value";
            Models.Save(key, value);
            Assert.AreEqual(value, Models.Find(key).Value);
        }

        /// <summary>
        ///A test for Find
        ///</summary>
        [TestMethod()]
        public void FindTest()
        {
            const string uuid = "key"; // TODO: Initialize to an appropriate value
            var expected = new KeyValuePair<string, string>("key", "value"); // TODO: Initialize to an appropriate value
            KeyValuePair<string, string> actual = Models.Find(uuid);
            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        ///A test for Delete
        ///</summary>
        [TestMethod()]
        public void DeleteTest()
        {
            const string uuid = "key"; // TODO: Initialize to an appropriate value
            var empty = new KeyValuePair<string, string>("", "");
            Models.Delete(uuid);
            Assert.AreEqual(empty, Models.Find(uuid));
        }

      
    }
}
