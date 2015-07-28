using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        private XmlReaderSettings settings;
        private readonly XmlRead xmlRead;
        private XElement rootElement;
        private readonly string filePath;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the list of xml errors.
        /// </summary>
        public List<string> XmlReaderErrors { get; private set; }

        /// <summary>
        /// Gets the list of xml warnings.
        /// </summary>
        public List<string> XmlReaderWarnings { get; private set; }

        #endregion

        #region Constructor
        
        public XmlFileManager(string filePath)
        {
            this.filePath = filePath;
            LoadXmlFile(filePath);
            var order = this.rootElement.Element("order");
            if (order != null)
            {
                this.elementModification = new ElementModification<XElement>(new XmlSave(order), new XmlDelete(order));
                this.xmlRead = new XmlRead(order); 
            }
            else
            {
                this.elementModification = new ElementModification<XElement>(new XmlSave(this.rootElement), new XmlDelete(this.rootElement));
                this.xmlRead = new XmlRead(this.rootElement); 
            }
        }

        #endregion
        
        #region Methods

        /// <summary>
        /// Update element.
        /// </summary>
        /// <param name="element"></param>
        public void Save(XElement element)
        {
            this.elementModification.Save(element);
        }

        public void Delete(XElement element)
        {
            this.elementModification.Save(element);
        }

        /// <summary>
        /// Read named elements. 
        /// </summary>
        /// <returns></returns>
        public XElement GetElementById(string id)
        {
            return this.xmlRead.GetElementById(id);
        }

        public List<XElement> GetElementsByTagName(string name)
        {
            return this.xmlRead.GetElementsByTagName(name).ToList();
        }

        /// <summary>
        /// Read all elements .
        /// </summary>
        /// <returns></returns>
        public List<XElement> GetAllElements()
        {
            return this.xmlRead.GetAllElements().ToList();
        }

        public void WriteFile()
        {
            this.rootElement.Save(this.filePath);
        }

        /// <summary>
        /// Error or Warnings collector.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="validationEventArgs"></param>
        private void OnValidationViolation(object sender, ValidationEventArgs validationEventArgs)
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

        /// <summary>
        /// Load xml filePath.
        /// </summary>
        /// <param name="file"></param>
        private void LoadXmlFile(string file)
        {
            this.XmlReaderErrors = new List<string>();
            this.XmlReaderWarnings = new List<string>();
            this.settings = new XmlReaderSettings
            {
                ValidationType = ValidationType.Schema,
                ValidationFlags = XmlSchemaValidationFlags.ProcessSchemaLocation |
                                  XmlSchemaValidationFlags.ReportValidationWarnings
            };
            settings.ValidationEventHandler += OnValidationViolation;
            using (var xmlReader = XmlReader.Create(file, settings))
            {
                this.rootElement = XElement.Load(xmlReader);
            }
        }

        #endregion
        
    }
}