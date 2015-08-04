﻿//-----------------------------------------------------------------------
// <brief>
//   Portofolio Model
// </brief>
//
// <author>Denis Keksel</author>
// <since>02.08.2015</since>
//-----------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Xml.Linq;

    /// <summary>
    /// Portofolio Model
    /// </summary>
    public class Portofolio : ModelBase
    {
        //---------------------------------------------------------------------
        #region [Constructors]
        //---------------------------------------------------------------------
        
        public Portofolio()
        {
            this.ProductGroups = new List<ProductGroup>();
        }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Properties]
        //---------------------------------------------------------------------

        public List<ProductGroup> ProductGroups { get; private set; }

        public override XElement CurrentElement { get; set; }
        
        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------

        //---------------------------------------------------------------------
        #region [Methods]
        //---------------------------------------------------------------------

        public override string ToString()
        {
            return "Potofolio";
        }

        //---------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------
    }
}