using System.Security.Cryptography;
using SDM.ApplicationCore.ModelRepositories;
using SDM.Domain.Models.Common;
using SDM.Infrastructure.Database.Entities;

namespace SDM.Infrastructure.Database.Repositories
{
    /// <summary>
    /// Provides methods to save/load <see cref="AccountModel"/>.
    /// </summary>
    public class AccountRepository : RepositoryBase<AccountEntity>, IAccountRepository
    {
        //**************************************************
        //
        // Constructors
        //
        //**************************************************

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public AccountRepository(DatabaseContext context)
            : base(context)
        {
        }

        #endregion


        #region Implementation of IAccountRepository

        /// <summary>
        /// Adds given account into repository.
        /// </summary>
        public void Add(AccountModel account)
        {
            // convert to entity
            AccountEntity entity = new AccountEntity
                                       {
                                           UserName = account.UserName,
                                           Password = this.Encrypt(account.Password),
                                           IsEnabled = account.IsEnabled
                                       };

            // add to database
            this.Set.Attach(entity);
        }

        #endregion


        //**************************************************
        //
        // Private methods
        //
        //**************************************************

        #region Private methods

        /// <summary>
        /// Encrypts given text.
        /// Very simple encryption.
        /// </summary>        
        private string Encrypt(string text)
        {
            // credit: Oli, http://stackoverflow.com/a/212526/633428
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.ASCII.GetBytes(text);
            data = md5.ComputeHash(data);
            return System.Text.Encoding.ASCII.GetString(data);
        }

        #endregion

    }
}
