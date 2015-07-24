using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataService;
using Suggestions;
using System.Threading.Tasks;


namespace UnitTestGoogleAds
{
    [TestClass]
    public class TestSearchEngien
    {
        [TestMethod]
        public void TestDosearchQuery()
        {
            var a = new GoogleSuggest();
            var l = a.GetSearchSuggestions("איך להפעיל ").Result;

            var b = new DataContext();
            //var l = new ();
            var c = b.GetAllAnswer();
            b.AddNewSeggestAnswer(l);
            var test = b.GetAllSeggestAnswer();
                Assert.IsTrue(test.Contains(l) != null);

            Assert.IsTrue(true);


        }
    }
}
