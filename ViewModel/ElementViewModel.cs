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
        private string name;

        private bool isSelected;

        private bool isExpanded;

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
        public IEnumerable<ElementViewModel> Children { get; set; }

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


        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Methods]
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------
        
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}