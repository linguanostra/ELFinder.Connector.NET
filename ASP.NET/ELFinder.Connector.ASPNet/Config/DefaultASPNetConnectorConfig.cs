using System.Web.Hosting;
using ELFinder.Connector.Config;

namespace ELFinder.Connector.ASPNet.Config
{

    /// <summary>
    /// Default connector config for ASP.Net
    /// </summary>
    public static class DefaultASPNetConnectorConfig
    {

        #region Static methods

        /// <summary>
        /// Create default configuration instance
        /// </summary>
        /// <returns>Result config</returns>
        public static ELFinderConfig Create()
        {

            var config = new ELFinderConfig(
                HostingEnvironment.MapPath("~/App_Data"),
                thumbnailsUrl: "Thumbnails/"
                );

            config.RootVolumes.Add(
                new ELFinderRootVolumeConfigEntry(
                    HostingEnvironment.MapPath("~/App_Data/Files"),
                    isLocked: false,
                    isReadOnly: false,
                    isShowOnly: false,
                    maxUploadSizeKb: null,      // null = Unlimited upload size
                    uploadOverwrite: true,
                    startDirectory: ""));

            return config;
        }

        #endregion

    }
}