//-----------------------------------------------------------------------
// <brief>
// Element view model interface
// </brief>
//
// <author>Denis Keksel</author>
// <since>01.08.2015</since>
//
//-----------------------------------------------------------------------

namespace ViewModel
{
    using System.Collections.Generic;

    /// <summary>
    /// Element view model interface
    /// </summary>
    public interface IElementViewModel
    {
        //---------------------------------------------------------------------
        #region [Properties]
        //---------------------------------------------------------------------
        
        /// <summary>
        /// Gets parent tree view element. 
        /// </summary>
        IElementViewModel Parent { get; } 

        /// <summary>
        /// Gets or set children.
        /// </summary>
        IEnumerable<IElementViewModel> Children { get; } 

        /// <summary>
        /// Gets or set selected state.
        /// </summary>
        bool IsSelected { get; set; }

        /// <summary>
        /// Gets or sets the expandet state.
        /// </summary>
        bool IsExpanded { get; set; }

        /// <summary>
        /// Gets or Sets the element name.
        /// </summary>
        string Name { get; }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Methods]
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------
    }
}