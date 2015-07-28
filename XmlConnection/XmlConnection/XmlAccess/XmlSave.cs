using System.Linq;
using System.Xml.Linq;
using XmlConnection.Interfaces;

namespace XmlConnection.XmlAccess
{
    internal class XmlSave:ISave<XElement>
    {
        public XmlSave(XElement rootElement)
        {
            this.RootElement = rootElement;
        }

        public XElement RootElement { get; private set; }
      

        public void Save(XElement element)
        {
            if (!element.HasAttributes)
            {
                return;
            }
           
            var currentElement = this.RootElement.Elements().FirstOrDefault(el => el.Attribute("id") == element.Attribute("id"));
            if (currentElement!=null)
            {
                currentElement.ReplaceWith(element);
            }
            var product = this.RootElement.Element("product");
            if (product != null) product.AddAfterSelf(element);
        }
    }
}