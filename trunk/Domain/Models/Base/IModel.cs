namespace SDM.Domain.Models.Base
{
    /// <summary>
    /// Defines general properties of a model.
    /// </summary>
    public interface IModel
    {
        /// <summary>
        /// Gets ID of the item.
        /// </summary>
        int ID { get; set; }
    }
}