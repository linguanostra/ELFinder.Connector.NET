using System;
using System.IO;
using ELFinder.Connector.Config;

namespace ELFinder.Connector.Nancy.Config
{

    /// <summary>
    /// Default connector config for NancyFX
    /// </summary>
    public static class DefaultNancyConnectorConfig
    {

        #region Static methods

        /// <summary>
        /// Create default configuration instance
        /// </summary>
        /// <returns>Result config</returns>
        public static ELFinderConfig Create()
        {

            var config = new ELFinderConfig(
                Environment.CurrentDirectory,
                thumbnailsUrl: "Thumbnails/"
                );

            config.RootVolumes.Add(
                new ELFinderRootVolumeConfigEntry(
                    Environment.CurrentDirectory,
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