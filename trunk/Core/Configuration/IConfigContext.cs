using SDM.Core.Context;

namespace SDM.Core.Configuration
{
    /// <summary>
    /// Contains reference to the config.
    /// </summary>
    public interface IConfigContext : IContext
    {
        /// <summary>
        /// Gets reference to the config manager.
        /// </summary>
        ConfigManager ConfigManager { get; set; }

        /// <summary>
        /// Gets direct reference to the configuration inside the ConfigManager.
        /// </summary>
        Config Config { get; set; }
    }
}