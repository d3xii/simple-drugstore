using System.Web;

namespace SDM.Core.Context
{
    /// <summary>
    /// Contains reference to the HTTP server config.
    /// </summary>
    public interface IServerContext : IContext
    {
        /// <summary>
        /// Gets reference to the HTTP Server.
        /// </summary>
        HttpServerUtilityBase Server { get; set; }
    }
}