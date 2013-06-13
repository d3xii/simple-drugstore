using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SDM.Core.Localization;
using SDM.Localization.Core;
using SDM.Main.Controllers;

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
            RegisterBundles(BundleTable.Bundles);

            // enable localization
            //LocalizationManager.Initialize(this.GetType().Assembly, new LocalizationTextsRoot(),
            //                               name => name.Name.StartsWith("SDM.") || name.Name.StartsWith("App_Web_"));
            LocalizationManager.Initialize(this.GetType().Assembly, new LocalizationTextsRoot());
        }

        private static void RegisterWebApiConfig(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new {id = RouteParameter.Optional});
        }

        private static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        private static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.MapRoute("Default", "{controller}/{action}/{id}",
            //                new
            //                    {
            //                        controller = "Home",
            //                        action = "Index",
            //                        id = UrlParameter.Optional
            //                    },
            //                new[] {typeof (HomeController).Namespace});
            //routes.MapRoute("Root", "", new { controller = "Home", action = "Index" }, new[] { "SDM.Main.Areas.User.Controllers" });
            routes.MapRoute("Root", "", new { controller = "Home", action = "Index" }, new[] { typeof(HomeController).Namespace });
        }

        private static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/css").IncludeDirectory("~/styles/", "*.css", true));
            bundles.Add(new ScriptBundle("~/bundles/js").IncludeDirectory("~/scripts/", "*.js", true));
            BundleTable.EnableOptimizations = false;
        }
    }
}