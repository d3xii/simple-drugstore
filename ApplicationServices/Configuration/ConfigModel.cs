using System;
using System.ComponentModel;
using SDM.ApplicationCore;

namespace SDM.ApplicationServices.Configuration
{
    /// <summary>
    /// Contains application config.
    /// </summary>
    public class ConfigModel : DataContractObject<ConfigModel>
    {
        /// <summary>
        /// Gets or sets SQL config.
        /// </summary>
        public SqlConfigModel Sql { get; set; }

        /// <summary>
        /// Gets or sets admin password.
        /// </summary>        
        [DisplayName("Admin Password"), DefaultValue("admin"), PasswordPropertyText(true)]
        public string AdminPassword { get; set; }
    }
}
