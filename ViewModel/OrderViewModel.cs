//-----------------------------------------------------------------------
// <brief>
//   BRIEF
// </brief>
//
// <author>AUTHOR</author>
// <since>Date</since>
//-----------------------------------------------------------------------

namespace ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using Model;

    using ViewModel.Annotations;

    /// <summary>
    /// BRIEF
    /// </summary>
    public class OrderViewModel : INotifyPropertyChanged
    {
        //---------------------------------------------------------------------
        #region [Fields]
        //---------------------------------------------------------------------
        
        private string productName;

        public event PropertyChangedEventHandler PropertyChanged;

        private string productGroup;

        private string productVersion;

        private ObservableCollection<string> featuresCollection;

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Constructors]
        //---------------------------------------------------------------------

        public OrderViewModel(Order order)
        {
            this.SerialNumber = order.SerialNumber;
        }

        public Guid SerialNumber { get; set; }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Properties]
        //---------------------------------------------------------------------

        public string ProductGroup
        {
            get
            {
                return this.productGroup;
            }
            private set
            {
                this.productGroup = value;
                this.OnPropertyChanged();
            }
        }

        public string ProductName
        {
            get
            {
                return this.productName;
            }
            set
            {
                this.productName = value;
                this.OnPropertyChanged();
            }
        }

        public string ProductVersion
        {
            get
            {
                return this.productVersion;
            }
            set
            {
                this.productVersion = value;
                this.OnPropertyChanged();
            }
        }

        public ObservableCollection<string> FeaturesCollection
        {
            get
            {
                return this.featuresCollection;
            }
            set
            {
                this.featuresCollection = value;
                this.OnPropertyChanged();
            }
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