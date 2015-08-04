//-----------------------------------------------------------------------
// <brief>
//   Version view Model
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

    using Version = Model.Version;

    /// <summary>
    /// Version view Model
    /// </summary>
    public sealed class TViewVersionViewModel:ElementViewModel 
    {
        //---------------------------------------------------------------------
        #region [Fields]
        //---------------------------------------------------------------------
   
        private Version version;

        private TViewProductGroupViewModel parenProductGroup;

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Constructors]
        //---------------------------------------------------------------------

        public TViewVersionViewModel(Version version, ElementViewModel parent)
        {
            this.version = version;
            this.Parent = parent;
            this.Name = version.ToString();
            this.Children = new ReadOnlyCollection<ElementViewModel>((from feature in this.version.Features
                                                                       select new TViewFeatureViewModel(feature, this)).ToList<ElementViewModel>()).ToList();
            var elementViewModel = this.Parent;
            if (elementViewModel != null && (elementViewModel.Parent != null && elementViewModel.Parent is TViewPortfolioViewModel))
            {
                this.Portfolio = (TViewPortfolioViewModel)this.Parent.Parent.Parent;
                this.ParenProductGroup = (TViewProductGroupViewModel)this.Parent.Parent;
                this.ParentProduct = ((TViewProductViewModel)this.Parent).Product;
            }
        }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Method]
        //---------------------------------------------------------------------

        internal override void SetIsChecked(bool? value, bool updateChildren, bool updateParent)
        {
            var productGroup =
                    this.Portfolio.Order.ProductGroups.FirstOrDefault(
                        pg => pg.ProductGroupName == this.ParenProductGroup.Group.ProductGroupName);
            Product currentProduct = null;
            if (productGroup!=null )
            {
                currentProduct = productGroup.Products.Find(x => x.Id == this.ParentProduct.Id);
            }
            
            if (value==null || (bool)value)
            {
                this.CreateOrderVersion(currentProduct);
            }
            else
            {
                this.DeleteOrderVersion(currentProduct);
            }
            
            base.SetIsChecked(value,updateChildren,updateParent);
        }

        private void DeleteOrderVersion(Product product)
        {
            if (product!=null)
            {
                product.Versions.RemoveAll(v => v.VersionNumber == this.version.VersionNumber);
            }
        }

        private void CreateOrderVersion(Product product)
        {
            if (product!=null )
            {
                if (product.Versions.Find(x=>x.VersionNumber==this.version.VersionNumber)==null)
                {
                    var item = new Version() { VersionNumber = this.version.VersionNumber };
                    product.Versions.Add(item);
                }
            }
        }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Properties]
        //---------------------------------------------------------------------
	
        public TViewPortfolioViewModel Portfolio { get; set; }

        public TViewProductGroupViewModel ParenProductGroup
        {
            get
            {
                return this.parenProductGroup;
            }
            set
            {
                this.parenProductGroup = value;
            }
        }

        public Product ParentProduct { get; set; }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

       
    }
}