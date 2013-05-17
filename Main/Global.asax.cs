using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace SDM.Main
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterWebApiConfig(GlobalConfiguration.Configuration);
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        private static void RegisterWebApiConfig(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new {id = RouteParameter.Optional}
                );
        }

        private static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        private static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Default", "{controller}/{action}/{id}",
                            new {controller = "Home", action = "Index", id = UrlParameter.Optional});
            //routes.MapRoute("Warehouse", "Warehouse/{controller}/{action}/{id}",
            //                new { action = "Index", id = UrlParameter.Optional });
        }
    }
}