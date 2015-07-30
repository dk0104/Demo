// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ElementModification.cs" company="">
//   
// </copyright>
// <summary>
//   The element modification.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace XmlConnection.XmlAccess.Decorator
{
    using XmlConnection.Interfaces;

    /// <summary>
    /// The element modification.
    /// </summary>
    /// <typeparam name="TElement">
    /// </typeparam>
    internal class ElementModification<TElement> : ISave<TElement>, IDelete<TElement>
    {
        #region Fields

        /// <summary>
        /// The save.
        /// </summary>
        private readonly ISave<TElement> save;

        /// <summary>
        /// The delete.
        /// </summary>
        private readonly IDelete<TElement> delete;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementModification{TElement}"/> class.
        /// </summary>
        /// <param name="save">
        /// The save.
        /// </param>
        /// <param name="delete">
        /// The delete.
        /// </param>
        public ElementModification(ISave<TElement> save, IDelete<TElement> delete)
        {
            this.save = save;
            this.delete = delete;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Save element
        /// </summary>
        /// <param name="element">
        /// </param>
        public void Save(TElement element)
        {
            this.save.Save(element);
        }

        /// <summary>
        /// Delete element.
        /// </summary>
        /// <param name="element">
        /// </param>
        public void Delete(TElement element)
        {
            this.delete.Delete(element);
        }

        #endregion
    }
}