// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XmlRead.cs" company="">
//   
// </copyright>
// <summary>
//   The xml read.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XmlConnection.XmlAccess
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net.Sockets;
    using System.Xml.Linq;

    using XmlConnection.Interfaces;

    /// <summary>
    /// The xml read.
    /// </summary>
    internal class XmlRead : IRead<XElement>
    {
        #region Constrictor

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlRead"/> class.
        /// </summary>
        /// <param name="rootElement">
        /// The root element.
        /// </param>
        public XmlRead(XElement rootElement)
        {
            this.RootElement = rootElement;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the root element.
        /// </summary>
        public XElement RootElement { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Get element by id.
        /// </summary>
        /// <param name="id">
        /// </param>
        /// <returns>
        /// The <see cref="XElement"/>.
        /// </returns>
        public XElement GetElementById(string id)
        {
            var productGroup = this.RootElement.Elements().SelectMany(x => x.Elements().Where(el=>el.Name.LocalName=="product"));
            var product = productGroup.FirstOrDefault(p => p.Attribute("id").Value == id);
            return product;
        }

        /// <summary>
        /// Get elements by name.
        /// </summary>
        /// <param name="childElementName">
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<XElement> GetElementsByTagName(string childElementName)
        {
            var elements =
                from x in
                    this.RootElement.Elements().SelectMany(x => x.Elements().Where(el => el.Name == childElementName))
                select x;
            return elements;
        }

        /// <summary>
        /// Get all elements.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<XElement> GetRootElements()
        {
            return this.RootElement.Elements();
        }

        #endregion
    }
}