using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Suggestions.Engine;

namespace UnitTestTermBuilder
{
    [TestClass]
    public class TestTermBuilder
    {
        [TestMethod]
        public void TestAddNewSeggestAnswer()
        {
            var a = new SearchTermBuilder();
            //var l = new ();
            var b = a.buildSearchTermsFromSeed("מה לקנות");
            Assert.IsTrue(b.Count>0);


        }
    }
}
