namespace XmlConnection.Interfaces
{
    /// <summary>
    /// Delete interface
    /// </summary>
    /// <typeparam name="TElement"></typeparam>
    public interface IDelete<in TElement>
    {
        /// <summary>
        /// Delete element.
        /// </summary>
        /// <param name="element"></param>
        void Delete(TElement element);
    }
}