using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using XmlConnection.Interfaces;

namespace XmlConnection.XmlAccess
{
    internal class XmlRead:IRead<XElement>
    {
        #region Constrictor

        public XmlRead(XElement rootElement)
        {
            this.RootElement = rootElement;
        }
       
        #endregion

        #region Properties

        public XElement RootElement { get; private set; }

        #endregion
        
        #region Methods
        
        /// <summary>
        /// Get element by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public XElement GetElementById(string id)
        {
            return this.RootElement.Elements().Single(x => x.Attribute("id").Value == id);
        }

        /// <summary>
        /// Get elements by name.
        /// </summary>
        /// <param name="childElementName"></param>
        /// <returns></returns>
        public IEnumerable<XElement> GetElementsByTagName(string childElementName)
        {
            var elements = from x in RootElement.Elements().SelectMany(x => x.Elements().Where(el=>el.Name==childElementName)) select x;
            return elements;
        }

        /// <summary>
        /// Get all elements.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<XElement> GetAllElements()
        {
            return this.RootElement.Elements();
        }
        
        #endregion
    }
}