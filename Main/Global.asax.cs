using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SDM.Localization.Core;
using SDM.Main.Localization;

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
            LocalizationManager.Initialize(this.GetType().Assembly, new LocalizationTextsRoot(), name => name.Name.StartsWith("SDM."));
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

            routes.MapRoute("Default", "{controller}/{action}/{id}",
                            new {controller = "Home", action = "Index", id = UrlParameter.Optional});
            //routes.MapRoute("Warehouse", "Warehouse/{controller}/{action}/{id}",
            //                new { action = "Index", id = UrlParameter.Optional });
        }

        private static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/css").IncludeDirectory("~/styles/", "*.css", true));
            //bundles.Add(new StyleBundle("~/bundles/js").Include("~/scripts/jquery-{version}.js"));
            //bundles.Add(new ScriptBundle("~/bundles/js")
            //    .Include("~/scripts/3rd_party/jquery/jquery-2.0.0.min.js")
            //    .IncludeDirectory("~/scripts/custom/", "*.js", true));
            //bundles.Add(new ScriptBundle("~/bundles/js").Include("~/scripts/3rd_party/jquery/jquery-2.0.0.js"));
            bundles.Add(new ScriptBundle("~/bundles/js").IncludeDirectory("~/scripts/", "*.js", true));
            BundleTable.EnableOptimizations = false;
        }


    }
}