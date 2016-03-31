using System.Web.Mvc;
using System.Web.Routing;

namespace ELFinder.WebServer.ASPNet
{

    /// <summary>
    /// Route configuration
    /// </summary>
    public class RouteConfig
    {

        /// <summary>
        /// Register routes
        /// </summary>
        /// <param name="routes">Routes collection</param>
        public static void RegisterRoutes(RouteCollection routes)
        {

            // Ignore resources routes
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Commands
            routes.MapRoute("Connector", "ELFinderConnector", 
                new { controller = "ELFinderConnector", action = "Main" });

            // Thumbnails
            routes.MapRoute("Thumbnauls", "Thumbnails/{target}",
                new {controller = "ELFinderConnector", action = "Thumbnails" });

            // Index view
            routes.MapRoute("Default", "{controller}/{action}",
                new { controller = "ELFinder", action = "Index" }
            );


        }

    }
}
