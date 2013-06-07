using System.Xml.Serialization;
using SDM.Localization.Core;
using SDM.Localization.SharedTexts;
using SDM.Main.Areas.Admin.Controllers;
using SDM.Main.Controllers;
using SDM.Main.Views.Home;

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

        public AdminHomeControllerTexts AdminController = new AdminHomeControllerTexts();
        public HomeIndexViewTexts HomeView = new HomeIndexViewTexts();
        public HomeControllerTexts HomeController = new HomeControllerTexts();

        #endregion

    }
}