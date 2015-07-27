using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using XmlConnection.Interfaces;

namespace XmlConnection.XmlAccess.Decorator
{
    internal class ValidatedElementAccess<TElement>:IRead<TElement>
    {

        #region Fields

        private readonly IRead<TElement> read;
        
        #endregion
        
        #region Constructor

        public ValidatedElementAccess(IRead<TElement> read, XmlReaderSettings settings)
        {
            this.read = read;
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
            settings.ValidationEventHandler += ValidationViolationNotificationTrigger;
        }

        #endregion

        #region Events

        public event EventHandler<ValidationEventArgs> ValidationViolationNotification;
        
        #endregion

        #region Methods

        public IEnumerable<TElement> ReadElement(string name)
        {
           return read.ReadElement(name);
        }

        public IEnumerable<TElement> ReadAll()
        {
            return read.ReadAll();
        }

        private void ValidationViolationNotificationTrigger(object sender, ValidationEventArgs e)
        {
            var handler = ValidationViolationNotification;
            if (handler!=null)
            {
                handler(this,e);
            }
        }

        #endregion
       
    }
}