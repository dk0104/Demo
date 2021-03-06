﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Feature.cs" company="">
//   
// </copyright>
// <summary>
//   The feature.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Model
{
    using System.Xml.Linq;

    /// <summary>
    /// The feature.
    /// </summary>
    public class Feature:ModelBase
    {
        //---------------------------------------------------------------------
        #region [Properties]
        //---------------------------------------------------------------------
	
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public override bool IsSelected { get; set; }
        
        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Methods]
        //---------------------------------------------------------------------

        public override string ToString()
        {
            return this.Name;
        }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

       
    }
}