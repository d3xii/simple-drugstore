using SDM.Domain.Models.Common;

namespace SDM.Domain.Factories.Common
{
    /// <summary>
    /// Provides methods to create an <see cref="AccountModel"/>.
    /// </summary>
    public class AccountFactory
    {
        /// <summary>
        /// Creates new <see cref="AccountModel"/>.
        /// </summary>
        public AccountModel Create(string userName, string password)
        {
            return new AccountModel
                       {
                           UserName = userName,
                           Password = password,                           
                           IsEnabled = true,
                       };
        }
    }
}
