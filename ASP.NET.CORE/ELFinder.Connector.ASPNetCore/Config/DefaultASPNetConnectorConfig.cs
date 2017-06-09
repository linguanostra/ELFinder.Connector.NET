using ELFinder.Connector.Config;
using System.IO;

namespace ELFinder.Connector.ASPNetCore.Config
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
                Directory.GetCurrentDirectory() + @"\App_Data",
                //HostingEnvironment.MapPath("~/App_Data"),
                thumbnailsUrl: "Thumbnails/"
                );

            config.RootVolumes.Add(
                new ELFinderRootVolumeConfigEntry(
                    Directory.GetCurrentDirectory() + @"~\App_Data",
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