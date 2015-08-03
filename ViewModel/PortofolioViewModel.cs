//-----------------------------------------------------------------------
// <brief>
// Portofolio view model.
// </brief>
//
// <author>Denis Keksel</author>
// <since>02.08.2015</since>
//-----------------------------------------------------------------------

namespace ViewModel
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;

    using Model;

    using ViewModel.Annotations;

    /// <summary>
    /// Denis Keksel
    /// </summary>
    public sealed class PortofolioViewModel : INotifyPropertyChanged,IElementViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool isSelected;

        private bool isExpanded;

        private Portofolio portofolio;

        //---------------------------------------------------------------------
        #region [Fields]
        //---------------------------------------------------------------------
        
        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Constructors]
        //---------------------------------------------------------------------

        public PortofolioViewModel(Portofolio p)
        {
            this.Parent = null;
            this.portofolio = p;
            this.Name = p.ToString();
            this.Children = new ReadOnlyCollection<IElementViewModel>((from pgroup in this.portofolio.ProductGroups
                                                                       select new ProductGroupViewModel(pgroup, this)).ToList<IElementViewModel>());
        }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Properties]
        //---------------------------------------------------------------------
        
        public IElementViewModel Parent { get; private set; }

        public IEnumerable<IElementViewModel> Children { get; private set; }

        public string Name { get; private set; }

        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }
            set
            {
                this.isSelected = value;
                this.OnPropertyChanged();
            }
        }

        public bool IsExpanded
        {
            get
            {
                return this.isExpanded;
            }
            set
            {
                this.isExpanded = value;
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
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
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