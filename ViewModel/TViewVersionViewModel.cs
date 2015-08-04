//-----------------------------------------------------------------------
// <brief>
//   Version view Model
// </brief>
//
// <author>Denis Keksel</author>
// <since>01.08.2015</since>
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
    /// Version view Model
    /// </summary>
    public sealed class TViewVersionViewModel:ElementViewModel 
    {
        //---------------------------------------------------------------------
        #region [Fields]
        //---------------------------------------------------------------------
   
        private Version version;
        
        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Constructors]
        //---------------------------------------------------------------------

        public TViewVersionViewModel(Version version, ElementViewModel parent)
        {
            this.version = version;
            this.Parent = parent;
            this.Name = version.ToString();
            this.Children = new ReadOnlyCollection<ElementViewModel>((from feature in this.version.Features
                                                                       select new TViewFeatureViewModel(feature, this)).ToList<ElementViewModel>());
        }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

       
    }
}