using System.Data.Entity;
using System.Linq;

namespace SDM.Domain.Models
{
    /// <summary>
    /// Provides database access of <see cref="AccountModel"/>.
    /// </summary>
    public static class AccountRepository
    {
        //**************************************************
        //
        // Public methods
        //
        //**************************************************

        #region Public methods

        /// <summary>
        /// Gets account by given user name.
        /// It is case-insensitive.
        /// </summary>
        public static AccountModel GetByName(this IDbSet<AccountModel> models, string userName)
        {
            return models.SingleOrDefault(t => t.UserName.ToLower() == userName.ToLower());
        }

        /// <summary>
        /// Gets account by given user name and password.
        /// The user name is case-insensitive.
        /// </summary>
        public static AccountModel GetByNameAndPassword(this IDbSet<AccountModel> models, string userName, string password)
        {
            // get encrypted password
            string encryptedPassword = AccountModel.Encrypt(password);

            // return result
            return models.SingleOrDefault(t => t.UserName.ToLower() ==  userName.ToLower() && t.EncryptedPassword == encryptedPassword);
        }

        #endregion
    }
}
