//-----------------------------------------------------------------------
// <brief>
//   Order item view Model
// </brief>
//
// <author>DEnis Keksel</author>
// <since>Date</since>
//-----------------------------------------------------------------------

namespace ViewModel
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using Model;

    using ViewModel.Annotations;

    /// <summary>
    /// Order item view Model
    /// </summary>
    public class OrderItemViewModel :INotifyPropertyChanged
    {
        private string productGroupName;

        private string productName;

        private string version;

        //---------------------------------------------------------------------
        #region [Fields]
        //---------------------------------------------------------------------
        
        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Constructors]
        //---------------------------------------------------------------------

        // OrderViewModel()

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Properties]

        //---------------------------------------------------------------------
        
        /// <summary>
        /// 
        /// </summary>
        public string ProductGroupName          
        {
            get
            {
                return this.productGroupName;
            }
            set
            {
                this.productGroupName = value;
            }
        }

        /// <summary>
        /// Product name.
        /// </summary>
        public string ProductName
        {
            get
            {
                return this.productName;
            }
            set
            {
                this.productName = value;
            }
        }

        /// <summary>
        /// Version.
        /// </summary>
        public string Version
        {
            get
            {
                return this.version;
            }
            set
            {
                this.version = value;
            }
        }

        public ObservableCollection<string> Features { get; set; } 
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