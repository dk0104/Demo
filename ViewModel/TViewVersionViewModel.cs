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
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;

    using ViewModel.Annotations;

    using Version = Model.Version;

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
                                                                       select new TViewFeatureViewModel(feature, this)).ToList<ElementViewModel>()).ToList();
        }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Method]
        //---------------------------------------------------------------------

        internal override void SetIsChecked(bool? value, bool updateChildren, bool updateParent)
        {

            if (value!=null && (bool)value)
            {
                Console.WriteLine("SCHREIBE V "+ version.ToString());
            }
            else if (value!=null && !(bool)value)
            {
                Console.WriteLine("Lösche V " + version.ToString());
            }
            
            base.SetIsChecked(value,updateChildren,updateParent);
        }
        

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

       
    }
}