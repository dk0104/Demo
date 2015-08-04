//-----------------------------------------------------------------------
// <brief>
//   Product Group
// </brief>
//
// <author>Denis Keksel</author>
// <since>01.08.2015</since>
//-----------------------------------------------------------------------

namespace Model
{
    using System.Collections.Generic;
    using System.Xml.Linq;

    /// <summary>
    /// Product Group
    /// </summary>
    public class ProductGroup : ModelBase
    {
        //---------------------------------------------------------------------
        #region [Constructors]
        //---------------------------------------------------------------------
        public ProductGroup()
        {
            this.Products=new List<PortofolioProduct>();
        }
        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Properties]
        //---------------------------------------------------------------------
        
        /// <summary>
        /// Gets or sets the product name
        /// </summary>
        public string ProductGroupName { get; set; }

        /// <summary>
        /// Gets or sets the products list.
        /// </summary>
        public List<PortofolioProduct> Products { get; set; }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------
        
        //---------------------------------------------------------------------
        #region [Methods]
        //---------------------------------------------------------------------
	
        public override string ToString()
        {
            return this.ProductGroupName;
        }

        public override XElement CurrentElement { get; set; }
        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------
       
    }
}