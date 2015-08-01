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
    using System.Collections.Generic;

    /// <summary>
    /// Version model
    /// </summary>
    public class Version 
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
        public uint VersionNumber { get; set; }

        /// <summary>
        /// Gets or sets the version description.
        /// </summary>
        public string VersionDescription { get; set; }

        /// <summary>
        /// Gets or sets the features list.
        /// </summary>
        public IEnumerable<Feature> Features { get; private set; }

       

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Methods]
        //---------------------------------------------------------------------
	
        public override string ToString()
        {
            return this.VersionNumber.ToString();
        }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------
    }
}