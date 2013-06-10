﻿using System.Data.Entity;
using System.Data.SqlClient;
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
        /// Initializes new instance of <see cref="DatabaseContext"/> as well as new SQL Connection 
        /// based on given connetion string.
        /// </summary>
        public DatabaseContext(SqlConnection sqlConnection)
            : base(sqlConnection, true)
        {
        }

        #endregion

    }
}
