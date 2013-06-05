using System.IO;
using SDM.ApplicationCore;

namespace SDM.ApplicationServices.Configuration
{
    /// <summary>
    /// Provides methods to load <see cref="ConfigModel"/>.
    /// </summary>
    public class ConfigRepository
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

        /// <summary>
        /// Loads config used by the server.
        /// </summary>
        public ConfigModel Load(IFileAccessProvider fileAccessProvider)
        {
            // read content of the file
            Stream stream = fileAccessProvider.GetFileStream(FilePath);

            // create default if the file does not exist
            if (stream == null || stream.Length == 0)
            {
                // use default config
                return new ConfigFactory().Create();
            }

            // parse it
            return ConfigModel.LoadFromStream(stream);
        }

        /// <summary>
        /// Saves config to the server.
        /// </summary>
        public void Save(ConfigModel config, IFileAccessProvider fileAccessProvider)
        {
            // get file stream
            Stream stream = fileAccessProvider.CreateNewFileStream(FilePath);

            // save it            
            config.SaveToStream(stream);
        }
    }
}