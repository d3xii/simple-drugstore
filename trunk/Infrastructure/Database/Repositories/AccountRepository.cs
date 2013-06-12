using System.Linq;
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
        public void Add(AccountModel model)
        {
            // convert to entity
            AccountEntity entity = new AccountEntity
                                       {
                                           UserName = model.UserName,
                                           Password = model.EncryptedPassword,
                                           IsEnabled = model.IsEnabled
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

        /// <summary>
        /// Gets account by given user name and password.
        /// The user name is case-insensitive.
        /// </summary>
        public AccountModel GetByNameAndPassword(string userName, string password)
        {
            // get encrypted password
            string encryptedPassword = new AccountModel().Encrypt(password);

            // return result
            return this.ConvertToModel(this.Set.SingleOrDefault(t => t.UserName == userName && t.Password == encryptedPassword));
        }

        #endregion


        //**************************************************
        //
        // Private methods
        //
        //**************************************************

        #region Private methods

        

        /// <summary>
        /// Converts entity to model.
        /// </summary>
        private AccountModel ConvertToModel(AccountEntity entity)
        {
            // return null if input is null
            if (entity == null)
            {
                return null;
            }

            return new AccountModel
                       {
                           UserName = entity.UserName,
                           EncryptedPassword = entity.Password,
                           IsEnabled = entity.IsEnabled,
                           IsAdmin = entity.IsAdmin,
                       };
        }

        #endregion

    }
}
