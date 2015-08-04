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
    using System.ComponentModel;
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

        private Order order;

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
                if (value==null)
                {
                    this.CreateProductGroupOrder(portfolio);
                }
                else if ((bool)value)
                {
                    this.CreateProductGroupOrder(portfolio);
                }
                else
                {
                    RemoveProductGroupOrder(portfolio);
                }
            }

            base.SetIsChecked(value, updateChildren, updateParent);
        }

        private void CreateProductGroupOrder(TViewPortfolioViewModel portfolio)
        {
            if (portfolio.Order != null)
            {
                if (null==portfolio.Order.ProductGroups.ToList().FirstOrDefault(pg=>pg.ProductGroupName==this.Group.ProductGroupName))
                {
                    var orderPg = new ProductGroup();
                    orderPg.ProductGroupName = this.Group.ProductGroupName;
                    portfolio.Order.ProductGroups.ToList().Add(orderPg);
                }
            }
        }

        private void RemoveProductGroupOrder(TViewPortfolioViewModel portfolio)
        {
             if (portfolio.Order != null)
             {
                 Console.WriteLine("LÖSCHE Ganze PG " + this.Group.ToString());
                 portfolio.Order.ProductGroups.ToList()
                     .RemoveAll(pg => pg.ProductGroupName == this.Group.ProductGroupName);
             }
        }
    }
}