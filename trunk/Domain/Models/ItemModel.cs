using System.ComponentModel.DataAnnotations.Schema;
using SDM.Domain.Models.Base;

namespace SDM.Domain.Models
{
    /// <summary>
    /// Contains information of an item in the database.
    /// </summary>
    [Table("Item")]
    public class ItemModel : ModelBase
    {
        /// <summary>
        /// Gets or sets full name of the item.
        /// </summary>
        public string Name { get; set; }
    }
}
