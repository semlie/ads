using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataService;

namespace UnitTestGoogleAds
{
    [TestClass]
    public class TestDataConnection
    {
        [TestMethod]
        public void TestAddNewSeggestAnswer()
        {
            var a = new DataContext();
            //var l = new ();
            var add = new Suggestions.SeggestAnswer()
            {
                SeggestAnswerId = 1,
                Query = "text",
                Answers = new Suggestions.Seggest[] { new Suggestions.Seggest { answer = "aaaa" } }
            };
            a.AddNewSeggestAnswer(add);
            Assert.IsTrue(a.GetAllSeggestAnswer().Contains(add)!=null);


        }
    }
}
