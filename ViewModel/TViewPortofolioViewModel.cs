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
    using System.Collections.ObjectModel;
    using System.Linq;

    using Model;

    /// <summary>
    /// Denis Keksel
    /// </summary>
    public sealed class TViewPortofolioViewModel : ElementViewModel
    {
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

        public TViewPortofolioViewModel(Portofolio p)
        {
            this.Parent = null;
            this.portofolio = p;
            this.Name = p.ToString();
            this.Children = new ReadOnlyCollection<ElementViewModel>((from pgroup in this.portofolio.ProductGroups
                                                                       select new TViewProductGroupViewModel(pgroup, this)).ToList<ElementViewModel>()).ToList();
        }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

       
    }
}