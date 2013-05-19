using System.IO;
using SDM.Core.Context;

namespace SDM.Core.Configuration
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
        private const string FilePath = "~/bin/datastore/config.xml";

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
        public Config Load(IServerContext context)
        {
            // get real file path
            string realFilePath = context.Server.MapPath(FilePath);
            //IOHelper.PrepareFolderContainsFile(realFilePath);

            // exists?
            if (!File.Exists(realFilePath))
            {
                // use default config                
                return new Config();
            }

            // read it            
            return Config.LoadFromFile(realFilePath);
        }

        /// <summary>
        /// Saves config to the server.
        /// </summary>
        public void Save(IServerContext context, Config config)
        {
            // get real file path
            string realFilePath = context.Server.MapPath(FilePath);
            //IOHelper.PrepareFolderContainsFile(realFilePath);

            // save it            
            config.SaveToFile(realFilePath);
        }

        #endregion
    }
}
