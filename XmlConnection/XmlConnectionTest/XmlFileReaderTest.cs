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
    public class XmlFileReaderTest
    {
        #region Fields
        
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

        private XmlFileReader xmlReader;

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
            
        }

        /// <summary>
        /// The test read all count elements validation error warning.
        /// </summary>
        [Test]
        public void TestReadAllCountElementsValidationErrorWarning()
        {
            
        }

        /// <summary>
        /// The test read all elements_ valid count.
        /// </summary>
        [Test]
        public void TestReadPortofolioValidElementCount()
        {
            this.xmlReader = new XmlFileReader(this.xmlPath);
            Portofolio portofolio;
            xmlReader.ReadPortofolio(out portofolio);
            Assert.That(portofolio.ProductGroups.Count,Is.EqualTo(2));
        }

        /// <summary>
        /// The test get element by id valid element.
        /// </summary>
        [Test]
        public void TestGetElementByIdValidElement()
        {
            
        }

        [Test]
        public void GetProductGroupByProductElement()
        {
            
        }

        /// <summary>
        /// The test get element by tag name_ valid element list.
        /// </summary>
        [Test]
        public void TestGetElementByTagNameValidElementList()
        {
           
        }

        /// <summary>
        /// The test save element_ add new element.
        /// </summary>
        [Test]
        public void TestSaveElementAddNewElement()
        {
           
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
        private static XElement CreateProductElement(PortofolioProduct product)
        {
            var productElement = new XElement("product");
            productElement.Add(new XAttribute("id", product.Id));
            productElement.Add(new XElement("productName", product.Name));
            productElement.Add(new XElement("productDescription", product.Description));
            foreach (var version in product.Versions)
            {
                var versionEl = new XElement("version");
                versionEl.Add("versionNumber",version.VersionNumber);
                foreach (var feature in version.Features)
                {
                    versionEl.Add("featureName",feature.Name);
                    versionEl.Add("featureDescription",feature.Description);
                }
               productElement.Add(versionEl);
            }
            return productElement;
        }

        /// <summary>
        /// The greate product dummy.
        /// </summary>
        /// <returns>
        /// The <see cref="PortofolioProduct"/>.
        /// </returns>
        private static PortofolioProduct GreateProductDummy()
        {
            var featureA = new Feature { Description = "feature A Deskription", Name = "Feature A" };

            var featureB = new Feature { Description = "feature A Deskription", Name = "Feature B" };

            var featureC = new Feature { Description = "feature A Deskription", Name = "Feature C" };

            var fl = new List<Feature> { featureC, featureA, featureB };

            var product = new PortofolioProduct
                              {
                                  Id = "PJ1", 
                                  Name = "Product J", 
                                  Description = "Product J Description", 
                                  
                              };
            return product;
        }

        #endregion
    }
}