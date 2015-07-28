using System.Linq;
using System.Xml.Linq;
using XmlConnection.Interfaces;

namespace XmlConnection.XmlAccess
{
    internal class XmlDelete:IDelete<XElement>
    {
        public XmlDelete(XElement rootElement)
        {
            this.RootElement = rootElement;
        }

        #region Properties

        public XElement RootElement { get; private set; }
       
        #endregion
        public void Delete(XElement element)
        {
            var foundElement = this.RootElement.Elements().Single(el => el.Attribute("id") == element.Attribute("id"));
            if (foundElement!=null)
            {
                 foundElement.Remove();
            }
        }
    }
}