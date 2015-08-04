// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XmlFileManager.cs" company="">
//   Ха
// </copyright>
// <summary>
//   The xml file manager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XmlConnection
{
    using System.Collections.Generic;
    using System.Xml.Linq;
    using System.Xml.Schema;

    using XmlConnection.XmlAccess;
    using XmlConnection.XmlAccess.Decorator;

    /// <summary>
    /// The xml file manager.
    /// </summary>
    internal class XmlFileWriter
    {
        //---------------------------------------------------------------------
        #region [Fields]
        //---------------------------------------------------------------------
	
         /// <summary>
        /// The element modification.
        /// </summary>
        private readonly ElementModification<XElement> elementModification;

        /// <summary>
        /// The root element.
        /// </summary>
        private XElement rootElement;
        
        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------
        
        //---------------------------------------------------------------------
        #region [Constructor]
        //---------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlFileWriter"/> class.
        /// </summary>
        public XmlFileWriter(XElement rootElement)
        {
            this.rootElement = rootElement;
            this.elementModification = new ElementModification<XElement>(
                    new XmlSave(rootElement), 
                    new XmlDelete(rootElement));
           
        }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------
     
        //---------------------------------------------------------------------
        #region [Properties]
        //---------------------------------------------------------------------
	
        /// <summary>
        /// Gets the list of xml errors.
        /// </summary>
        public List<string> XmlReaderErrors { get; private set; }

        /// <summary>
        /// Gets the list of xml warnings.
        /// </summary>
        public List<string> XmlReaderWarnings { get; private set; }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------
        
        //---------------------------------------------------------------------
        #region [Methods]
        //---------------------------------------------------------------------
        
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
        /// The write file.
        /// </summary>
        public void WriteFile(string filePath)
        {
            this.rootElement.Save(filePath);
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

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------        
        
    }
}