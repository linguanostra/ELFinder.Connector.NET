using System.Collections.Generic;

namespace ELFinder.Connector.Config.Interfaces
{

    /// <summary>
    /// ELFinder configuration interface
    /// </summary>
    public interface IELFinderConfig
    {

        #region Properties

        /// <summary>
        /// Thumbnails local storage directory
        /// </summary>
        string ThumbnailsStorageDirectory { get; }

        /// <summary>
        /// Thumbnails url
        /// </summary>
        string ThumbnailsUrl { get; }

        /// <summary>
        /// Thumbnails size
        /// </summary>
        int ThumbnailsSize { get; }

        /// <summary>
        /// Root volumes
        /// </summary>
        List<IELFinderRootVolumeConfigEntry> RootVolumes { get; }

        #endregion

    }
}