using System.Collections.Generic;
using System.Xml.Serialization;
using SDM.Localization.Core;

namespace SDM.Core.Localization
{
    /// <summary>
    /// Contains the root to localization keys & values.
    /// </summary>
    [XmlRoot("Localization")]
    public class LocalizationTextsRoot : ILocalizationRoot
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


        ////**************************************************
        ////
        //// Specific messages
        ////
        ////**************************************************

        //#region Specific messages

        //public AdminHomeControllerTexts AdminController = new AdminHomeControllerTexts();
        //public HomeIndexViewTexts HomeView = new HomeIndexViewTexts();
        //public HomeControllerTexts HomeController = new HomeControllerTexts();

        //#endregion


        //**************************************************
        //
        // Extended messages
        //
        //**************************************************

        #region Extended messages

        /// <summary>
        /// Gets reference to the shared text.
        /// </summary>
        ISharedTexts ILocalizationRoot.SharedText { get { return this.Shared; } }

        /// <summary>
        /// Gets available translatation scopes.
        /// </summary>
        public List<ILocalizationScope> Scopes { get; private set; }

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
        public LocalizationTextsRoot()
        {
            this.Shared = new SharedTexts();
            this.Scopes = new List<ILocalizationScope>();
        }

        #endregion

    }
}