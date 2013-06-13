using System.Web.Mvc;

namespace SDM.Main.Areas.App
{
    /// <summary>
    /// Register area: "/user".
    /// </summary>
    public class AppAreaRegistration : AreaRegistration
    {
        /// <summary>
        /// Gets the name of the area to register.
        /// </summary>
        /// <returns>
        /// The name of the area to register.
        /// </returns>
        public override string AreaName
        {
            get { return "App"; }
        }

        /// <summary>
        /// Registers an area in an ASP.NET MVC application using the specified area's context information.
        /// </summary>
        /// <param name="context">Encapsulates the information that is required in order to register the area.</param>
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "App_default",
                "App/{controller}/{action}/{id}",
                new
                    {
                        controller = "Home",
                        action = "Index",
                        id = UrlParameter.Optional
                    })
                .DataTokens["area"] = this.AreaName;
        }
    }
}
