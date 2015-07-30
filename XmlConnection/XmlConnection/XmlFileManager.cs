// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XmlFileManager.cs" company="">
//   
// </copyright>
// <summary>
//   The xml file manager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XmlConnection
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.Schema;

    using XmlConnection.XmlAccess;
    using XmlConnection.XmlAccess.Decorator;

    /// <summary>
    /// The xml file manager.
    /// </summary>
    internal class XmlFileManager
    {
        #region Field

        /// <summary>
        /// The file path.
        /// </summary>
        private readonly string filePath;

        /// <summary>
        /// The xml read.
        /// </summary>
        private readonly XmlRead xmlRead;

        /// <summary>
        /// The element modification.
        /// </summary>
        private readonly ElementModification<XElement> elementModification;

        /// <summary>
        /// The root element.
        /// </summary>
        private XElement rootElement;

        /// <summary>
        /// The settings.
        /// </summary>
        private XmlReaderSettings settings;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlFileManager"/> class.
        /// </summary>
        /// <param name="filePath">
        /// The file path.
        /// </param>
        public XmlFileManager(string filePath)
        {
            this.filePath = filePath;
            this.LoadXmlFile(filePath);
            var order = this.rootElement.Element("order");
            if (order != null)
            {
                this.elementModification = new ElementModification<XElement>(new XmlSave(order), new XmlDelete(order));
                this.xmlRead = new XmlRead(order);
            }
            else
            {
                this.elementModification = new ElementModification<XElement>(
                    new XmlSave(this.rootElement), 
                    new XmlDelete(this.rootElement));
                this.xmlRead = new XmlRead(this.rootElement);
            }
        }

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

        #region Methods

        /// <summary>
        /// Update element.
        /// </summary>
        /// <param name="element">
        /// </param>
        public void Save(XElement element)
        {
            this.elementModification.Save(element);
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="element">
        /// The element.
        /// </param>
        public void Delete(XElement element)
        {
            this.elementModification.Save(element);
        }

        /// <summary>
        /// Read named elements. 
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="XElement"/>.
        /// </returns>
        public XElement GetElementById(string id)
        {
            return this.xmlRead.GetElementById(id);
        }

        /// <summary>
        /// The get elements by tag name.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see />.
        /// </returns>
        public List<XElement> GetElementsByTagName(string name)
        {
            return this.xmlRead.GetElementsByTagName(name).ToList();
        }

        /// <summary>
        /// Read all elements .
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<XElement> GetAllElements()
        {
            return this.xmlRead.GetAllElements().ToList();
        }

        /// <summary>
        /// The write file.
        /// </summary>
        public void WriteFile()
        {
            this.rootElement.Save(this.filePath);
        }

        /// <summary>
        /// Error or Warnings collector.
        /// </summary>
        /// <param name="sender">
        /// </param>
        /// <param name="validationEventArgs">
        /// </param>
        private void OnValidationViolation(object sender, ValidationEventArgs validationEventArgs)
        {
            switch (validationEventArgs.Severity)
            {
                case XmlSeverityType.Error:
                    {
                        this.XmlReaderErrors.Add(
                            string.Format(
                                "Error Line Number : {0} Message {1}", 
                                validationEventArgs.Exception.LineNumber, 
                                validationEventArgs.Message));
                    }

                    break;

                case XmlSeverityType.Warning:
                    {
                        this.XmlReaderWarnings.Add(
                            string.Format(
                                "Warning Line Number : {0} Message {1}", 
                                validationEventArgs.Exception.LineNumber, 
                                validationEventArgs.Message));
                    }

                    break;
            }
        }

        /// <summary>
        /// Load xml filePath.
        /// </summary>
        /// <param name="file">
        /// </param>
        private void LoadXmlFile(string file)
        {
            this.XmlReaderErrors = new List<string>();
            this.XmlReaderWarnings = new List<string>();
            this.settings = new XmlReaderSettings
                                {
                                    ValidationType = ValidationType.Schema, 
                                    ValidationFlags =
                                        XmlSchemaValidationFlags.ProcessSchemaLocation
                                        | XmlSchemaValidationFlags.ReportValidationWarnings
                                };
            this.settings.ValidationEventHandler += this.OnValidationViolation;
            using (var xmlReader = XmlReader.Create(file, this.settings))
            {
                this.rootElement = XElement.Load(xmlReader);
            }
        }

        #endregion
    }
}