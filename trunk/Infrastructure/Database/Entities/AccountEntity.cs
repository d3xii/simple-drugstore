namespace SDM.Infrastructure.Database.Entities
{
    /// <summary>
    /// Contains information of an account in the database.
    /// </summary>
    public class AccountEntity : EntityBase
    {
        /// <summary>
        /// Gets or sets user name of the account.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets password of the account.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicates whether the account is enabled to use.
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}