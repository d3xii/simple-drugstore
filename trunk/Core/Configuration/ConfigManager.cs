using System;
using System.ComponentModel;
using System.IO;
using MHTools.Common;
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
                var result = new Config();
                this.SetDefaultValues(result);
                return result;
            }

            // read it            
            return IOHelper.DeserializeAsXml<Config>(realFilePath);
        }

        #endregion


        //**************************************************
        //
        // Private methods
        //
        //**************************************************

        #region Private methods

        /// <summary>
        /// Sets default values for given objects and its chidlren.
        /// This will Stackoverflow exception if there is circular dependency in the object.
        /// </summary>
        public void SetDefaultValues(object obj)
        {
            // for each property
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(obj))
            {
                // reset its value
                property.ResetValue(obj);

                // get its value
                object value = property.GetValue(obj);

                // if value is complex type
                if (!(value is ValueType) && property.PropertyType != typeof(string))
                {
                    // if it is null
                    if (value == null)
                    {          
                        // try to initialize it
                        value = Activator.CreateInstance(property.PropertyType);
                        property.SetValue(obj, value);
                    }

                    // move deeper
                    SetDefaultValues(value);
                }
            }
        }

        #endregion

    }
}
