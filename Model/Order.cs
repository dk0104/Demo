using System;
using System.Collections.Generic;

namespace Model
{
    /// <summary>
    /// Order.
    /// </summary>
    public class Order 
    {
        //---------------------------------------------------------------------
        #region [Constructor]
        //---------------------------------------------------------------------
	
        public Order()
        {
            this.ProductGroups = new List<ProductGroup>();
        }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------
        
        //---------------------------------------------------------------------
        #region [Properties]
        //---------------------------------------------------------------------
	    
        /// <summary>
	    /// Gets or sets the Date time.
	    /// </summary>
        public DateTime DateTime { get; set; }
        
        /// <summary>
        /// Gets or sets the List of products. 
        /// </summary>
        public IEnumerable<ProductGroup> ProductGroups { get; private set; } 
        
        /// <summary>
        /// Gets or sets the serial number.
        /// </summary>
        public Guid SerialNumber { get; set; }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------
       
    }
}