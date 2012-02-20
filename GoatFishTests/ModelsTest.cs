using System.Collections.Generic;
using GoatFish.net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoatFishTests
{
    /// <summary>
    ///This is a test class for ModelsTest and is intended
    ///to contain all ModelsTest Unit Tests
    ///</summary>
    [TestClass]
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
        [TestMethod]
        [DeploymentItem("GoatFish.net.exe")]
        public void InitializeTest()
        {
            
            var model = new Models();
            Assert.AreEqual(true, Models.IsOpen());
        }

        /// <summary>
        ///A test for IsOpen
        ///</summary>
        [TestMethod]
        public void IsOpenTest()
        {
            const bool expected = true; // 
            bool actual = Models.IsOpen();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for OpenConnection
        ///</summary>
        [TestMethod]
        [DeploymentItem("GoatFish.net.exe")]
        public void OpenConnectionTest()
        {
            Assert.AreEqual(true, Models.IsOpen());
        }

        /// <summary>
        ///A test for Save
        ///</summary>
        [TestMethod]
        public void InsertTest()
        {
            var entity = new KeyValuePair<string, string>("key", "value"); // 
            Models.Save(entity);
            Assert.AreEqual(entity, Models.Find("key"));
        }


        /// <summary>
        ///A test for Find
        ///</summary>
        [TestMethod]
        public void FindTest()
        {
            const string uuid = "key";
            var expected = new KeyValuePair<string, string>("key", "value");
            var actual = Models.Find(uuid);
            Models.Save(expected);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for finding non-existant entities.
        ///</summary>
        [TestMethod]
        public void FindNonExistantTest()
        {
            const string uuid = "anonexistantentity";
            var expected = new KeyValuePair<string, string>("empty", "empty");
            Assert.AreEqual(expected, Models.Find("anonexistantentity"));
        }

        /// <summary>
        ///A test for Find All
        ///</summary>
        [TestMethod]
        public void FindAllTest()
        {
            const string uuid = "key";
            const string uuid2 = "another key";
            var key1 = new KeyValuePair<string, string>(uuid, uuid2);
            var key2 = new KeyValuePair<string, string>(uuid2, uuid);
            IDictionary<string, string> expectedDictionary = new Dictionary<string, string>();
            expectedDictionary.Add(key1);
            expectedDictionary.Add(key2);
            Models.Save(uuid, uuid2);
            Models.Save(uuid2, uuid);
            IDictionary<string, string> actual = Models.Find();
            Assert.AreEqual(actual["key"], uuid2);
            Assert.AreEqual(actual["another key"], uuid);
        }

        /// <summary>
        ///A test for Delete
        ///</summary>
        [TestMethod]
        public void DeleteTest()
        {
            const string uuid = "key"; // 
            var empty = new KeyValuePair<string, string>("empty", "empty");
            Models.Delete(uuid);
            Assert.AreEqual(empty, Models.Find(uuid));
        }

        /// <summary>
        ///A test for Deleting a non-existing entity
        ///</summary>
        [TestMethod]
        public void DeleteNullTest()
        {
            const string uuid = "key"; // 
            const string value = "value";
            var empty = new KeyValuePair<string, string>("empty", "empty");
            Models.Save(uuid, value);
            Models.Delete(uuid);
            Assert.AreEqual(empty, Models.Find(uuid));
        }

        /// <summary>
        ///A test for Deleting a non-existing entity
        ///</summary>
        [TestMethod]
        public void DeleteMultipleTest()
        {
            const string uuid = "key"; // 
            const string value = "value";
            var empty = new KeyValuePair<string, string>("empty", "empty");
            Models.Save(uuid, value);
            Models.Save(value, uuid);
            Models.Delete(uuid);
            Models.Delete(value);
            Assert.AreEqual(empty, Models.Find(uuid));
            Assert.AreEqual(empty, Models.Find(value));
        }

        /// <summary>
        ///A test for Update (Save, checks if key exists, then updates key).
        ///</summary>
        [TestMethod]
        public void UpdateTest()
        {
            const string uuid = "key"; // 
            var expected = new KeyValuePair<string, string>("key", "value");
            var inital = new KeyValuePair<string, string>("key", "empty");
            Models.Save(inital);
            Assert.AreEqual(inital, Models.Find(uuid));
            Models.Save(expected);
            Assert.AreEqual(expected, Models.Find(uuid));
        }

        /// <summary>
        ///A test for Save
        ///</summary>
        [TestMethod]
        public void ClearTest()
        {
            const string key = "key";
            const string value = "value";
            var empty = new KeyValuePair<string, string>("empty", "empty");
            Models.Save(key, value);
            Models.Clear();
            Assert.AreEqual(empty, Models.Find(key));
        }

        /// <summary>
        ///A test serializing lists to JSON objects
        ///</summary>
        [TestMethod]
        public void JSONSerializeDeserializeTest()
        {
            const string key = "key";
            var list = new List<string> {"name:", "zach nedwich", "age:", "21"};
            var zachnedwich = new KeyValuePair<string, string>("zach", JSONSerializer.ToJSON(list));
            Models.Save(zachnedwich);
            var zach = JSONSerializer.Deserialize<List<string>>(Models.Find("zach").Value);
            Assert.AreEqual(list[0] + list[1] + list[2] + list[3], zach[0] + zach[1] + zach[2] + zach[3]);
        }

        [TestMethod]
        [ExpectedException(typeof (GoatFishSyntaxException), "Incorrect syntax")]
        public void GoatFishSyntaxExceptionTest()
        {
            Models.Delete("anonexistantentity");
        }
    }
}