using System.IO;
using System.Linq;
using ELFinder.Connector.Drivers.FileSystem.Extensions;
using ELFinder.Connector.Drivers.FileSystem.Models.Directories.Common;
using ELFinder.Connector.Drivers.FileSystem.Volumes;
using ELFinder.Connector.Utils;
using Newtonsoft.Json;

namespace ELFinder.Connector.Drivers.FileSystem.Models.Directories
{

    /// <summary>
    /// Root directory entry object model
    /// </summary>
    public class RootDirectoryEntryObjectModel : BaseDirectoryEntryObjectModel
    {

        #region Properties

        /// <summary>
        /// Volume Id
        /// </summary>
        [JsonProperty(PropertyName = "volumeId")]
        public string VolumeId { get; set; }

        /// <summary>
        /// Contains directories
        /// </summary>
        [JsonProperty(PropertyName = "dirs")]
        public bool Dirs { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance
        /// </summary>
        protected RootDirectoryEntryObjectModel()
        {
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="directoryInfo">Directory path info</param>
        /// <param name="rootVolume">Root volume</param>
        /// <returns>Result instance</returns>
        public static RootDirectoryEntryObjectModel Create(DirectoryInfo directoryInfo, FileSystemRootVolume rootVolume)
        {

            // Get visible subdirectories
            var subDirectories = directoryInfo.GetVisibleDirectories();

            // Create new instance
            var objectModel = new RootDirectoryEntryObjectModel
            {
                Mime = "directory",
                Dirs = subDirectories.Any(),
                Hash = rootVolume.VolumeId + UrlPathUtils.EncodePath(directoryInfo.Name),
                Read = true,
                Write = !rootVolume.IsReadOnly,
                Locked = rootVolume.IsLocked,
                Name = rootVolume.Alias,
                Size = 0,
                UnixTimeStamp = (long) (directoryInfo.LastWriteTimeUtc - UnixOrigin).TotalSeconds,
                VolumeId = rootVolume.VolumeId
            };

            // Return it
            return objectModel;

        }

        #endregion

    }
}