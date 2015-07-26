using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using XmlConnection.XmlAccess;
using XmlConnection.XmlAccess.Decorator;

namespace XmlConnection
{
    public class XmlFileManager
    {
        #region Field

        private readonly ElementModification<XElement> elementModification;
        private readonly ElementAccess<XElement> elementAccess;
        private const string ElementNamespace = "http://www.demo-application.com/products";

        #endregion
        public XmlFileManager(string file )
        {
            var xmlDelete = new XmlDelete(file);;
            var xmlSave = new XmlSave(file,ElementNamespace);
            var xmlRead = new XmlRead(file);
            this.elementModification = new ElementModification<XElement>(xmlSave,xmlDelete);
            this.elementAccess = new ElementAccess<XElement>(xmlRead);
        }

        public void AddElement(XElement element)
        {
            this.elementModification.Save(element);
        }

        public void UpdateElementVersion(string id, string value)
        {
            var elem = this.elementAccess.ReadElement(id);
            if (elem == null) return;
            var elementVersion = elem.Elements().FirstOrDefault(x => x.Name == "version");
            if (elementVersion != null) elementVersion.SetValue(value);
            this.elementModification.Save(elem);
        }

        public XElement ReadElement(string id)
        {
           return this.elementAccess.ReadElement(id);
        }

        public List<XElement> ReadAllElements(string elementPath)
        {
            return this.elementAccess.ReadAll().ToList();
        }
    }
}