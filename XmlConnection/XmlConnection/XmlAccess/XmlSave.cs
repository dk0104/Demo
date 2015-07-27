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
            var currentElement = this.RootElement.Elements().Select(x=>x.Name==element.Name).FirstOrDefault();
            element.Add(element);
        }
    }
}