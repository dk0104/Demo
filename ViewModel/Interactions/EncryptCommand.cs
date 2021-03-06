﻿//-----------------------------------------------------------------------
// <brief>
//   Encrypt commans
// </brief>
//
// <author>Denis Keksel</author>
// <since>02.08.2015</since>
//-----------------------------------------------------------------------

namespace ViewModel.Interactions
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// Encrypt commans
    /// </summary>
    public class EncryptCommand : ICommand
    {
        //---------------------------------------------------------------------
        #region [Fields]
        //---------------------------------------------------------------------

        /// <summary>
        /// view Model.
        /// </summary>
        private MainViewModel viewModel;

        public event EventHandler CanExecuteChanged;

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Constructors]
        //---------------------------------------------------------------------
        public EncryptCommand(MainViewModel mainViewModel)
        {
            this.viewModel = mainViewModel;
        }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------
        
        //---------------------------------------------------------------------
        #region [Methods]
        //---------------------------------------------------------------------

        public bool CanExecute(object parameter)
        {
            //return this.viewModel.IsEncryptLicenseAvailable;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            this.viewModel.ExecuteEncrypt();
        }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------
    }
   
}