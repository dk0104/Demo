namespace XmlConnection.Interfaces
{
    /// <summary>
    /// Save or update an element.
    /// </summary>
    /// <typeparam name="TElement"></typeparam>
    public interface ISave<in TElement>
    {
        /// <summary>
        /// Save Metod-
        /// </summary>
        /// <param name="element"></param>
        void Save(TElement element);
    }
}