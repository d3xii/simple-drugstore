namespace SDM.ApplicationServices.Configuration
{
    /// <summary>
    /// Provides methods to create <see cref="ConfigModel"/>.
    /// </summary>
    public class ConfigFactory
    {
        /// <summary>
        /// Creates new <see cref="ConfigModel"/>.
        /// </summary>
        public ConfigModel Create()
        {
            return new ConfigModel().ResetValues();
        }
    }
}
