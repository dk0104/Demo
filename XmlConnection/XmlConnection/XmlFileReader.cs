//-----------------------------------------------------------------------
// <brief>
//   XmlFileReader
// </brief>
//
// <author>Denis Keksel</author>
// <since>02.08.2015</since>
//-----------------------------------------------------------------------

namespace XmlConnection
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.Schema;

    using Model;

    using Version = Model.Version;

    /// <summary>
    /// XmlFileReader
    /// </summary>
    public class XmlFileReader 
    {
        //---------------------------------------------------------------------
        #region [Fields]
        //---------------------------------------------------------------------
        
        /// <summary>
        /// 
        /// </summary>
        private XmlReaderSettings settings;

        /// <summary>
        /// Root element
        /// </summary>
        private XElement rootElement;
        
        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Constructors]
        //---------------------------------------------------------------------
        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="filePath"></param>
        public XmlFileReader(string filePath)
        {
            this.LoadXmlFile(filePath);
        }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Properties]
        //---------------------------------------------------------------------
        
        /// <summary>
        /// Gets Reader warnings.
        /// </summary>
        public List<string> XmlReaderWarnings { get; private set; }

        /// <summary>
        /// Gets Reader errors.
        /// </summary>
        public List<string> XmlReaderErrors { get; private set; }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Methods]
        //---------------------------------------------------------------------

        /// <summary>
        /// Return portofolie object.
        /// </summary>
        /// <param name="portfolio"></param>
        public void ReadPortfolio(out Portfolio portfolio)
        {
            var portfolioElement = this.rootElement.Element("portofolio");
            if (portfolioElement!=null)
            {
                portfolio = new Portfolio { CurrentElement = portfolioElement };
                foreach (var productGroupElement in portfolioElement.Elements())
                {
                    ProductGroup pg;
                    CreateProductGroup(productGroupElement,out pg);
                    portfolio.ProductGroups.Add(pg);
                }
            }
            else
            {
                portfolio = null;
            }
        }

        private static void CreateProductGroup(XElement element,out ProductGroup productGroup)
        {
            productGroup = new ProductGroup{ CurrentElement = element }; 
            var productGroupElementName = element.Element("productGroupName");
            if (productGroupElementName != null)
            {
                   productGroup.ProductGroupName = productGroupElementName.Value;
            }

            foreach (var productElement in element.Elements("product"))
            {
                var product = new PortfolioProduct { CurrentElement = productElement };
                productGroup.Products.Add(product);
                var descriptionElement = productElement.Element("productDescription");
                if (descriptionElement != null)
                {
                    product.Description = descriptionElement.Value;
                }
                var nameElement = productElement.Element("productName");
                if (nameElement!=null)
                {
                    product.Name = nameElement.Value;
                }

                foreach (var versionElement in productElement.Elements("version"))
                {
                    var version = new Version { CurrentElement = versionElement };
                    product.Versions.Add(version);
                    var versionNumberElement = versionElement.Element("versionNumber");
                    if (versionNumberElement != null)
                    {
                        version.VersionNumber = versionNumberElement.Value;
                    }
                    foreach (var feautureElement in versionElement.Elements("feature"))
                    {
                        var feauture = new Feature{CurrentElement = feautureElement};
                        version.Features.Add(feauture);
                        var featureElementName = feautureElement.Element("featureName");
                        if (featureElementName != null)
                        {
                            feauture.Name = featureElementName.Value;
                        }
                        var feautureElementDescription = feautureElement.Element("featureDescription");
                        if (feautureElementDescription != null)
                        {
                            feauture.Description = feautureElementDescription.Value;
                        }
                    }
                }
            }
            
        }

        /// <summary>
        /// Load xml filePath.
        /// </summary>
        /// <param name="file">
        /// </param>
        private void LoadXmlFile(string file)
        {
            this.XmlReaderErrors = new List<string>();
            this.XmlReaderWarnings = new List<string>();
            this.settings = new XmlReaderSettings
            {
                ValidationType = ValidationType.Schema,
                ValidationFlags =
                    XmlSchemaValidationFlags.ProcessSchemaLocation
                    | XmlSchemaValidationFlags.ReportValidationWarnings
            };

            var appPath = Environment.CurrentDirectory;
            var validationPath = Path.Combine(appPath, "ValidationSchema", "ProductPortfolioSchema.xsd");
            settings.Schemas.Add(null, validationPath);

            this.settings.ValidationEventHandler += this.OnValidationViolation;
            using (var xmlReader = XmlReader.Create(file, this.settings))
            {
                this.rootElement = XElement.Load(xmlReader);
            }

            if (this.XmlReaderErrors.Count!=0)
            {
                var exceptionMessage= this.XmlReaderErrors.Aggregate(string.Empty, 
                    (current, error) => current + string.Format("Validation error \n {0}", error));
                throw  new XmlSchemaException(exceptionMessage); 
            }
        }

        private void OnValidationViolation(object sender, ValidationEventArgs validationEventArgs)
        {
            switch (validationEventArgs.Severity)
            {
                case XmlSeverityType.Error:
                    {
                        this.XmlReaderErrors.Add(
                            string.Format(
                                "Error Line Number : {0} Message {1}",
                                validationEventArgs.Exception.LineNumber,
                                validationEventArgs.Message));
                    }
                    break;

                case XmlSeverityType.Warning:
                    {
                        this.XmlReaderWarnings.Add(
                            string.Format(
                                "Warning Line Number : {0} Message {1}",
                                validationEventArgs.Exception.LineNumber,
                                validationEventArgs.Message));
                    }
                    break;
            }
        }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------
    }
}