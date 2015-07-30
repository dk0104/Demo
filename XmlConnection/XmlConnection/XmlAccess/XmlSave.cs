// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XmlSave.cs" company="">
//   
// </copyright>
// <summary>
//   The xml save.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XmlConnection.XmlAccess
{
    using System.Linq;
    using System.Xml.Linq;

    using XmlConnection.Interfaces;

    /// <summary>
    /// The xml save.
    /// </summary>
    internal class XmlSave : ISave<XElement>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XmlSave"/> class.
        /// </summary>
        /// <param name="rootElement">
        /// The root element.
        /// </param>
        public XmlSave(XElement rootElement)
        {
            this.RootElement = rootElement;
        }

        /// <summary>
        /// Gets the root element.
        /// </summary>
        public XElement RootElement { get; private set; }

        /// <summary>
        /// The save.
        /// </summary>
        /// <param name="element">
        /// The element.
        /// </param>
        public void Save(XElement element)
        {
            if (!element.HasAttributes)
            {
                return;
            }

            var currentElement =
                this.RootElement.Elements().FirstOrDefault(el => el.Attribute("id") == element.Attribute("id"));
            if (currentElement != null)
            {
                currentElement.ReplaceWith(element);
            }

            var product = this.RootElement.Element("product");
            if (product != null)
            {
                product.AddAfterSelf(element);
            }
        }
    }
}