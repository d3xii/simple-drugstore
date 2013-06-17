using System.Data.Entity;
using System.Linq;

namespace SDM.Domain.Models
{
    /// <summary>
    /// Provides database access of <see cref="ItemModel"/>.
    /// </summary>
    public static class ItemRepository
    {
        //**************************************************
        //
        // Public methods
        //
        //**************************************************

        #region Public methods

        /// <summary>
        /// Checks whether given name existed in the database.
        /// The user name is case-insensitive.
        /// This method has been optimized.
        /// </summary>
        public static bool IsExisted(this IDbSet<ItemModel> models, string name)
        {
            return models.Any(t => t.Name.ToLower() == name.ToLower());
        }

        /// <summary>
        /// Gets model by given user name.
        /// The user name is case-insensitive.
        /// </summary>
        public static ItemModel GetByName(this IDbSet<ItemModel> models, string name)
        {
            return models.SingleOrDefault(t => t.Name.ToLower() == name.ToLower());
        }

        #endregion
    }
}
