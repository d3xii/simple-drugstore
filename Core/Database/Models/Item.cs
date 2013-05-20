using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDM.Core.Database.Models
{
    /// <summary>
    /// Contains information of an item in the database.
    /// </summary>
    public class Item
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
