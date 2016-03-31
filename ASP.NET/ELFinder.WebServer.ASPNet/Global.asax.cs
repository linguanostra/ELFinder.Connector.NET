using System.Web.Mvc;
using System.Web.Routing;
using ELFinder.Connector.ASPNet.ModelBinders;
using ELFinder.Connector.Config;
using ELFinder.WebServer.ASPNet.Config;

namespace ELFinder.WebServer.ASPNet
{

    /// <summary>
    /// Main application
    /// </summary>
    public class MvcApplication : System.Web.HttpApplication
    {

        #region Methods

        /// <summary>
        /// Application start
        /// </summary>
        protected void Application_Start()
        {

            // Standard registrations
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // Use custom model binder
            ModelBinders.Binders.DefaultBinder = new ELFinderModelBinder();

            // Initialize ELFinder configuration
            InitELFinderConfiguration();

        }

        /// <summary>
        /// Initialize ELFinder configuration
        /// </summary>
        protected void InitELFinderConfiguration()
        {

            SharedConfig.ELFinder = new ELFinderConfig(
                Server.MapPath("~/App_Data"),
                thumbnailsUrl: "Thumbnails/"
                );

            SharedConfig.ELFinder.RootVolumes.Add(
                new ELFinderRootVolumeConfigEntry(
                    Server.MapPath("~/App_Data/Files"),
                    isLocked: false,
                    isReadOnly: false,
                    isShowOnly: false,
                    maxUploadSizeKb: 0,
                    uploadOverwrite: true,
                    startDirectory: ""));

        }

        #endregion

    }
}
