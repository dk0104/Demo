// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XmlFileManager.cs" company="">
//   
// </copyright>
// <summary>
//   The xml file manager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XmlConnection
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Xml.Linq;
    using System.Xml.Schema;

    using Model;

    using XmlConnection.XmlAccess;
    using XmlConnection.XmlAccess.Decorator;

    /// <summary>
    /// The xml file manager.
    /// </summary>
    public class XmlFileWriter
    {
        //---------------------------------------------------------------------
        #region [Methods]
        //---------------------------------------------------------------------

        /// <summary>
        /// Write order.
        /// </summary>
        /// <param name="order"></param>
        public void WriteOrder(Order order,string filePath)
        {
            XNamespace xsi = XmlSchema.InstanceNamespace;
            var rootElement = new XElement("order", new XAttribute(XNamespace.Xmlns + "xsi", xsi));
            rootElement.Add(new XElement("orderDate",order.DateTime));
            rootElement.Add(new XElement("SerialNumber",order.SerialNumber));
            var productGroup = new XElement("productCroup");
            
            foreach (var pg in order.ProductGroups)
            { 
                productGroup.Add(new XElement("productGroupName", pg.ProductGroupName));
                var products = new XElement("product"); 
                foreach (var p in pg.Products)
                { 
                    var product = new XElement("product");
                    product.Add(new XAttribute("id", p.Id));
                    product.Add(new XElement("productName", p.Name));
                    product.Add(new XElement("productDescription", p.Description));
                    var versionElement = new XElement("version");
                    foreach (var version in p.Versions)
                    {
                        versionElement.Add(new XElement("versionNumber", version.VersionNumber));
                        var featureElements = new XElement("feature");
                       
                        foreach (var feature in version.Features)
                        {

                            featureElements.Add(new XElement("featureName", feature.Name));
                            featureElements.Add(new XElement("featureDescription"), feature.Description); 
                        } 
                      versionElement.Add(featureElements);
                    }
                    product.Add(versionElement);
                    products.Add(products);
                } 
                productGroup.Add(products);
                rootElement.Add(productGroup);
            }
            rootElement.Save(filePath);
        }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------        
        
    }
}