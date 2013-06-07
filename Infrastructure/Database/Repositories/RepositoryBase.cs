using System.Data.Entity;
using System.Linq;

namespace SDM.Infrastructure.Database.Repositories
{
    /// <summary>
    /// Provides foundation for repositories that use EF as underlying ORM.
    /// </summary>
    public abstract class RepositoryBase<T>
        where T : class
    {
        //**************************************************
        //
        // Protected variables
        //
        //**************************************************

        #region Protected variables

        /// <summary>
        /// Gets database context used by this repository.
        /// </summary>
        protected readonly DatabaseContext Context;

        /// <summary>
        /// Gets set of this repository to start a new query.
        /// </summary>
        protected DbSet<T> Set
        {
            get { return this.Context.Set<T>(); }
        }

        #endregion


        //**************************************************
        //
        // Constructors
        //
        //**************************************************

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        protected RepositoryBase(DatabaseContext context)
        {
            Context = context;
        }

        #endregion        
    }
}