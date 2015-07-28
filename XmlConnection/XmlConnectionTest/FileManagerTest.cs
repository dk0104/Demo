using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using Model;
using NUnit.Framework;
using XmlConnection;

namespace XmlConnectionTest
{
    [TestFixture]
    public class FileManagerTest
    {
        #region Fields

        private XmlFileManager xmlFileManager;
        private string xmlPath;
        private string xmlPathErrorFile;

		string xmlPathErrorWarningFile;
        private string xmlOrderFile;

        #endregion

        #region SetUp/TearDown

		[SetUp]
		public void SetUp ()
		{
			this.xmlPath = Path.Combine (Environment.CurrentDirectory, "TestData", "ProductPortofolio.xml");
			this.xmlPathErrorFile = Path.Combine (Environment.CurrentDirectory, "TestData", "ProductPortofolioError.xml");
			this.xmlPathErrorWarningFile = Path.Combine (Environment.CurrentDirectory, "TestData", "ProductPortofolioErrorWarning.xml");
            this.xmlOrderFile = Path.Combine(Environment.CurrentDirectory, "TestData", "TestOrder.xml");
		}

		[TearDown]
		public void TearDown ()
		{
            
		}

		#endregion

        #region Test Methods

        [Test]
        public void TestReadAllCountElementsValidationError_ReceiveErrors()
        {
            this.xmlFileManager = new XmlFileManager(xmlPathErrorFile);
            var elements = xmlFileManager.GetAllElements();
            Assert.That(xmlFileManager.XmlReaderWarnings.Count, Is.EqualTo(0));
            Assert.That(xmlFileManager.XmlReaderErrors.Count, Is.EqualTo(1));
            Assert.That(elements.Count, Is.EqualTo(2), "Element count should be 2");
        }

        [Test]
        public void TestReadAllCountElementsValidationErrorWarning()
        {
            this.xmlFileManager = new XmlFileManager(this.xmlPathErrorWarningFile);
            var elements = xmlFileManager.GetAllElements();
            Assert.That(xmlFileManager.XmlReaderWarnings.Count, Is.EqualTo(0));
            Assert.That(xmlFileManager.XmlReaderErrors.Count, Is.EqualTo(1));
           
        }

        [Test]
        public void TestReadAllElements_ValidCount()
        {
            this.xmlFileManager = new XmlFileManager(this.xmlPath);
            var elements = xmlFileManager.GetAllElements();
            Assert.That(elements.Count, Is.EqualTo(2), "Element count should be 2");
        }

        [Test]
        public void TestGetElementById_ValidElement()
        {
            this.xmlFileManager = new XmlFileManager(this.xmlPath);
            var element = xmlFileManager.GetElementById("p2");
            Assert.That(element.FirstAttribute.Value,Is.EqualTo("p2"));
        }

        [Test]
        public void TestGetElementByTagName_ValidElementList()
        {
            this.xmlFileManager = new XmlFileManager(this.xmlPath);
            var elements = xmlFileManager.GetElementsByTagName("version");
        }

        [Test]
        public void TestSaveElement_AddNewElement()
        {
            var product = GreateProductDummy();
            var element = this.CreateProductElement(product);
            this.xmlFileManager = new XmlFileManager(this.xmlOrderFile);
            var elements = xmlFileManager.GetAllElements();
            var prevCount = elements.Count;
            xmlFileManager.Save(element);
            Assert.That(prevCount,Is.Not.GreaterThan(xmlFileManager.GetAllElements().Count));
            xmlFileManager.WriteFile();
        }

       

        #endregion

        #region Helper methods 
        
        private static Product GreateProductDummy()
        {
            var featureA = new Feature
            {
                Description = "feature A Deskription",
                Name = "Feature A",
                IsSelected = true
            };

            var featureB = new Feature
            {
                Description = "feature A Deskription",
                Name = "Feature B",
                IsSelected = true
            };

            var featureC = new Feature
            {
                Description = "feature A Deskription",
                Name = "Feature C",
                IsSelected = true
            };

            var fl = new List<Feature>() {featureC, featureA, featureB};

            var product = new Product
            {
                Id = "PJ1",
                Name = "Product J",
                Description = "Product J Description",
                Features = fl
            };
            return product;
        }

        internal XElement CreateProductElement(Product product)
        {
            var productElement = new XElement("product");
            productElement.Add(new XAttribute("id",product.Id));
            productElement.Add(new XElement("productName",product.Name));
            productElement.Add(new XElement("version",product.Version));
            productElement.Add(new XElement("productDescription",product.Description));
            foreach (var feature in product.Features)
            { 
                var featureEl = new XElement("features"); 
                featureEl.Add(new XElement("featureName",feature.Name));
                featureEl.Add(new XElement("featureDescription",feature.Description));
                featureEl.Add(new XElement("isSelected",feature.IsSelected));
                productElement.Add(featureEl);
            }

            return productElement;
        }

        #endregion
    }

}