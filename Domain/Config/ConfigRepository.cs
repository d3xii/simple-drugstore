using System.IO;
using System.Runtime.Serialization;
using SDM.Core;

namespace SDM.Domain.Config
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
            using (Stream stream = _accessProvider.GetFileStream(FilePath))
            {
                if (stream == null || stream.Length == 0)
                {
                    // use default config
                    return new ConfigModel();
                }

                // parse it
                try
                {
                    return ConfigModel.LoadFromStream(stream);
                }
                catch (SerializationException)
                {
                    // use default config
                    return new ConfigModel();
                }
            }
        }

        /// <summary>
        /// Saves config to the server.
        /// </summary>
        public void Save(ConfigModel config)
        {
            // get file stream
            using (Stream stream = _accessProvider.CreateNewFileStream(FilePath))
            {
                config.SaveToStream(stream);
            }
        }

        #endregion

    }
}