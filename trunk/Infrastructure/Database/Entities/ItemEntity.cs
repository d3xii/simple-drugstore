namespace SDM.Infrastructure.Database.Entities
{
    /// <summary>
    /// Contains information of an item in the database.
    /// </summary>
    public class ItemEntity
    {
        /// <summary>
        /// Gets ID of the item.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets full name of the item.
        /// </summary>
        public string Name { get; set; }
    }
}
