using System.ComponentModel;
using MHTools.Common;

namespace SDM.Core.Configuration
{
    /// <summary>
    /// Contains application config.
    /// </summary>
    public class Config : DataContractObject<Config>
    {
        /// <summary>
        /// Gets or sets SQL config.
        /// </summary>
        public SqlConfig Sql { get; set; }

        /// <summary>
        /// Gets or sets admin password.
        // </summary>        
        [DisplayName("Admin Password"), DefaultValue("admin"), PasswordPropertyText(true)]
        public string AdminPassword { get; set; }
    }
}
