﻿//-----------------------------------------------------------------------
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
    /// The product.
    /// </summary>
    public class Product : ModelBase
    {
        //---------------------------------------------------------------------
        #region [Constructor]
        //---------------------------------------------------------------------
	
        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class.
        /// </summary>
        public Product()
        {
            this.Versions = new List<Version>();
        }


        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------
       
        //---------------------------------------------------------------------
        #region [Properties]
        //---------------------------------------------------------------------
	
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the features.
        /// </summary>
        public List<Version> Versions { get; private set; }

        public override string ToString()
        {
            return this.Name;
        }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------
      
    }
}