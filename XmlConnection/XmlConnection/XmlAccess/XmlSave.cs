using System.Linq;
using System.Xml.Linq;
using XmlConnection.Interfaces;

namespace XmlConnection.XmlAccess
{
    internal class XmlSave:ISave<XElement>
    {
        public XmlSave(string path, string documentNamespace)
        {
           this.RootElement = XElement.Load(path);
           this.DocumentNamespace = documentNamespace;
        }

        public XElement RootElement { get; private set; }
        public XNamespace DocumentNamespace { get; private set; }

        public void Save(XElement element)
        {
            var currentElement = this.RootElement.Elements().FirstOrDefault(e => e.Attribute("id") == element.Attribute("id"));
            if (currentElement != null) currentElement.Remove();
            element.Add(element);
        }
    }
}