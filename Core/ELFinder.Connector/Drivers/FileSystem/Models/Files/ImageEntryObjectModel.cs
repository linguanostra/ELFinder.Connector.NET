using System.IO;
using ELFinder.Connector.Drivers.FileSystem.Models.Files.Common;
using ELFinder.Connector.Drivers.FileSystem.Volumes;
using ELFinder.Connector.Utils;
using Newtonsoft.Json;

namespace ELFinder.Connector.Drivers.FileSystem.Models.Files
{
    /// <summary>
    /// Image entry object model
    /// </summary>
    public class ImageEntryObjectModel : BaseFileEntryObjectModel
    {

        #region Properties

        /// <summary>
        /// Thumbnail file name
        /// </summary>
        [JsonProperty(PropertyName = "tmb")]
        public string Tmb { get; set; }

        /// <summary>
        /// File dimensions
        /// </summary>
        [JsonProperty(PropertyName = "dim")]
        public string Dimension { get; set; }

        #endregion

        #region Static methods

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="fileInfo">File info</param>
        /// <param name="rootVolume">Root volume</param>
        /// <returns>Result instance</returns>
        public static ImageEntryObjectModel Create(FileInfo fileInfo, FileSystemRootVolume rootVolume)
        {

            // Get paths
            var parentPath = fileInfo.Directory?.FullName.Substring(rootVolume.Directory.FullName.Length);
            var relativePath = fileInfo.FullName.Substring(rootVolume.Directory.FullName.Length);

            // Get image size
            var imageSize = ImagingUtils.GetImageSize(fileInfo.FullName);

            // Create new instance
            var objectModel = new ImageEntryObjectModel
            {
                Read = true,
                Write = !rootVolume.IsReadOnly,
                Locked = rootVolume.IsLocked,
                Name = fileInfo.Name,
                Size = fileInfo.Length,
                UnixTimeStamp = (long)(fileInfo.LastWriteTimeUtc - UnixOrigin).TotalSeconds,
                Mime = UrlPathUtils.GetMimeType(fileInfo),
                Hash = rootVolume.VolumeId + UrlPathUtils.EncodePath(relativePath),
                ParentHash =
                    rootVolume.VolumeId +
                    UrlPathUtils.EncodePath(!string.IsNullOrEmpty(parentPath) ? parentPath : fileInfo.Directory?.Name),
                Tmb = rootVolume.GetExistingThumbHash(fileInfo) ?? "",
                Dimension = $"{imageSize.Width}x{imageSize.Height}"
        };

            // Return it
            return objectModel;

        }

        #endregion

    }
}