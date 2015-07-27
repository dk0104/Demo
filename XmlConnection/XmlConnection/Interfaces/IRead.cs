using System.Collections.Generic;
using System.Xml.Linq;

namespace XmlConnection.Interfaces
{
    /// <summary>
    /// Read interface.
    /// </summary>
    /// <typeparam name="TElement"></typeparam>
    public interface IRead<out TElement>
    {
        /// <summary>
        /// Read elements adressed by name
        /// </summary>
        /// <param name="name">
        /// Element name.
        /// </param>
        /// <returns>Returns a list of elements</returns>
        IEnumerable<TElement> ReadElement(string name);
        
        /// <summary>
        /// Read all elements
        /// </summary>
        /// <returns>Returns a list of elements </returns>
        IEnumerable<TElement> ReadAll();

    }
}