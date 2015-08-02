﻿//-----------------------------------------------------------------------
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
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    using Microsoft.Win32;

    using ViewModel.Annotations;

    /// <summary>
    /// Main view Model
    /// </summary>
    public class MainViewModel:INotifyPropertyChanged        
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

        public MainViewModel()
        {
           // open = new MenuCommand();
        }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Properties]
        //---------------------------------------------------------------------
        
        public bool IsPortofolioOpened { get; private set; }

        private TViewModel TreeViewModel { get; private set; };
        
        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Methods]
        //---------------------------------------------------------------------

        public  void NewCommand(object sender, ExecutedRoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.ShowDialog();
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
                        this.LoadPortofolio(dialog.FileName);
                        
                        break;
                    case "*.portofolio":
                        dialog.Filter = "Portofolio file|*.xml";
                        dialog.Title = "Open portofolio file";
                        showDialog = dialog.ShowDialog();
                        if (showDialog != null && (bool)showDialog)
                        this.LoadLicenseFile(dialog.FileName);
                        break;
                }
            }
        }

        private void LoadLicenseFile(string fileName)
        {
            throw new NotImplementedException();
        }

        private void LoadPortofolio(string fileName)
        {
            this.TreeViewModel =new TViewModel();
        }

        public string PortofolioFile { get; private set; }

        public  void SaveCommand(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            throw new NotImplementedException();
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

        public  void CanExecuteNew(object sender,CanExecuteRoutedEventArgs canExecuteRoutedEventArgs)
        {
            canExecuteRoutedEventArgs.CanExecute = true;
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