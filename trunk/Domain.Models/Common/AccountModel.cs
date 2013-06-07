namespace SDM.Domain.Models.Common
{
    /// <summary>
    /// Represents domain model: Common.Account.
    /// </summary>
    public class AccountModel
    {
        /// <summary>
        /// Gets or sets user name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets encrypted password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicates whether the account is enabled to use.
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}
