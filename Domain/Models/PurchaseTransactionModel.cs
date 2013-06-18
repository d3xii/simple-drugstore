using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using SDM.Core.Localization;
using SDM.Domain.Models.Base;
using SDM.Localization.Core;

namespace SDM.Domain.Models
{
    /// <summary>
    /// Contains information of an purchase transaction in the database.
    /// </summary>
    [Table("PurchaseTransaction")]
    public class PurchaseTransactionModel : ModelBase, ILocalizable<PurchaseTransactionModel.Texts>
    {
        //**************************************************
        //
        // Public properties
        //
        //**************************************************

        #region Public properties

        /// <summary>
        /// Gets or sets time this transaction was done.
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Gets or sets name of the supplier.
        /// </summary>
        public string Supplier { get; set; }

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
        public PurchaseTransactionModel()
        {
            this.Time = DateTime.Now;
        }

        #endregion


        //**************************************************
        //
        // Public methods
        //
        //**************************************************

        #region Public methods

        /// <summary>
        /// Validates and creates new item.
        /// </summary>
        public string Create(IDbSet<PurchaseTransactionModel> items)
        {
            // just add, no validation
            items.Add(this);
            return null;
        }

        /// <summary>
        /// Updates this model with information contained in the dummy model.
        /// </summary>
        public string Update(PurchaseTransactionModel model)
        {
            // update other information
            // TODO: use structure mapping?
            this.Time = model.Time;
            this.Supplier = model.Supplier;

            // ok
            return null;
        }

        #endregion

        //**************************************************
        //
        // Nested classes
        //
        //**************************************************

        #region Nested classes

        public class Texts : CustomLocalizationScopeBase
        {
            public string Time = "Time";
            public string Supplier = "Supplier";
        }

        #endregion
    }
}