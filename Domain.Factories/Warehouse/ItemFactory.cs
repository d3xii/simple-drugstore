using SDM.Domain.Models.Warehouse;

namespace SDM.Domain.Factories.Warehouse
{
    /// <summary>
    /// Provides methods to create an <see cref="ItemModel"/>.
    /// </summary>
    public class ItemFactory
    {
        /// <summary>
        /// Creates new <see cref="ItemModel"/>.
        /// </summary>
        public ItemModel Create()
        {
            return new ItemModel();
        }
    }
}
