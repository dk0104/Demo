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
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using Model;

    using ViewModel.Annotations;

    /// <summary>
    /// Feature view model
    /// </summary>
    public class FeatureViewModel:INotifyPropertyChanged,IElementViewModel 
    {
        //---------------------------------------------------------------------
        #region [Fields]
        //---------------------------------------------------------------------
        
        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 
        /// </summary>
        private bool isSelected;

        /// <summary>
        /// 
        /// </summary>
        private bool isExpanded;

        private Feature feature;

        public FeatureViewModel(Feature feature, IElementViewModel parent)
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
        #region [Properties]
        //---------------------------------------------------------------------
        
        public IElementViewModel Parent { get; private set; }

        public IEnumerable<IElementViewModel> Children { get; private set; }

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

        public string Name { get; private set; }

        

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Methods]
        //---------------------------------------------------------------------
        
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
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