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
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
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
       
        private ObservableCollection<OrderItemViewModel> orderItemsCollection;

        public event PropertyChangedEventHandler PropertyChanged;

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Constructors]
        //---------------------------------------------------------------------

        public OrderViewModel(Order order)
        {
            this.TimeStamp = order.DateTime;
            this.SerialNumber = order.SerialNumber;
            this.OrderModel = order;
            orderItemsCollection=new ObservableCollection<OrderItemViewModel>();
        }


        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Properties]
        //---------------------------------------------------------------------

        public ObservableCollection<OrderItemViewModel> OrderItems
        {
            get
            {
                return this.orderItemsCollection;
            }
            set
            {
                this.orderItemsCollection = value;
            }
        }

        public Order OrderModel { get; set; }

        public DateTime TimeStamp { get; set; }

        public Guid SerialNumber { get; set; }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Methods]
        //---------------------------------------------------------------------

        public void UpdateOrder()
        {
            this.OrderItems.Clear();
            foreach (var productGroup in OrderModel.ProductGroups)
            {
                foreach (var product in productGroup.Products)
                {
                    foreach (var version in product.Versions)
                    {
                        if (version.IsSelected)
                        {
                            var item = new OrderItemViewModel
                                           {
                                               ProductGroupName = productGroup.ProductGroupName,
                                               ProductName = product.Name,
                                               Version = version.VersionNumber
                                           };
                            var features = version.Features.Where(f => f.IsSelected);
                            var featureRepresentation= features.Aggregate(string.Empty, (current, feature) => current + string.Format("{0}\n ", feature.ToString()));
                            item.Features=featureRepresentation;
                            
                            if (null==OrderItems.FirstOrDefault(x=>x.ProductName==item.ProductName && x.Version==item.Version))
                            {
                                 this.OrderItems.Add(item);
                            }
                           
                        }
                    }
                }
            }
        }
        
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