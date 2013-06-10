using System;
using System.Linq;
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
            this.Set.Add(entity);
        }

        /// <summary>
        /// Gets account by given user name.
        /// It is case-insensitive.
        /// </summary>
        public AccountModel GetByName(string userName)
        {
            return this.ConvertToModel(this.Set.SingleOrDefault(t => t.UserName == userName));
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
            return Convert.ToBase64String(data);
        }

        /// <summary>
        /// Converts entity to model.
        /// </summary>
        private AccountModel ConvertToModel(AccountEntity entity)
        {
            return new AccountModel
                       {
                           UserName = entity.UserName,
                           Password = entity.Password,
                           IsEnabled = entity.IsEnabled,
                       };
        }

        #endregion

    }
}
