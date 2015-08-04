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
    using System.Collections.ObjectModel;
    using System.Linq;

    using Model;

    /// <summary>
    /// Product view model
    /// </summary>
    public sealed class TViewProductViewModel:ElementViewModel 
    {
        //---------------------------------------------------------------------
        #region [Fields]
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Constructors]
        //---------------------------------------------------------------------
        
        public TViewProductViewModel(Product product, ElementViewModel parent = null)
        {
            this.Parent = parent;
            this.Product = product;
            this.Name = product.ToString();
            this.Children = new ReadOnlyCollection<ElementViewModel>((from version in this.Product.Versions
                                                                       select new TViewVersionViewModel(version, this)).ToList<ElementViewModel>()).ToList();
        }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Methods]
        //---------------------------------------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="updateChildren"></param>
        /// <param name="updateParent"></param>
        internal override void SetIsChecked(bool? value, bool updateChildren, bool updateParent)
        {
            var portfolio = (TViewPortfolioViewModel)this.Parent.Parent;

            if (value == null||(bool)value)
            {
                this.Product.IsSelected = true;
            }
            else
            {
                this.Product.IsSelected = false;
            }
            portfolio.Order.UpdateOrder();
         
            base.SetIsChecked(value, updateChildren, updateParent);
        }
        
        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Properties]
        //---------------------------------------------------------------------
       
        public Product Product { get; private set; }
        
        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------
      
    }
}