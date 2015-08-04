//-----------------------------------------------------------------------
// <brief>
//   Portfolio view model
// </brief>
//
// <author>Denis Keksel</author>
// <since>01.08.2015</since>
//-----------------------------------------------------------------------

namespace ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Model;

    /// <summary>
    /// Portfolio view model
    /// </summary>
    public class TViewProductGroupViewModel :ElementViewModel
    {
        //---------------------------------------------------------------------
        #region [Fields]
        //---------------------------------------------------------------------

        public ProductGroup Group { get; private set; }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Constructors] 
        //---------------------------------------------------------------------

        public TViewProductGroupViewModel(ProductGroup productGroup, ElementViewModel parent = null)
        {
            this.Group = productGroup;
            this.Parent = parent;
            this.Name = this.Group.ToString();
            this.Children = new ReadOnlyCollection<ElementViewModel>((from  product in this.Group.Products 
                                                                           select new TViewProductViewModel(product,this)).ToList<ElementViewModel>()).ToList();
        }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        internal override void SetIsChecked(bool? value, bool updateChildren, bool updateParent)
        {
            TViewPortfolioViewModel portfolio = null;
            if (this.Parent != null && this.Parent is TViewPortfolioViewModel)
            {
                portfolio = (TViewPortfolioViewModel)this.Parent;
            }

            if (portfolio!=null)
            {
                if (value==null||(bool)value)
                {
                    this.Group.IsSelected = true; 
                    portfolio.Order.OrderModel.ProductGroups.Add(this.Group);
                    portfolio.Order.UpdateOrder();
                }
                else
                {
                    this.Group.IsSelected = false;
                    portfolio.Order.OrderModel.ProductGroups.RemoveAll(x => x.ProductGroupName == Group.ProductGroupName);
                }
            }

            base.SetIsChecked(value, updateChildren, updateParent);
        }

        internal void CreateProductGroupOrder(TViewPortfolioViewModel portfolio)
        {
           
        }
    }
}