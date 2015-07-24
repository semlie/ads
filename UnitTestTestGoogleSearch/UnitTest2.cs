using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ArticlesBuilder;
using Wordpress;

namespace UnitTestTestGoogleSearch
{
    [TestClass]
    public class UnitTestWordpress
    {
        [TestMethod]
        public void TestAddPost()
        {
            var a = new PostBuilder();
            var b = a.addPage();
            Assert.IsNotNull(b);
            var c = a.addPost();
            Assert.IsNotNull(c);
        }

    }
}
