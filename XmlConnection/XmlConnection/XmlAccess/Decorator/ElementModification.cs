using XmlConnection.Interfaces;

namespace XmlConnection.XmlAccess.Decorator
{
    internal class ElementModification<TElement> : ISave<TElement>,IDelete<TElement>
    {
        #region Fields

        private readonly ISave<TElement> save;
        private readonly IDelete<TElement> delete;
        
        #endregion

        #region Constructor

        public ElementModification(ISave<TElement> save, IDelete<TElement> delete )
        {
            this.save = save;
            this.delete = delete;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Save element
        /// </summary>
        /// <param name="element"></param>
        public void Save(TElement element)
        {
            save.Save(element);
        }

        /// <summary>
        /// Delete element.
        /// </summary>
        /// <param name="element"></param>
        public void Delete(TElement element)
        {
            delete.Delete(element);
           
        }

        #endregion
    }

    
}