using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Xml.Linq;
using NUnit.Framework;
using XmlConnection;

namespace XmlConnectionTest
{
    [TestFixture]
    public class FileManagerTest
    {
        #region Properties

        private XmlFileManager xmlFileManager;
        private string xmlPath;

        #endregion
        [SetUp]
        public void SetUp()
        {
            this.xmlPath = Path.Combine(Environment.CurrentDirectory, "TestData", "ProductPortofolio.xml");
            this.xmlFileManager = new XmlFileManager(xmlPath);
        }

        [TearDown]
        public void TearDown()
        {
            
        }

        [Test]
        public void TestReadAllCountElements()
        {
            var elements = xmlFileManager.ReadAllElements();
            Assert.That(xmlFileManager.XmlReaderWarnings.Count,Is.EqualTo(0));
            Assert.That(xmlFileManager.XmlReaderErrors.Count,Is.EqualTo(0));
            Assert.That(elements.Count,Is.EqualTo(2),"Element count should be 2");
            
        }

        [Test]    
        public void TestReadOneCheckValue()
        {
            var elements = xmlFileManager.ReadNamedElements("features");
        }
    }

}
