//-----------------------------------------------------------------------
// <brief>
// Model base. 
// </brief>
//
// <author>Denis Keksel</author>
// <since>Date</since>
//-----------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Xml.Linq;

    /// <summary>
    /// Denis Keksel
    /// </summary>
    public abstract class ModelBase 
    {

        //---------------------------------------------------------------------
        #region [Properties]
        //---------------------------------------------------------------------

        public abstract bool IsSelected { get; set; }
        
        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------
    }
}