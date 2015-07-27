using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
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
            this.xmlPath = Path.Combine(Environment.CurrentDirectory, "TestData", "DefaultFile.xml");
            this.xmlFileManager = new XmlFileManager(xmlPath);
        }

        [TearDown]
        public void TearDown()
        {
            
        }

        [Test]
        public void TestReadAllCountElements()
        {
            var elements = xmlFileManager.ReadAllElements(this.xmlPath);
            Assert.That(elements.Count,Is.EqualTo(4),"Element count should be 4");
        }

        [Test]    
        public void TestReadOneCheckValue()
        {
            var element = xmlFileManager.ReadNamedElements("1000");
            var name = element.Elements().FirstOrDefault(x => x.Name == "name");
            Assert.That(name,Is.Not.Null);
            Assert.That(name.Value,Is.EqualTo("Product A"),"Value should be equal to Product A");
        }

        [Test]
        public void TestUpdateElementChangeVersion()
        {
            xmlFileManager.UpdateElementVersion("1000","2.0");
            var element = xmlFileManager.ReadNamedElements("1000");
            var versionElement = element.Elements().FirstOrDefault(x => x.Name == "version");
            Assert.That(versionElement,Is.Not.Null, "versionElement != null");
            Assert.That(versionElement.Value,Is.EqualTo("2.0"));
        }
    }

}
