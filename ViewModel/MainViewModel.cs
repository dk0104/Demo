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
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Data;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    using ViewModel.Annotations;

    /// <summary>
    /// Main view Model
    /// </summary>
    public class MainViewModel:INotifyPropertyChanged        
    {
        //---------------------------------------------------------------------
        #region [Fields]

        public event PropertyChangedEventHandler PropertyChanged;

        private ICommand open;

        //---------------------------------------------------------------------
        
        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Constructors]
        //---------------------------------------------------------------------

        public MainViewModel()
        {
            open = new Command();
        }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Properties]
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Methods]
        //---------------------------------------------------------------------

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

        #endregion
        //---------------------------------------------------------------------
    }
}