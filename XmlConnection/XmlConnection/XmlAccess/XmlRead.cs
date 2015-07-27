using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using XmlConnection.Interfaces;

namespace XmlConnection.XmlAccess
{
    internal class XmlRead:IRead<XElement>
    {
        public XmlRead(string path)
        {
            RootElement = XElement.Load(path);
        }

        #region MyRegion

        public XElement RootElement { get; private set; }
        public XNamespace DocumentNamespace { get; private set; }
		 
	    #endregion

        #region Methods
        
        public XElement ReadElement(string id)
        {
            var element = this.RootElement.Elements().FirstOrDefault(x => x.Attribute("id").Value == id);
            return element;
        }

        public IEnumerable<XElement> ReadAll()
        {
            return this.RootElement.Elements();
        }
        
        #endregion

       
    }
}