using System.ComponentModel.DataAnnotations;

namespace SDM.Domain.Models.Base
{
    /// <summary>
    /// Provides foundation for all entities in this application.
    /// </summary>
    public abstract class ModelBase : IModel
    {
        //**************************************************
        //
        // Public variables
        //
        //**************************************************

        #region Public variables

        /// <summary>
        /// Gets ID of the item.
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Gets a value indicates whether this model is newly created.
        /// </summary>
        public bool IsNew
        {
            get { return this.ID == IdForNewlyCreatedObject; }
        }

        #endregion


        //**************************************************
        //
        // Constructors
        //
        //**************************************************

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        protected ModelBase()
        {
            this.ID = IdForNewlyCreatedObject;
        }

        #endregion


        //**************************************************
        //
        // Constants
        //
        //**************************************************

        #region Constants

        /// <summary>
        /// Gets the default ID for newly created object.
        /// </summary>
        private const int IdForNewlyCreatedObject = -1;

        #endregion

    }
}