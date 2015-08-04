﻿//-----------------------------------------------------------------------
// <brief>
//   Portofolio view model
// </brief>
//
// <author>Denis Keksel</author>
// <since>01.08.2015</since>
//-----------------------------------------------------------------------

namespace ViewModel
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;

    using Model;

    /// <summary>
    /// Portofolio view model
    /// </summary>
    public class TViewProductGroupViewModel :ElementViewModel
    {
        //---------------------------------------------------------------------
        #region [Fields]
        //---------------------------------------------------------------------
       
        private ProductGroup productGroup;

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Constructors] 
        //---------------------------------------------------------------------

        public TViewProductGroupViewModel(ProductGroup productGroup, ElementViewModel parent = null)
        {
            this.productGroup = productGroup;
            this.Parent = parent;
            this.Name = this.productGroup.ToString();
            this.Children = new ReadOnlyCollection<ElementViewModel>((from  product in this.productGroup.Products 
                                                                           select new TViewProductViewModel(product,this)).ToList<ElementViewModel>());
        }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------
    }
}