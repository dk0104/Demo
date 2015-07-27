using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using XmlConnection.Interfaces;

namespace XmlConnection.XmlAccess
{
    internal class XmlRead:IRead<XElement>
    {
        #region Fields

        /// <summary>
        /// Gets the Root element.
        /// </summary>
        private readonly XElement rootElement; 
        
        #endregion

        #region Constrictor

        public XmlRead(string path, XmlReaderSettings settings)
        {
            rootElement = XElement.Load(XmlReader.Create(path, settings));
        }

        #endregion

        #region Methods

        /// <summary>
        /// Read named elements.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<XElement> ReadElement(string name)
        {
            return this.rootElement.Elements(name);
        }

        /// <summary>
        /// Read all elements.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<XElement> ReadAll()
        {
            return this.rootElement.Elements();
        }
        
        #endregion

       
    }
}