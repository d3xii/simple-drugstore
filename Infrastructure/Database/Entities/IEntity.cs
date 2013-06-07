namespace SDM.Infrastructure.Database.Entities
{
    /// <summary>
    /// Defines general properties of an entity.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Gets ID of the item.
        /// </summary>
        int ID { get; set; }
    }
}