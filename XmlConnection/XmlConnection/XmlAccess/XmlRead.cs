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
        private readonly XmlReaderSettings settings;
        private readonly string path;
        public event Action<ValidationEventArgs> ValidationViolationEvent; 

        #region Constrictor

        public XmlRead(string path)
        {
            this.path = path;
            this.settings = new XmlReaderSettings
            {
                ValidationType = ValidationType.Schema,
                ValidationFlags = XmlSchemaValidationFlags.ProcessSchemaLocation|
                XmlSchemaValidationFlags.ReportValidationWarnings
            };
        }

        public void LoadXml()
        {
            settings.ValidationEventHandler += OnValidationEventHandler;
            using (var xmlReader = XmlReader.Create(path,settings))
            {
                this.RootElement = XElement.Load(xmlReader);
            }
        }

        private void OnValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (ValidationViolationEvent != null) ValidationViolationEvent.Invoke(e);
        }

        public XElement RootElement { get;private set; }

        #endregion

        #region Methods

        public XElement ReadHeadElement(uint id)
        {
            return this.RootElement.Elements().Single(x => x.Attribute("id").Value == id.ToString());
        }

        public XElement ReadChildElement(uint id, string childElementName)
        {
            var element = this.ReadHeadElement(id);
            return element.Element(childElementName);
        }

        /// <summary>
        /// Read all elements.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<XElement> ReadAll()
        {
            
            return this.RootElement.Elements();
        }
        
        #endregion

       
    }
}