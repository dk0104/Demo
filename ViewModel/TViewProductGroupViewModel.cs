//-----------------------------------------------------------------------
// <brief>
//   Portofolio view model
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
    /// Portofolio view model
    /// </summary>
    public sealed class TViewProductGroupViewModel :INotifyPropertyChanged,IElementViewModel
    {
        //---------------------------------------------------------------------
        #region [Fields]
        //---------------------------------------------------------------------
        
        public event PropertyChangedEventHandler PropertyChanged;

        private bool isSelected;

        private bool isExpanded;

        private ProductGroup productGroup;

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Constructors] 
        //---------------------------------------------------------------------

        public TViewProductGroupViewModel(ProductGroup productGroup, IElementViewModel parent = null)
        {
            this.productGroup = productGroup;
            this.Parent = parent;
            this.Name = this.productGroup.ToString();
            this.Children = new ReadOnlyCollection<IElementViewModel>((from  product in this.productGroup.Products 
                                                                           select new TViewProductViewModel(product,this)).ToList<IElementViewModel>());
        }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------

        #region [Properties]
        //---------------------------------------------------------------------
        
        /// <summary>
        /// 
        /// </summary>
        public IElementViewModel Parent { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<IElementViewModel> Children { get; private set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }
            set
            {
                this.isSelected = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsExpanded
        {
            get
            {
                return this.isExpanded;
            }
            set
            {
                this.isExpanded = value;
                this.OnPropertyChanged();
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
        /// <param name="propertyName"></param>
        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------
    }
}