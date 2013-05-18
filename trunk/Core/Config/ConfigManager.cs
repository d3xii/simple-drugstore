using MHTools.Common;

namespace SDM.Core.Config
{
    /// <summary>
    /// Provides methods to load/save config.
    /// </summary>
    public class ConfigManager
    {
        //**************************************************
        //
        // Constants
        //
        //**************************************************

        #region Constants

        /// <summary>
        /// Gets the filepath to the config file on the server.
        /// </summary>
        private const string FilePath = "datastore\\config.xml";

        #endregion


        //**************************************************
        //
        // Public methods
        //
        //**************************************************

        #region Public methods

        /// <summary>
        /// Loads config used by the server.
        /// </summary>
        public Config Load()
        {
            IOHelper.DeserializeAsXml<Config>("");
        }

        #endregion

    }
}
