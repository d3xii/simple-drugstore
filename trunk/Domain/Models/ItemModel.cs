using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using SDM.Core.Localization;
using SDM.Domain.Models.Base;
using SDM.Localization.Core;

namespace SDM.Domain.Models
{
    /// <summary>
    /// Contains information of an item in the database.
    /// </summary>
    [Table("Item")]
    public class ItemModel : ModelBase, ILocalizable<ItemModel.Texts>
    {
        //**************************************************
        //
        // Public methods
        //
        //**************************************************

        #region Public methods

        /// <summary>
        /// Gets or sets full name of the item.
        /// </summary>
        [Required]
        public string Name { get; set; }

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
        public string Create(IDbSet<ItemModel> items)
        {
            // not duplicated name
            if (items.IsExisted(this.Name))
            {
                // duplicated
                return this.Localize(t => t.DuplicatedName);
            }

            // add to database
            items.Add(this);
            return null;
        }

        /// <summary>
        /// Updates this model with information contained in the dummy model.
        /// </summary>
        public string Update(ItemModel model)
        {
            // update other information
            this.Name = model.Name;

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
            public string Name = "Name";

            public string DuplicatedName = "The name has been used. Please choose another one.";
        }

        #endregion        
    }
}
