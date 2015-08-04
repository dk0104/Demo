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
    using System;
    using System.Collections.ObjectModel;
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
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Constructors]
        //---------------------------------------------------------------------

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
        /// 
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

        public string Features { get; set; } 

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Methods]

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

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------
    }
}