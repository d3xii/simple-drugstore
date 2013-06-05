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
        // Dependencies
        //
        //**************************************************

        #region Dependencies

        /// <summary>
        /// Gets access provider used in this instance.
        /// </summary>
        private readonly IFileAccessProvider _accessProvider;

        #endregion


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
        // Constructors
        //
        //**************************************************

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public ConfigRepository(IFileAccessProvider accessProvider)
        {
            _accessProvider = accessProvider;
        }

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
        public ConfigModel Load()
        {
            // read content of the file
            Stream stream = _accessProvider.GetFileStream(FilePath);

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
        public void Save(ConfigModel config)
        {
            // get file stream
            Stream stream = _accessProvider.CreateNewFileStream(FilePath);

            // save it            
            config.SaveToStream(stream);
        }

        #endregion

    }
}