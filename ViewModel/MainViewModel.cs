//-----------------------------------------------------------------------
// <brief>
//   Main view Model
// </brief>
//
// <author>Denis Keksel</author>
// <since>01.08.2015</since>
//-----------------------------------------------------------------------

namespace ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;
    using EncryptionMock;
    using Microsoft.Win32;
    using Model;
    using ViewModel.Annotations;
    using ViewModel.Interactions;
    using XmlConnection;

    /// <summary>
    /// Main view Model
    /// </summary>
    public class MainViewModel:INotifyPropertyChanged        
    {
        //---------------------------------------------------------------------
        #region [Fields] 
        //---------------------------------------------------------------------

        /// <summary>
        /// Property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Portofolio view model. 
        /// </summary>
        private TViewPortfolioViewModel portfolioViewModel;

        /// <summary>
        /// Root element collecxtion.
        /// </summary>
        private ObservableCollection<TViewPortfolioViewModel> rootElementCollection;

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Constructors]
        //---------------------------------------------------------------------

        public MainViewModel()
        {
            this.rootElementCollection = new ObservableCollection<TViewPortfolioViewModel>();
            this.EncryptCommand = new EncryptCommand(this);
            this.Order = new Order { DateTime = DateTime.Now };
            this.CurrentOrder = new OrderViewModel(this.Order);
            this.IsPortfolioOpened = false;
            this.IsOrderFileOpened = false;
            //this.IsEncryptLicenseAvailable = true;
            this.CurrentOrder.OrderItems.CollectionChanged += OrderItems_CollectionChanged;
        }

        void OrderItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.IsEncryptLicenseAvailable = this.CurrentOrder.OrderItems.Count > 0;
        }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Properties]
        //---------------------------------------------------------------------
        
        public bool IsPortfolioOpened { get; private set; }

        public ObservableCollection<TViewPortfolioViewModel> RootElementCollection
        {
            get
            {
                return this.rootElementCollection;
            }
            set
            {
                this.rootElementCollection = value;
                this.OnPropertyChanged();
            }
        }

        public OrderViewModel CurrentOrder { get; private set; }

        public Order Order { get; set; }

        private bool isEncryptLicenseAvailable;
        public bool IsEncryptLicenseAvailable
        { 
            get
            {
                return isEncryptLicenseAvailable;
            } 
            set
            {
                if (this.isEncryptLicenseAvailable != value)
                {
                    this.isEncryptLicenseAvailable = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public bool IsOrderFileOpened { get; set; }

        public ICommand EncryptCommand { get; set; }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Methods]
        //---------------------------------------------------------------------
        
        public  void NewCommand(object sender, ExecutedRoutedEventArgs e)
        {
            this.CreateOrder();
        }

        public  void OpenCommand(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            var dialog = new OpenFileDialog();
            if (executedRoutedEventArgs.Parameter!=null)
            {
                bool? showDialog;
                switch (executedRoutedEventArgs.Parameter.ToString())
                {
                    case "*.key":
                        dialog.Filter = "License key|*.xml";
                        dialog.Title = "Open license file";
                        showDialog = dialog.ShowDialog();
                        if ( showDialog != null && (bool)showDialog)
                        this.LoadLicenseFile(dialog.FileName);
                        
                        break;
                    case "*.portfolio":
                        dialog.Filter = "Portfolio file|*.xml";
                        dialog.Title = "Open portfolio file";
                        showDialog = dialog.ShowDialog();
                        if (showDialog != null && (bool)showDialog)
                        this.LoadPortfolio(dialog.FileName);
                        break;
                }
            }
        }

        public  void SaveCommand(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            //var dialog = new SaveFileDialog { Filter = "License key|*.xml", Title = "Savie license key" };
            //XmlFileWriter.WriteOrder(this.Order,dialog.FileName);
        }

        /// <summary>
        /// Save Command.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="executedRoutedEventArgs"></param>
        public void SaveAsCommand(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            var dialog = new SaveFileDialog { Filter = "License key|*.xml", Title = "Savie license key" };
            var showDialog = dialog.ShowDialog();
            if (showDialog != null && (bool)showDialog)
            {
                var xmlWriter=new XmlFileWriter();
                xmlWriter.WriteOrder(this.Order,dialog.FileName);
            }
        }

        public  void FindCommand(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            if (executedRoutedEventArgs.Parameter!=null)
            {
                var textToFind = executedRoutedEventArgs.Parameter;
            }
            else
            {
                MessageBox.Show("TODO: Implement Search dialog");
            }

        }

        public void DeleteCommand(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            this.DeleteOrder();
        }

        public void HelpCommand(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            MessageBox.Show("TODO: Create help page");
        }

        public  void CanExecuteNew(object sender,CanExecuteRoutedEventArgs canExecuteRoutedEventArgs)
        {
            canExecuteRoutedEventArgs.CanExecute = this.IsPortfolioOpened;
        }

        public void CanExecuteOpen(object sender,CanExecuteRoutedEventArgs canExecuteRoutedEventArgs)
        {
            if (canExecuteRoutedEventArgs.Parameter!=null)
            {
                switch (canExecuteRoutedEventArgs.Parameter.ToString())
                {
                    case "*.key":
                        {
                            canExecuteRoutedEventArgs.CanExecute = this.IsPortfolioOpened;

                        }break;
                    case "*.portfolio":
                        {
                            //TODO: Add logic
                            canExecuteRoutedEventArgs.CanExecute = true;
                        }break;
                }
            }
        }

        public  void CanExecuteSave(object sender,CanExecuteRoutedEventArgs canExecuteRoutedEventArgs)
        {
            //TODO: store file path
            canExecuteRoutedEventArgs.CanExecute = this.IsOrderFileOpened;
            canExecuteRoutedEventArgs.CanExecute = false;
        }

        public void CanExecuteSaveAs(object sender, CanExecuteRoutedEventArgs canExecuteRoutedEventArgs)
        {
            canExecuteRoutedEventArgs.CanExecute = this.IsOrderFileOpened;
        }

        public void CanExecuteFind(object sender, CanExecuteRoutedEventArgs canExecuteRoutedEventArgs)
        {
            canExecuteRoutedEventArgs.CanExecute = true;
        }

        public void CanExecuteDelete(object sender, CanExecuteRoutedEventArgs canExecuteRoutedEventArgs)
        {
            canExecuteRoutedEventArgs.CanExecute = this.IsOrderFileOpened;
        }

        public void CanExecuteHelp(object sender, CanExecuteRoutedEventArgs canExecuteRoutedEventArgs)
        {
            canExecuteRoutedEventArgs.CanExecute = true;
        }

        /// <summary>
        /// Execute encrypt.
        /// </summary>
        public void ExecuteEncrypt()
        {
            var dialog = new SaveFileDialog{ Filter = "Save License key|*.xml", Title = "Save license file" };
            var showDialog = dialog.ShowDialog();
            if (showDialog != null && (bool)showDialog)
            {
                this.Order.SerialNumber=Encrypt.Enctypt(Guid.NewGuid());
                var xmlWriter = new XmlFileWriter();
                xmlWriter.WriteOrder(this.Order,dialog.FileName);
            }
        }

        /// <summary>
        /// Load license file.
        /// </summary>
        /// <param name="fileName"></param>
        private void LoadLicenseFile(string fileName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Load portofolio.
        /// </summary>
        /// <param name="fileName"></param>
        private void LoadPortfolio(string fileName)
        {
            if (this.RootElementCollection.Any())
            {
                this.RootElementCollection.Clear();
            }
            this.ReadPortfolio(fileName);
            this.IsPortfolioOpened = true;
        }

        /// <summary>
        /// Create order.
        /// </summary>
        private void CreateOrder()
        {
            this.IsOrderFileOpened = true;
            this.CurrentOrder.TimeStamp = DateTime.Now;
            this.portfolioViewModel.Order = this.CurrentOrder;
            //this.IsEncryptLicenseAvailable = true;
        }

        /// <summary>
        /// Delete order.
        /// </summary>
        public void DeleteOrder()
        {
            this.IsOrderFileOpened = false;
        }

        /// <summary>
        /// Read Portfolio.
        /// </summary>
        /// <param name="fileName"></param>
        private void ReadPortfolio(string fileName)
        {
            Portfolio portfolio;
            var xmlReader = new XmlFileReader(fileName);
            xmlReader.ReadPortfolio(out portfolio);
            this.portfolioViewModel = new TViewPortfolioViewModel(portfolio);
            this.RootElementCollection.Add(this.portfolioViewModel);
            this.CreateOrder();
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