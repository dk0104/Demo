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
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using ViewModel.Annotations;

    /// <summary>
    /// Order item view Model
    /// </summary>
    public class OrderItemViewModel :INotifyPropertyChanged
    {
        //---------------------------------------------------------------------
        #region [Fields]
        //---------------------------------------------------------------------
        
        /// <summary>
        /// Property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        
        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Constructors]
        //---------------------------------------------------------------------

        /// <summary>
        /// Oreder Item view model
        /// </summary>
        public OrderItemViewModel()
        {
            this.Features=string.Empty;
        }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Properties]
        //---------------------------------------------------------------------
        
        /// <summary>
        /// Product group name.
        /// </summary>
        public string ProductGroupName { get; set; }

        /// <summary>
        /// Product name.
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Version.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Features
        /// </summary>
        public string Features { get; set; } 

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