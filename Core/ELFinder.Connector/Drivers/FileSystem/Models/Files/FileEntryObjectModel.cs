using System.IO;
using ELFinder.Connector.Drivers.FileSystem.Models.Files.Common;
using ELFinder.Connector.Drivers.FileSystem.Volumes;
using ELFinder.Connector.Utils;

namespace ELFinder.Connector.Drivers.FileSystem.Models.Files
{
    /// <summary>
    /// File entry object model
    /// </summary>
    public class FileEntryObjectModel : BaseFileEntryObjectModel
    {

        #region Static methods

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="fileInfo">File info</param>
        /// <param name="rootVolume">Root volume</param>
        /// <returns>Result instance</returns>
        public static FileEntryObjectModel Create(FileInfo fileInfo, FileSystemRootVolume rootVolume)
        {

            // Get paths
            var parentPath = fileInfo.Directory?.FullName.Substring(rootVolume.Directory.FullName.Length);
            var relativePath = fileInfo.FullName.Substring(rootVolume.Directory.FullName.Length);

            // Create new instance
            var objectModel = new FileEntryObjectModel
            {
                Read = true,
                Write = !rootVolume.IsReadOnly,
                Locked = rootVolume.IsLocked,
                Name = fileInfo.Name,
                Size = fileInfo.Length,
                UnixTimeStamp = (long) (fileInfo.LastWriteTimeUtc - UnixOrigin).TotalSeconds,
                Mime = UrlPathUtils.GetMimeType(fileInfo),
                Hash = rootVolume.VolumeId + UrlPathUtils.EncodePath(relativePath),
                ParentHash =
                    rootVolume.VolumeId +
                    UrlPathUtils.EncodePath(!string.IsNullOrEmpty(parentPath) ? parentPath : fileInfo.Directory?.Name)
            };

            // Return it
            return objectModel;

        }

        #endregion

    }
}