using System;
using System.Data.Entity;
using SDM.Domain.Models.Base;

namespace SDM.Domain.Models
{
    /// <summary>
    /// Provides extensions for common-used features of a repository.
    /// </summary>
    public static class RepositoryExtensions
    {
        /// <summary>
        /// Gets single object by given id.
        /// </summary>
        public static T GetById<T>(this IDbSet<T> models, int id)
            where T : class, IModel
        {
            return models.Find(id);
        }

        /// <summary>
        /// Gets single object by given id or throw <see cref="InvalidOperationException"/>.
        /// </summary>
        public static T GetByIdOrThrowException<T>(this IDbSet<T> models, int id)
            where T : class, IModel
        {
            // try to find it
            var result = models.Find(id);

            // not found
            if (result == null)
            {
                // object doesnt exist
                throw new InvalidOperationException(string.Format("Repository object {0}.#{1} not found: ", typeof(T).Name, id));
            }

            // return result
            return result;
        }

        /// <summary>
        /// Deletes given id in the database.
        /// Be noticed that the entity will be get from the database before marking it as deleted.
        /// </summary>
        public static void Remove<T>(this IDbSet<T> models, int id)
            where T : class, IModel
        {
            // try to find it
            var result = models.Find(id);

            // not found
            if (result == null)
            {
                // dothing
                return;
            }

            // delete it
            models.Remove(result);
        }
    }
}