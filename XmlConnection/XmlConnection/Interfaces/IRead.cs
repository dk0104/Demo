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
        /// Read root adressed by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns a list of elements</returns>
        XElement ReadHeadElement(uint id);

        /// <summary>
        /// Read child element adressd by id and name.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="childElementName"></param>
        /// <returns></returns>
        XElement ReadChildElement(uint id, string childElementName);
        
        /// <summary>
        /// Read all elements
        /// </summary>
        /// <returns>Returns a list of elements </returns>
        IEnumerable<TElement> ReadAll();

    }
}