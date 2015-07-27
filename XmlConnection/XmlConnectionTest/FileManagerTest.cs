using System;
using System.IO;
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
        private string xmlPathWarning;

        #endregion
        [SetUp]
        public void SetUp()
        {
            this.xmlPath = Path.Combine(Environment.CurrentDirectory, "TestData", "ProductPortofolio.xml");
            this.xmlPathWarning = Path.Combine(Environment.CurrentDirectory, "TestData", "ProductPortofolioWarning.xml");
         
        }

        [TearDown]
        public void TearDown()
        {
            
        }

        [Test]
        public void TestReadAllCountElementsError()
        {   this.xmlFileManager = new XmlFileManager(xmlPathWarning);
            var elements = xmlFileManager.ReadAllElements();
            Assert.That(xmlFileManager.XmlReaderWarnings.Count,Is.EqualTo(0));
            Assert.That(xmlFileManager.XmlReaderErrors.Count,Is.EqualTo(1));
            Assert.That(elements.Count,Is.EqualTo(2),"Element count should be 2");
            
        }

        [Test]    
        public void TestReadOneCheckValue()
        {
            var elements = xmlFileManager.ReadNamedElements(0001);
        }
    }

}
