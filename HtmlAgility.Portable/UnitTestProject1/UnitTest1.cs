using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HtmlAgilityPack;
using System.Net;
using System.IO;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var request = HttpWebRequest.CreateHttp("http://www.leboncoin.fr/annonces/offres/ile_de_france/");
            using (var response = request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(stream))
                    {
                        var content = reader.ReadToEnd();
                        var doc = new HtmlDocument();
                        doc.LoadHtml(content);
                        System.Console.WriteLine(doc.DocumentNode.InnerText);
                    }
                }
            }
        }
    }
}
