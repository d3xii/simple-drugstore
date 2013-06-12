using SDM.Domain.Models.Common;

namespace SDM.ApplicationCore.ModelRepositories
{
    /// <summary>
    /// Provides update/delete methods to <see cref="AccountModel"/>.
    /// </summary>
    public interface IAccountRepository
    {
        /// <summary>
        /// Adds given account into repository.
        /// </summary>
        void Add(AccountModel account);

        /// <summary>
        /// Gets account by given user name.
        /// It is case-insensitive.
        /// </summary>
        AccountModel GetByName(string userName);

        /// <summary>
        /// Gets account by given user name and password.
        /// The user name is case-insensitive.
        /// </summary>
        AccountModel GetByNameAndPassword(string userName, string password);
    }
}
