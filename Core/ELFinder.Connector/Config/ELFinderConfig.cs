using System.Collections.Generic;
using ELFinder.Connector.Config.Interfaces;

namespace ELFinder.Connector.Config
{

    /// <summary>
    /// ELFinder configuration
    /// </summary>
    public class ELFinderConfig : IELFinderConfig
    {

        #region Properties

        /// <summary>
        /// Thumbnails local storage directory
        /// </summary>
        public string ThumbnailsStorageDirectory { get; }

        /// <summary>
        /// Thumbnails url
        /// </summary>
        public string ThumbnailsUrl { get; }

        /// <summary>
        /// Thumbnails size
        /// </summary>
        public int ThumbnailsSize { get; }

        /// <summary>
        /// Root volumes
        /// </summary>
        public List<IELFinderRootVolumeConfigEntry> RootVolumes { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="thumbnailsStorageDirectory">Thumbnails storage directory</param>
        /// <param name="thumbnailsUrl">Thumbnails url</param>
        /// <param name="thumbnailsSize">Thumbnails size</param>
        public ELFinderConfig(string thumbnailsStorageDirectory, string thumbnailsUrl, int thumbnailsSize = 48)
        {
            ThumbnailsStorageDirectory = thumbnailsStorageDirectory?.TrimEnd('/');
            ThumbnailsUrl = thumbnailsUrl;
            ThumbnailsSize = thumbnailsSize;
            RootVolumes = new List<IELFinderRootVolumeConfigEntry>();
        }

        #endregion

    }
}