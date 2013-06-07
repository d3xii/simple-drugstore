using System.Data.Entity;
using SDM.Infrastructure.Database.Entities;

namespace SDM.Infrastructure.Database
{
    /// <summary>
    /// Implements <see cref="DbContext"/> to provides access to the database.
    /// </summary>
    public class DatabaseContext : DbContext
    {        
        //**************************************************
        //
        // Public properties
        //
        //**************************************************

        #region Public properties

        /// <summary>
        /// Gets list of items in the database.
        /// </summary>
        public DbSet<ItemEntity> Items { get; set; }

        /// <summary>
        /// Gets list of accounts in the database.
        /// </summary>
        public DbSet<AccountEntity> Accounts { get; set; }

        #endregion


        //**************************************************
        //
        // Constructors
        //
        //**************************************************

        #region Constructors

        /// <summary>
        /// Constructs a new context instance using the given string as the name or connection string for the
        ///                 database to which a connection will be made.
        ///                 See the class remarks for how this is used to create a connection.
        /// </summary>
        /// <param name="nameOrConnectionString">Either the database name or a connection string.</param>
        public DatabaseContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        #endregion

    }
}
