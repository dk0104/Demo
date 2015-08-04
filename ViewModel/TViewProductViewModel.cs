//-----------------------------------------------------------------------
// <brief>
//   Product view model
// </brief>
//
// <author>Denis Keksel</author>
// <since>01.08.2015</since>
//-----------------------------------------------------------------------

namespace ViewModel
{
    using System;
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

        private readonly PortfolioProduct product;
        
        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Constructors]
        //---------------------------------------------------------------------
        
        public TViewProductViewModel(PortfolioProduct product, ElementViewModel parent = null)
        {
            this.Parent = parent;
            this.product = product;
            this.Name = product.ToString();
            this.Children = new ReadOnlyCollection<ElementViewModel>((from version in this.product.Versions
                                                                       select new TViewVersionViewModel(version, this)).ToList<ElementViewModel>()).ToList();
        }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Methods]
        //---------------------------------------------------------------------

        internal override void SetIsChecked(bool? value, bool updateChildren, bool updateParent)
        {

            if (value==null)
            {
                Console.WriteLine("SCHREIBE Product " + product.ToString());
            }
            
           
            else if (value != null && !(bool)value)
            {
                Console.WriteLine("Lösche Product " + product.ToString());
            }
            
            base.SetIsChecked(value, updateChildren, updateParent);
        }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

      
    }
}