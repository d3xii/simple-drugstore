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

        /// <summary>
        /// Gets a value indicates whether this model is newly created.
        /// </summary>
        bool IsNew { get; }
    }
}