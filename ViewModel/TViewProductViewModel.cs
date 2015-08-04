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

            var elementViewModel = this.Parent;
            if (elementViewModel != null && (elementViewModel.Parent != null && elementViewModel.Parent is TViewPortfolioViewModel))
            {
                this.Portfolio = (TViewPortfolioViewModel)this.Parent.Parent;
                this.ParentProductGroup = ((TViewProductGroupViewModel)this.Parent).Group;
            }

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
            var productGroup =
                    this.Portfolio.Order.ProductGroups.FirstOrDefault(
                        pg => pg.ProductGroupName == this.ParentProductGroup.ProductGroupName);
            
            if (value == null)
            {
                this.CreateOrderProduc(productGroup);
            }
            else if ((bool)value)
            {
                this.CreateOrderProduc(productGroup);
            } 
            else 
            {
                this.DeleteOrderProduc(productGroup);
            }
            
            base.SetIsChecked(value, updateChildren, updateParent);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productGroup"></param>
        private void DeleteOrderProduc(ProductGroup productGroup)
        {
            if (productGroup!=null)
            {
                productGroup.Products.RemoveAll(p => p.Id == this.Product.Id);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productGroup"></param>
        private void CreateOrderProduc(ProductGroup productGroup)
        {
            if (productGroup != null)
            {
                if (productGroup.Products.FirstOrDefault(p => p.Name == this.Product.Name) == null)
                {
                    var item = new Product
                              {
                                  Name = this.Product.Name,
                                  Id = this.Product.Id,
                                  Description = this.Product.Description
                              };
                    productGroup.Products.Add(item); 
                }
               
            }
        }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Properties]
        //---------------------------------------------------------------------
        
        public ProductGroup ParentProductGroup { get; set; }

        public TViewPortfolioViewModel Portfolio { get; set; }

        public Product Product { get; private set; }
        
        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------
      
    }
}