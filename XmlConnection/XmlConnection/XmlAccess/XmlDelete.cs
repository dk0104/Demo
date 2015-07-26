using System.Linq;
using System.Xml.Linq;
using XmlConnection.Interfaces;

namespace XmlConnection.XmlAccess
{
    public class XmlDelete:IDelete<XElement>
    {
        public XmlDelete(string path)
        {
            RootElement = XElement.Load(path);
        }

        #region Properties

        public XElement RootElement { get; private set; }
       
        #endregion
        public void Delete(XElement element)
        {
            var foundElement = this.RootElement.Elements().FirstOrDefault(el => el.Attribute("id")==element.Attribute("id"));
            if (foundElement != null) foundElement.Remove();
        }
    }
}