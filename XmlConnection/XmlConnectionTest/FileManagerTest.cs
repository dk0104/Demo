// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileManagerTest.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the FileManagerTest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace XmlConnectionTest
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Linq;

    using Model;

    using NUnit.Framework;

    using XmlConnection;

    /// <summary>
    /// The file manager test.
    /// </summary>
    [TestFixture]
    public class FileManagerTest
    {
        #region Fields

        /// <summary>
        /// The xml file manager.
        /// </summary>
        private XmlFileManager xmlFileManager;

        /// <summary>
        /// The xml path.
        /// </summary>
        private string xmlPath;

        /// <summary>
        /// The xml path error file.
        /// </summary>
        private string xmlPathErrorFile;

        /// <summary>
        /// The xml path error warning file.
        /// </summary>
        private string xmlPathErrorWarningFile;

        /// <summary>
        /// The xml order file.
        /// </summary>
        private string xmlOrderFile;

        #endregion

        #region SetUp/TearDown

        /// <summary>
        /// The set up.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.xmlPath = Path.Combine(Environment.CurrentDirectory, "TestData", "ProductPortofolio.xml");
            this.xmlPathErrorFile = Path.Combine(Environment.CurrentDirectory, "TestData", "ProductPortofolioError.xml");
            this.xmlPathErrorWarningFile = Path.Combine(
                Environment.CurrentDirectory, 
                "TestData", 
                "ProductPortofolioErrorWarning.xml");
            this.xmlOrderFile = Path.Combine(Environment.CurrentDirectory, "TestData", "TestOrder.xml");
        }

        /// <summary>
        /// The tear down.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
        }

        #endregion

        #region Test Methods

        /// <summary>
        /// The test read all count elements validation error_ receive errors.
        /// </summary>
        [Test]
        public void TestReadAllCountElementsValidationErrorReceiveErrors()
        {
            this.xmlFileManager = new XmlFileManager(this.xmlPathErrorFile);
            var elements = this.xmlFileManager.GetAllElements();
            Assert.That(this.xmlFileManager.XmlReaderWarnings.Count, Is.EqualTo(0));
            Assert.That(this.xmlFileManager.XmlReaderErrors.Count, Is.EqualTo(1));
            Assert.That(elements.Count, Is.EqualTo(2), "Element count should be 2");
        }

        /// <summary>
        /// The test read all count elements validation error warning.
        /// </summary>
        [Test]
        public void TestReadAllCountElementsValidationErrorWarning()
        {
            this.xmlFileManager = new XmlFileManager(this.xmlPathErrorWarningFile);
            this.xmlFileManager.GetAllElements();
            Assert.That(this.xmlFileManager.XmlReaderWarnings.Count, Is.EqualTo(0));
            Assert.That(this.xmlFileManager.XmlReaderErrors.Count, Is.EqualTo(1));
        }

        /// <summary>
        /// The test read all elements_ valid count.
        /// </summary>
        [Test]
        public void TestReadAllElementsValidCount()
        {
            this.xmlFileManager = new XmlFileManager(this.xmlPath);
            var elements = this.xmlFileManager.GetAllElements();
            Assert.That(elements.Count, Is.EqualTo(2), "Element count should be 2");
        }

        /// <summary>
        /// The test get element by id valid element.
        /// </summary>
        [Test]
        public void TestGetElementByIdValidElement()
        {
            this.xmlFileManager = new XmlFileManager(this.xmlPath);
            var element = this.xmlFileManager.GetElementById("p2");
            Assert.That(element.FirstAttribute.Value, Is.EqualTo("p2"));
        }

        /// <summary>
        /// The test get element by tag name_ valid element list.
        /// </summary>
        [Test]
        public void TestGetElementByTagNameValidElementList()
        {
            this.xmlFileManager = new XmlFileManager(this.xmlPath);
            var elements = this.xmlFileManager.GetElementsByTagName("version");
        }

        /// <summary>
        /// The test save element_ add new element.
        /// </summary>
        [Test]
        public void TestSaveElementAddNewElement()
        {
            var product = GreateProductDummy();
            var element = this.CreateProductElement(product);
            this.xmlFileManager = new XmlFileManager(this.xmlOrderFile);
            var elements = this.xmlFileManager.GetAllElements();
            var prevCount = elements.Count;
            this.xmlFileManager.Save(element);
            Assert.That(prevCount, Is.Not.GreaterThan(this.xmlFileManager.GetAllElements().Count));
            this.xmlFileManager.WriteFile();
        }

        #endregion

        #region Helper methods 

        /// <summary>
        /// The create product element.
        /// </summary>
        /// <param name="product">
        /// The product.
        /// </param>
        /// <returns>
        /// The <see cref="XElement"/>.
        /// </returns>
        internal XElement CreateProductElement(Product product)
        {
            var productElement = new XElement("product");
            productElement.Add(new XAttribute("id", product.Id));
            productElement.Add(new XElement("productName", product.Name));
            productElement.Add(new XElement("version", product.Version));
            productElement.Add(new XElement("productDescription", product.Description));
            foreach (var feature in product.Features)
            {
                var featureEl = new XElement("features");
                featureEl.Add(new XElement("featureName", feature.Name));
                featureEl.Add(new XElement("featureDescription", feature.Description));
                featureEl.Add(new XElement("isSelected", feature.IsSelected));
                productElement.Add(featureEl);
            }

            return productElement;
        }

        /// <summary>
        /// The greate product dummy.
        /// </summary>
        /// <returns>
        /// The <see cref="Product"/>.
        /// </returns>
        private static Product GreateProductDummy()
        {
            var featureA = new Feature { Description = "feature A Deskription", Name = "Feature A", IsSelected = true };

            var featureB = new Feature { Description = "feature A Deskription", Name = "Feature B", IsSelected = true };

            var featureC = new Feature { Description = "feature A Deskription", Name = "Feature C", IsSelected = true };

            var fl = new List<Feature> { featureC, featureA, featureB };

            var product = new Product
                              {
                                  Id = "PJ1", 
                                  Name = "Product J", 
                                  Description = "Product J Description", 
                                  Features = fl
                              };
            return product;
        }

        #endregion
    }
}