using System.Collections.Generic;

namespace XmlConnection.Interfaces
{
    /// <summary>
    /// Read interface.
    /// </summary>
    /// <typeparam name="TElement"></typeparam>
    public interface IRead<out TElement>
    {
        /// <summary>
        /// Read single element adressed by name
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TElement ReadElement(string id);
        
        /// <summary>
        /// Read all elements
        /// </summary>
        /// <returns></returns>
        IEnumerable<TElement> ReadAll();

    }
}