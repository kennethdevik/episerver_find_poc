using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using DA_POC.Models.Pages;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAccess;
using EPiServer.ServiceLocation;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;

namespace SideGeneratorer
{
    [TestFixture]
    public class Nyhetsgenerator
    {
        [Test]
        public void TestMethod1()
        {
            var browser = new OpenQA.Selenium.PhantomJS.PhantomJSDriver();

            var dir = Directory.CreateDirectory(@"c:\temp\news");

            for (int i = 0; i < 1000; i++)
            {
                browser.Navigate().GoToUrl("http://webadvice.no/svada");

                var svada = browser.FindElementById("content").FindElement(By.ClassName("wrap"));

                using (var reader = new StringReader(svada.Text))
                {
                    reader.ReadLine(); // header
                    //reader.ReadLine();

                    var title = reader.ReadLine();
                    var ingress = reader.ReadLine();
                    var content = new StringBuilder();

                    var iterations = 0;

                    while (reader.Peek() != -1)
                    {
                        string header = "";
                        reader.ReadLine(); // br
                        if (iterations++ < 5)
                        {
                            header = "<h3>" + reader.ReadLine() + "</h3>";
                        }
                        var paragraph = reader.ReadLine();

                        content.AppendFormat("{0}<p>{1}</p>", header, paragraph);
                    }

                    
                    Console.Out.WriteLine("Title: " + title);
                    Console.Out.WriteLine("Ingress: " + ingress);
                    Console.Out.WriteLine("Body: " + content);

                    using (var writer = new StreamWriter(@"c:\temp\news\news" + i + ".txt"))
                    {
                        writer.WriteLine(title);
                        writer.WriteLine(ingress);
                        writer.WriteLine(content);
                    }

                }
            }

            browser.Close();
        }
    }
}
