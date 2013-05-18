using System.Xml.Serialization;
using SDM.Localization.Core;
using SDM.Localization.SharedTexts;
using SDM.Main.Areas.Admin.Controllers;

namespace SDM.Main.Localization
{
    /// <summary>
    /// Contains the root to localization keys & values.
    /// </summary>
    [XmlRoot("Localization")]
    public class LocalizationTextsRoot : ILocalizationScope
    {

        //**************************************************
        //
        // Shared messages
        //
        //**************************************************

        #region Shared messages

        /// <summary>
        /// Gets or sets shared texts used in all projects.
        /// For example: OK, Cancel, Error, etc.
        /// </summary>
        public SharedTexts Shared = new SharedTexts();

        #endregion        


        //**************************************************
        //
        // Specific messages
        //
        //**************************************************

        #region Specific messages

        public AdminHomeControllerTexts Admin = new AdminHomeControllerTexts();
        public WarehouseTexts Warehouse = new WarehouseTexts();

        #endregion

    }
}