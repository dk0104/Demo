using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Schema;
using XmlConnection.XmlAccess;
using XmlConnection.XmlAccess.Decorator;

namespace XmlConnection
{
    internal class XmlFileManager
    {
        #region Field

        private readonly ElementModification<XElement> elementModification;
        private readonly XmlRead xmlRead;
        private const string ElementNamespace = "http://www.demo-application.com/products";

        public List<string> XmlReaderWarnings { get; private set; }
        public List<string> XmlReaderErrors { get; private set; }

        #endregion

        #region Constructor
        
        public XmlFileManager(string file)
        {
            var xmlDelete = new XmlDelete(file);
            var xmlSave = new XmlSave(file, ElementNamespace);
            this.xmlRead = new XmlRead(file); 
            xmlRead.ValidationViolationEvent += OnValidationViolation;
            this.elementModification = new ElementModification<XElement>(xmlSave, xmlDelete);
            this.XmlReaderErrors = new List<string>();
            this.XmlReaderWarnings = new List<string>();
            xmlRead.LoadXml();
        }

        #endregion
        
        #region Methods

        /// <summary>
        /// Update element.
        /// </summary>
        /// <param name="element"></param>
        public void Update(XElement element)
        {
            this.elementModification.Save(element);
        }

        /// <summary>
        /// Read named elements. 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public XElement ReadNamedElements(uint id)
        {
            return this.xmlRead.ReadHeadElement(id);
        }

        public XElement ReadChildElement(uint id, string name)
        {
            return this.xmlRead.ReadChildElement(id, name);
        }

        /// <summary>
        /// Read all elements .
        /// </summary>
        /// <returns></returns>
        public List<XElement> ReadAllElements()
        {
            return this.xmlRead.ReadAll().ToList();
        }

        /// <summary>
        /// Error or Warnings collector.
        /// </summary>
        /// <param name="validationEventArgs"></param>
        private void OnValidationViolation(ValidationEventArgs validationEventArgs)
        {
            switch (validationEventArgs.Severity)
            {
                case XmlSeverityType.Error:
                {
                    XmlReaderErrors.Add(string.Format("Error Line Number : {0} Message {1}", validationEventArgs.Exception.LineNumber,
                        validationEventArgs.Message));

                }
                    break;

                case XmlSeverityType.Warning:
                {
                    XmlReaderWarnings.Add(string.Format("Warning Line Number : {0} Message {1}", validationEventArgs.Exception.LineNumber,
                        validationEventArgs.Message));
                }
                    break;
            }
        }

        #endregion
        
    }
}