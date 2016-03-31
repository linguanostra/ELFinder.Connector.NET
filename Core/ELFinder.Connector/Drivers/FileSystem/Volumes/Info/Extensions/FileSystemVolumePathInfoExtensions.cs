using ELFinder.Connector.Drivers.FileSystem.Models.Directories;
using ELFinder.Connector.Drivers.FileSystem.Models.Directories.Common;
using ELFinder.Connector.Utils;

namespace ELFinder.Connector.Drivers.FileSystem.Volumes.Info.Extensions
{

    /// <summary>
    /// File system volume path info extensions
    /// </summary>
    public static class FileSystemVolumePathInfoExtensions
    {

        #region Extension methods

        /// <summary>
        /// Create target entry object model for given directory
        /// </summary>
        /// <param name="fullPath">Target directory path info</param>
        /// <returns>Result target entry object model</returns>
        public static BaseDirectoryEntryObjectModel CreateTargetEntryObjectModel(
            this FileSystemVolumeDirectoryInfo fullPath)
        {

            // Check if we are targeting the root directory
            if (fullPath.IsDirectorySameAsRoot)
            {

                // Result instance as root
                return RootDirectoryEntryObjectModel.Create(fullPath.Directory, fullPath.Root);

            }

            // Result instance as directory
            return DirectoryEntryObjectModel.Create(fullPath.Directory, fullPath.Root);

        }

        /// <summary>
        /// Get if file system volume path info refers to a directory
        /// </summary>
        /// <param name="info">Info</param>
        /// <returns>True/False, based on result</returns>
        public static bool IsDirectory(this FileSystemVolumePathInfo info)
        {

            return info is FileSystemVolumeDirectoryInfo;

        }

        /// <summary>
        /// Get if file system volume path info refers to a file
        /// </summary>
        /// <param name="info">Info</param>
        /// <returns>True/False, based on result</returns>
        public static bool IsFile(this FileSystemVolumePathInfo info)
        {

            return info is FileSystemVolumeFileInfo;

        }

        /// <summary>
        /// Get if file system volume path info refers to an image
        /// </summary>
        /// <param name="info">Info</param>
        /// <returns>True/False, based on result</returns>
        public static bool IsImage(this FileSystemVolumePathInfo info)
        {

            return info.IsFile()
                && ImagingUtils.CanProcessFile(info.Info.Extension);

        }

        /// <summary>
        /// Get if file system volume path info refers to an existing entry
        /// </summary>
        /// <param name="info">Info</param>
        /// <returns>True/False, based on result</returns>
        public static bool Exists(this FileSystemVolumePathInfo info)
        {

            return info.Info.Exists;

        }

        #endregion

    }
}