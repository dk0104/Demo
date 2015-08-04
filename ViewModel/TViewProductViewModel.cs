﻿//-----------------------------------------------------------------------
// <brief>
//   Product view model
// </brief>
//
// <author>Denis Keksel</author>
// <since>01.08.2015</since>
//-----------------------------------------------------------------------

namespace ViewModel
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;

    using Model;

    using ViewModel.Annotations;

    /// <summary>
    /// Product view model
    /// </summary>
    public sealed class TViewProductViewModel:ElementViewModel 
    {
        //---------------------------------------------------------------------
        #region [Fields]
        //---------------------------------------------------------------------

        private readonly PortofolioProduct product;
        
        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Constructors]
        //---------------------------------------------------------------------
        
        public TViewProductViewModel(PortofolioProduct product, ElementViewModel parent = null)
        {
            this.Parent = parent;
            this.product = product;
            this.Name = product.ToString();
            this.Children = new ReadOnlyCollection<ElementViewModel>((from version in this.product.Versions
                                                                       select new TViewVersionViewModel(version, this)).ToList<ElementViewModel>());
        }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

      
    }
}