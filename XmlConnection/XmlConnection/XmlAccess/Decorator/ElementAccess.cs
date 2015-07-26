using System.Collections.Generic;
using XmlConnection.Interfaces;

namespace XmlConnection.XmlAccess.Decorator
{
    public class ElementAccess<TElement>:IRead<TElement>
    {
        private readonly IRead<TElement> read;

        public ElementAccess(IRead<TElement> read)
        {
            this.read = read;
        }

        #region Methods
        
        public TElement ReadElement(string id)
        {
           return read.ReadElement(id);
        }

        public IEnumerable<TElement> ReadAll()
        {
            return read.ReadAll();
        }

        #endregion
       
    }
}