//-----------------------------------------------------------------------
// <brief>
// Portfolio view model.
// </brief>
//
// <author>Denis Keksel</author>
// <since>02.08.2015</since>
//-----------------------------------------------------------------------

namespace ViewModel
{
    using System.Collections.ObjectModel;
    using System.Linq;

    using Model;

    /// <summary>
    /// Denis Keksel
    /// </summary>
    public sealed class TViewPortfolioViewModel : ElementViewModel
    {

        //---------------------------------------------------------------------
        #region [Fields]
        //---------------------------------------------------------------------

        private Portfolio portoflio;
        
        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Constructors]
        //---------------------------------------------------------------------

        public TViewPortfolioViewModel(Portfolio p)
        {
            this.Parent = null;
            this.portoflio = p;
            this.Name = p.ToString();
            this.Children = new ReadOnlyCollection<ElementViewModel>((from pgroup in this.portoflio.ProductGroups
                                                                       select new TViewProductGroupViewModel(pgroup, this)).ToList<ElementViewModel>()).ToList();
        }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Properties]
        //---------------------------------------------------------------------
	
        public Order Order { get; set; }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------
        
    }
}