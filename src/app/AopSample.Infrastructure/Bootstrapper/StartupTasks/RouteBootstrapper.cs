using System.Web.Mvc;
using System.Web.Routing;
using Seterlund.CodeGuard;

namespace AopSample.Infrastructure.Bootstrapper.StartupTasks
{
    public class RouteBootstrapper : IRunTaskAtStartup
    {
        public void Execute()
        {
            RouteTable.Routes.Clear();
            RegisterRoutes(RouteTable.Routes);
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            Guard.That(() => routes).IsNotNull();

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("favicon.ico");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new {controller = "Home", action = "Index", id = UrlParameter.Optional} // Parameter defaults
                );
        }
    }
}
