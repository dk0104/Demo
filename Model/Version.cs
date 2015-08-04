//-----------------------------------------------------------------------
// <brief>
//   Version model
// </brief>
//
// <author>Denis Keksel</author>
// <since>08.01.2015</since>
//-----------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Linq;

    /// <summary>
    /// Version model
    /// </summary>
    public class Version : ModelBase
    {
        //---------------------------------------------------------------------
        #region [Constructors]
        //---------------------------------------------------------------------
        
        public Version()
        {
            this.Features=new List<Feature>();
        }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Properties]
        //---------------------------------------------------------------------
        
        /// <summary>
        /// Gets or sets the version number.
        /// </summary>
        public string VersionNumber { get; set; }
        
        /// <summary>
        /// Gets or sets the features list.
        /// </summary>
        public List<Feature> Features { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public override XElement CurrentElement { get; set; }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Methods]
        //---------------------------------------------------------------------
	
        public override string ToString()
        {
            return string.Format("Version Number: {0}", this.VersionNumber.ToString()); 
        }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------
       
    }
}