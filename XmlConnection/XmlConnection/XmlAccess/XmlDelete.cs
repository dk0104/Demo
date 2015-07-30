// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XmlDelete.cs" company="">
//   
// </copyright>
// <summary>
//   The xml delete.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XmlConnection.XmlAccess
{
    using System.Linq;
    using System.Xml.Linq;

    using XmlConnection.Interfaces;

    /// <summary>
    /// The xml delete.
    /// </summary>
    internal class XmlDelete : IDelete<XElement>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XmlDelete"/> class.
        /// </summary>
        /// <param name="rootElement">
        /// The root element.
        /// </param>
        public XmlDelete(XElement rootElement)
        {
            this.RootElement = rootElement;
        }

        #region Properties

        /// <summary>
        /// Gets the root element.
        /// </summary>
        public XElement RootElement { get; private set; }

        #endregion

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="element">
        /// The element.
        /// </param>
        public void Delete(XElement element)
        {
            var foundElement = this.RootElement.Elements().Single(el => el.Attribute("id") == element.Attribute("id"));
            if (foundElement != null)
            {
                foundElement.Remove();
            }
        }
    }
}