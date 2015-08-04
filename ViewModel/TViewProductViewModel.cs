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
    public sealed class TViewProductViewModel:INotifyPropertyChanged ,IElementViewModel 
    {
        //---------------------------------------------------------------------
        #region [Fields]
        //---------------------------------------------------------------------

        private readonly PortofolioProduct product;

        /// <summary>
        /// 
        /// </summary>
        private bool isSelected;

        /// <summary>
        /// 
        /// </summary>
        private bool isExpanded;

        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Constructors]
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        public TViewProductViewModel(PortofolioProduct product, IElementViewModel parent = null)
        {
            this.Parent = parent;
            this.product = product;
            this.Name = product.ToString();
            this.Children = new ReadOnlyCollection<IElementViewModel>((from version in this.product.Versions
                                                                       select new TViewVersionViewModel(version, this)).ToList<IElementViewModel>());
        }

        
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