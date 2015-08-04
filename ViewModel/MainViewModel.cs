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
    using System.IO;
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


        private TViewPortofolioViewModel portofolioViewModel;

        private ObservableCollection<TViewPortofolioViewModel> rootElementCollection;

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Constructors]
        //---------------------------------------------------------------------

        public MainViewModel()
        {
            this.rootElementCollection = new ObservableCollection<TViewPortofolioViewModel>();
            this.EncryptCommand = new EncryptCommand(this);
            this.IsPortofolioOpened = false;
            this.IsLicenseFileOpened = false;
        }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Properties]
        //---------------------------------------------------------------------
        
        public bool IsPortofolioOpened { get; private set; }

        public ObservableCollection<TViewPortofolioViewModel> RootElementCollection
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

        public bool IsLicenseFileOpened { get; set; }

        public ICommand EncryptCommand { get; set; }

        //---------------------------------------------------------------------

        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Methods]
        //---------------------------------------------------------------------

        public  void NewCommand(object sender, ExecutedRoutedEventArgs e)
        {
            
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
                    case "*.portofolio":
                        dialog.Filter = "Portofolio file|*.xml";
                        dialog.Title = "Open portofolio file";
                        showDialog = dialog.ShowDialog();
                        if (showDialog != null && (bool)showDialog)
                        this.LoadPortofolio(dialog.FileName);
                        break;
                }
            }
        }

        public  void SaveCommand(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = "License key|*.xml";
            dialog.Title = "Savie license key";
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
            
        }

        public void HelpCommand(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            MessageBox.Show("TODO: Create help page");
        }

        public  void CanExecuteNew(object sender,CanExecuteRoutedEventArgs canExecuteRoutedEventArgs)
        {
            canExecuteRoutedEventArgs.CanExecute = this.IsPortofolioOpened;
        }

        public void CanExecuteOpen(object sender,CanExecuteRoutedEventArgs canExecuteRoutedEventArgs)
        {
            if (canExecuteRoutedEventArgs.Parameter!=null)
            {
                switch (canExecuteRoutedEventArgs.Parameter.ToString())
                {
                    case "*.key":
                        {
                            canExecuteRoutedEventArgs.CanExecute = this.IsPortofolioOpened;

                        }break;
                    case "*.portofolio":
                        {
                            //TODO: Add logic
                            canExecuteRoutedEventArgs.CanExecute = true;
                        }break;
                }
            }
        }

        public  void CanExecuteSave(object sender,CanExecuteRoutedEventArgs canExecuteRoutedEventArgs)
        {
            canExecuteRoutedEventArgs.CanExecute = true;
        }

        public void CanExecuteFind(object sender, CanExecuteRoutedEventArgs canExecuteRoutedEventArgs)
        {
            canExecuteRoutedEventArgs.CanExecute = true;
        }

        public void CanExecuteDelete(object sender, CanExecuteRoutedEventArgs canExecuteRoutedEventArgs)
        {
            canExecuteRoutedEventArgs.CanExecute = this.IsLicenseFileOpened;
        }

        public void CanExecuteHelp(object sender, CanExecuteRoutedEventArgs canExecuteRoutedEventArgs)
        {
            canExecuteRoutedEventArgs.CanExecute = true;
        }

        public static void ExecuteEncrypt()
        {
            Encrypt.Enctypt(Guid.NewGuid());
        }

        private void LoadLicenseFile(string fileName)
        {
            throw new NotImplementedException();
        }

        private void LoadPortofolio(string fileName)
        {

            if (RootElementCollection.Any())
            {
                this.RootElementCollection.Clear();
            }

            this.ReadPortofolio(fileName);

            this.IsPortofolioOpened = true;
        }

        /// <summary>
        /// Read Portofolio.
        /// </summary>
        /// <param name="fileName"></param>
        private void ReadPortofolio(string fileName)
        {
            Portofolio portofolio;
            var xmlReader = new XmlFileReader(fileName);
            xmlReader.ReadPortofolio(out portofolio);
            this.portofolioViewModel = new TViewPortofolioViewModel(portofolio);
            this.RootElementCollection.Add(this.portofolioViewModel);
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