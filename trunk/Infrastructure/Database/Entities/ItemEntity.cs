using System.ComponentModel.DataAnnotations.Schema;

namespace SDM.Infrastructure.Database.Entities
{
    /// <summary>
    /// Contains information of an item in the database.
    /// </summary>
    [Table("Item")]
    public class ItemEntity : EntityBase
    {
        /// <summary>
        /// Gets or sets full name of the item.
        /// </summary>
        public string Name { get; set; }
    }
}
