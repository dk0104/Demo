//-----------------------------------------------------------------------
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
        
        public event EventHandler CanExecuteChanged
        {
            add { canExecuteChanged.Add(value); }
            remove { canExecuteChanged.Remove(value); }
        }

        private MainViewModel viewModel;

        /// <summary>
        /// The weak collection of delegates for <see cref="CanExecuteChanged"/>.
        /// </summary>
        private WeakCollection<EventHandler> canExecuteChanged = new WeakCollection<EventHandler>();
        
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
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return this.viewModel.IsLicenseFileOpened && this.viewModel.IsPortfolioOpened;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            MainViewModel.ExecuteEncrypt();
        }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------
    }
   
}