using System.ComponentModel.DataAnnotations;

namespace SDM.Domain.Models.Base
{
    /// <summary>
    /// Provides foundation for all entities in this application.
    /// </summary>
    public abstract class ModelBase : IModel
    {
        /// <summary>
        /// Gets ID of the item.
        /// </summary>
        [Key]
        public int ID { get; set; }
    }
}