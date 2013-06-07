using System.ComponentModel.DataAnnotations;

namespace SDM.Infrastructure.Database.Entities
{
    /// <summary>
    /// Provides foundation for all entities in this application.
    /// </summary>
    public abstract class EntityBase : IEntity
    {
        /// <summary>
        /// Gets ID of the item.
        /// </summary>
        [Key]
        public int ID { get; set; }
    }
}