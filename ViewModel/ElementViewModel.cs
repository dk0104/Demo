//-----------------------------------------------------------------------
// <brief>
// Element view model interface
// </brief>
//
// <author>Denis Keksel</author>
// <since>01.08.2015</since>
//
//-----------------------------------------------------------------------

namespace ViewModel
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using ViewModel.Annotations;

    /// <summary>
    /// Element view model interface
    /// </summary>
    public abstract class ElementViewModel : INotifyPropertyChanged
    {
        //---------------------------------------------------------------------
        #region [Fields]
        //---------------------------------------------------------------------
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        private string name;

        private bool isSelected;

        private bool isExpanded;
        
        bool? isChecked = false;

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------
        
        //---------------------------------------------------------------------
        #region [Properties]
        //---------------------------------------------------------------------
        
        /// <summary>
        /// Gets parent tree view element. 
        /// </summary>
        public ElementViewModel Parent { get; set; } 

        /// <summary>
        /// Gets or set children.
        /// </summary>
        public List<ElementViewModel> Children { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
            
            set
            {
                this.name = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool? IsChecked
        {
            get { return this.isChecked; }
            set { this.SetIsChecked(value, true, true); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="updateChildren"></param>
        /// <param name="updateParent"></param>
        void SetIsChecked(bool? value, bool updateChildren, bool updateParent)
        {
            if (value == this.isChecked)
                return;

            this.isChecked = value;

            if (updateChildren && this.isChecked.HasValue && this.Children!=null)
                this.Children.ForEach(c => c.SetIsChecked(this.isChecked, true, false));

            if (updateParent && this.Parent != null)
                this.Parent.VerifyCheckState();

            this.OnPropertyChanged("IsChecked");
        }

        /// <summary>
        /// 
        /// </summary>
        void VerifyCheckState()
        {
            bool? state = null;
            for (var i = 0; i < this.Children.Count; ++i)
            {
                var current = this.Children[i].IsChecked;
                if (i == 0)
                {
                    state = current;
                }
                else if (state != current)
                {
                    state = null;
                    break;
                }
            }
            this.SetIsChecked(state, false, true);
        }

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