using System.Web;

namespace SDM.Core.Context
{
    /// <summary>
    /// Implements <see cref="IServerContext"/>.
    /// </summary>
    public class ServerContext : IServerContext
    {
        #region Implementation of IServerContext

        /// <summary>
        /// Gets reference to the HTTP Server.
        /// </summary>
        public HttpServerUtilityBase Server { get; set; }

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
        public ServerContext(HttpServerUtilityBase server)
        {
            Server = server;
        }

        #endregion

    }
}