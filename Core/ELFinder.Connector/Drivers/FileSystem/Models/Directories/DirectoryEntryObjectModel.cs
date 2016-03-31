using System.IO;
using System.Linq;
using ELFinder.Connector.Drivers.FileSystem.Extensions;
using ELFinder.Connector.Drivers.FileSystem.Models.Directories.Common;
using ELFinder.Connector.Drivers.FileSystem.Volumes;
using ELFinder.Connector.Exceptions;
using ELFinder.Connector.Utils;
using Newtonsoft.Json;

namespace ELFinder.Connector.Drivers.FileSystem.Models.Directories
{
    /// <summary>
    /// Directory entry object model
    /// </summary>
    public class DirectoryEntryObjectModel : BaseDirectoryEntryObjectModel
    {

        #region Properties

        /// <summary>
        /// Hash of parent directory. Required except roots dirs.
        /// </summary>
        [JsonProperty(PropertyName = "phash")]
        public string ParentHash { get; set; }

        /// <summary>
        /// Directory contains child folders
        /// </summary>
        [JsonProperty(PropertyName = "dirs")]
        public bool ContainsChildDirs { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance
        /// </summary>
        protected DirectoryEntryObjectModel()
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
        public static DirectoryEntryObjectModel Create(DirectoryInfo directoryInfo, FileSystemRootVolume rootVolume)
        {

            // Get visible subdirectories
            var subDirectories = directoryInfo.GetVisibleDirectories();

            // Validate parent directory
            if (directoryInfo.Parent == null) throw new ELFinderParentDirectoryNotFoundException(directoryInfo.FullName);

            // Get parent path
            var parentPath = directoryInfo.Parent.FullName.Substring(rootVolume.Directory.FullName.Length);

            // Create new instance
            var objectModel = new DirectoryEntryObjectModel
            {
                Mime = "directory",
                ContainsChildDirs = subDirectories.Any(),
                Hash =
                    rootVolume.VolumeId +
                    UrlPathUtils.EncodePath(
                        directoryInfo.FullName.Substring(rootVolume.Directory.FullName.Length)),
                Read = true,
                Write = !rootVolume.IsReadOnly,
                Locked = rootVolume.IsLocked,
                Size = 0,
                Name = directoryInfo.Name,
                UnixTimeStamp = (long) (directoryInfo.LastWriteTimeUtc - UnixOrigin).TotalSeconds,
                ParentHash =
                    rootVolume.VolumeId +
                    UrlPathUtils.EncodePath(!string.IsNullOrEmpty(parentPath)
                        ? parentPath
                        : directoryInfo.Parent?.Name)
            };

            // Return it
            return objectModel;

        }

        #endregion

    }
}