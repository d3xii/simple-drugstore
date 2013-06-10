namespace SDM.ApplicationServices.Configuration
{
    /// <summary>
    /// Provides services to process <see cref="ConfigModel"/>.
    /// </summary>
    public class ConfigServices
    {
        /// <summary>
        /// Copies values from source to target. 
        /// Ignores empty password.
        /// Returns target reference.
        /// </summary>
        public ConfigModel CopyValues(ConfigModel source, ConfigModel target)
        {
            source.CopyValues(target);
            return target;
        }

        /// <summary>
        /// Checks whether the admin password was changed.
        /// </summary>
        public bool IsAdminPasswordChanged(ConfigModel source, ConfigModel target)
        {
            return source.AdminPassword != null && source.AdminPassword != target.AdminPassword;
        }
    }
}