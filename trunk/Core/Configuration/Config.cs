﻿using System.ComponentModel;

namespace SDM.Core.Configuration
{
    /// <summary>
    /// Contains application config.
    /// </summary>
    public class Config
    {
        /// <summary>
        /// Gets or sets SQL config.
        /// </summary>
        public SqlConfig Sql { get; set; }

        /// <summary>
        /// Gets or sets admin password.
        /// </summary>
        [DisplayName("Admin Password"), DefaultValue("admin")]
        public string AdminPassword { get; set; }
    }
}