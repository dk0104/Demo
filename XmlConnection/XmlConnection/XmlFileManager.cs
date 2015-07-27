using System.Collections.Generic;
using System.Linq;
using System.Xml;
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
        private readonly ValidatedElementAccess<XElement> elementAccess;
        private const string ElementNamespace = "http://www.demo-application.com/products";

        public List<string> XmlReaderWarnings { get; private set; }
        public List<string> XmlReaderErrors { get; private set; }

        #endregion

        #region Constructor
        
        public XmlFileManager(string file)
        {
            var xmlDelete = new XmlDelete(file);
            var xmlSave = new XmlSave(file, ElementNamespace);
            var xmlReaderSettings = new XmlReaderSettings();
            var xmlRead = new XmlRead(file, xmlReaderSettings);
            this.elementModification = new ElementModification<XElement>(xmlSave, xmlDelete);
            this.elementAccess = new ValidatedElementAccess<XElement>(xmlRead, xmlReaderSettings);
            this.elementAccess.ValidationViolationNotification += OnValidationViolation;
            this.XmlReaderErrors = new List<string>();
            this.XmlReaderWarnings = new List<string>();
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
        public List<XElement> ReadNamedElements(string name)
        {
            return this.elementAccess.ReadElement(name).ToList();
        }

        /// <summary>
        /// Read all elements .
        /// </summary>
        /// <returns></returns>
        public List<XElement> ReadAllElements()
        {
            return this.elementAccess.ReadAll().ToList();
        }

        /// <summary>
        /// Error or Warnings collector.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnValidationViolation(object sender, ValidationEventArgs e)
        {
            switch (e.Severity)
            {
                case XmlSeverityType.Error:
                {
                    XmlReaderErrors.Add(string.Format("Error Line Number : {0} Message {1}", e.Exception.LineNumber,
                        e.Message));

                }
                    break;

                case XmlSeverityType.Warning:
                {
                    XmlReaderWarnings.Add(string.Format("Warning Line Number : {0} Message {1}", e.Exception.LineNumber,
                        e.Message));
                }
                    break;
            }
        }

        #endregion
        
    }
}