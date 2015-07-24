using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Google.Apis.Customsearch.v1.Data;
using TestGoogleSearch;
using System.Net.Http;
using googleSherchApi;

namespace UnitTestTestGoogleSearch
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {



            var client = new GoogleSearch1();

            var go = new GoogleSearchParser();
            var rv = go.SearchQuery("מדריך לחיפוש דירה");
           var b = client.MyProperty();
            client.GetAsync("");

        }
    }
}
