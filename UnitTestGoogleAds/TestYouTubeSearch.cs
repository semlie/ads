using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Google.Apis.YouTube.Samples;

namespace UnitTestYouTubeSearch
{
    [TestClass]
    public class TestYouTubeSearch
    {
        [TestMethod]
        public void TestYouTubeSearchAnswer()
        {
           var a = new  SearchYoutube();
          var b =a.Run("מדריך לקניית דירה");
          Assert.IsNotNull(b);
            
            //var a = new DataContext();
            ////var l = new ();
            //var add = new Suggestions.SeggestAnswer()
            //{
            //    SeggestAnswerId = 1,
            //    Query = "text",
            //    Answers = new Suggestions.Seggest[] { new Suggestions.Seggest { answer = "aaaa" } }
            //};
            //a.AddNewSeggestAnswer(add);
            //Assert.IsTrue(a.GetAllSeggestAnswer().Contains(add)!=null);


        }
    }
}
