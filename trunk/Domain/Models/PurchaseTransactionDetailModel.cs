using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using SDM.Core.Localization;
using SDM.Domain.Models.Base;
using SDM.Localization.Core;

namespace SDM.Domain.Models
{
    /// <summary>
    /// Contains detail information of an purchase transaction in the database.
    /// </summary>
    [Table("PurchaseTransactionDetail")]
    public class PurchaseTransactionDetailModel : ModelBase, ILocalizable<PurchaseTransactionDetailModel.Texts>
    {
        //**************************************************
        //
        // Public properties
        //
        //**************************************************

        #region Public properties

        /// <summary>
        /// Gets or sets the header of this detail.
        /// </summary>
        public PurchaseTransactionModel Header { get; set; }

        /// <summary>
        /// Gets or sets name of the item.
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// Gets or sets quantity of the item.
        /// </summary>
        public int Quantity { get; set; }

        #endregion


        //**************************************************
        //
        // Constructors
        //
        //**************************************************

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public PurchaseTransactionDetailModel()
        {
        }

        #endregion


        //**************************************************
        //
        // Public methods
        //
        //**************************************************

        #region Public methods

        ///// <summary>
        ///// Validates and creates new item.
        ///// </summary>
        //public string Create(IDbSet<PurchaseTransactionModel> items)
        //{
        //    // just add, no validation
        //    items.Add(this);
        //    return null;
        //}

        ///// <summary>
        ///// Updates this model with information contained in the dummy model.
        ///// </summary>
        //public string Update(PurchaseTransactionModel model)
        //{
        //    // update other information
        //    // TODO: use structure mapping?
        //    this.Time = model.Time;
        //    this.Supplier = model.Supplier;

        //    // ok
        //    return null;
        //}

        #endregion

        //**************************************************
        //
        // Nested classes
        //
        //**************************************************

        #region Nested classes

        public class Texts : CustomLocalizationScopeBase
        {
            public string ItemName = "Item Name";
            public string Quantity = "Quantity";
        }

        #endregion
    }
}