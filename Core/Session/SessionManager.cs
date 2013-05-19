using System.Web;

namespace SDM.Core.Session
{
    /// <summary>
    /// Provides extension methods to get or create strong-typed session data from ASP.NET session.
    /// </summary>
    public static class SessionManager
    {
        //**************************************************
        //
        // Public methods
        //
        //**************************************************

        #region Public methods

        /// <summary>
        /// Creates new instance of the session.
        /// If it already exists, the existing one will be overwritten.
        /// </summary>
        public static T Create<T>(this HttpSessionStateBase session) where T : class, new()
        {
            // create new object
            T result = new T();

            // save it
            session[GetKey<T>()] = result;

            // return result
            return result;
        }

        /// <summary>
        /// Gets existing instance of the session.
        /// If it does not exist, a new one will be created.
        /// </summary>
        public static T GetOrCreate<T>(this HttpSessionStateBase session) where T : class, new()
        {
            // try to get it
            T result = (T) session[GetKey<T>()];

            // if not created
            if (result == null)
            {
                // create new one
                result = new T();

                // save it
                session[GetKey<T>()] = result;
            }

            // return result
            return result;
        }

        #endregion


        //**************************************************
        //
        // Private methods
        //
        //**************************************************

        #region Private methods

        /// <summary>
        /// Gets string key of given type.
        /// </summary>
        private static string GetKey<T>()
        {
            return typeof (T).FullName;
        }

        #endregion

    }
}
