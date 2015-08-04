//-----------------------------------------------------------------------
// <brief>
//   Feature view model
// </brief>
//
// <author>Denis Keksel</author>
// <since>01.08.2015</since>
//-----------------------------------------------------------------------

namespace ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using Model;

    using ViewModel.Annotations;

    /// <summary>
    /// Feature view model
    /// </summary>
    public sealed class TViewFeatureViewModel:ElementViewModel 
    {
        //---------------------------------------------------------------------
        #region [Fields]
        //---------------------------------------------------------------------
       
        private Feature feature;
        

        public TViewFeatureViewModel(Feature feature, ElementViewModel parent)
        {
            this.feature = feature;
            this.Parent = parent;
            this.Name = feature.ToString();
            this.Children = null;
        }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Constructors]
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Methods]
        //---------------------------------------------------------------------

        internal override void SetIsChecked(bool? value, bool updateChildren, bool updateParent)
        {
            var portfolio = (TViewPortfolioViewModel)this.Parent.Parent.Parent.Parent;
            if (value==null ||  (bool)value)
            {
                this.feature.IsSelected = true;
            }
            else
            {
                this.feature.IsSelected = false;
            }
            portfolio.Order.UpdateOrder();
            base.SetIsChecked(value,updateChildren,updateParent);
        }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------
    }
}