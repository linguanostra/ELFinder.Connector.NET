using ELFinder.Connector.Nancy.Bootstrap;
using Nancy.Conventions;

namespace ELFinder.WebServer.Nancy.Bootstrap
{

    /// <summary>
    /// ELFinder Nancy bootstrapper
    /// </summary>
    public class ELFinderNancyBootstrapper : ELFinderBaseNancyBootstrapper
    {

        #region Overrides

        /// <summary>
        /// Overrides/configures Nancy's conventions
        /// </summary>
        /// <param name="nancyConventions">Convention object instance</param>
        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {

            // Call base method
            base.ConfigureConventions(nancyConventions);

            // Add static content
            nancyConventions.StaticContentsConventions.AddDirectory("/css", "/Content/css/");
            nancyConventions.StaticContentsConventions.AddDirectory("/js", "/Content/js/");
            nancyConventions.StaticContentsConventions.AddDirectory("/img", "/Content/img/");
            nancyConventions.StaticContentsConventions.AddDirectory("/sounds", "/Content/sounds/");

        }

        #endregion

    }
}