using System.IO;

namespace ELFinder.Connector.Drivers.FileSystem.Volumes.Info
{
    /// <summary>
    /// File system volume directory info
    /// </summary>
    public class FileSystemVolumeDirectoryInfo : FileSystemVolumePathInfo
    {

        #region Properties

        /// <summary>
        /// Directory info
        /// </summary>
        public DirectoryInfo Directory => Info as DirectoryInfo;

        #endregion

        #region Computed properties

        /// <summary>
        /// Get if directory is same as root
        /// </summary>
        public bool IsDirectorySameAsRoot => Directory.FullName == Root.Directory.FullName;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="root">Root volume</param>
        /// <param name="directoryInfo">Directory info</param>
        public FileSystemVolumeDirectoryInfo(FileSystemRootVolume root, DirectoryInfo directoryInfo) : base(root, directoryInfo)
        {
        }

        #endregion

        #region Overrides
        
        /// <summary>
        /// Get directory of file system info object
        /// </summary>
        /// <returns>Result</returns>
        public override DirectoryInfo GetDirectory()
        {

            return Directory;

        }

        /// <summary>
        /// Remove thumbnails for target path
        /// </summary>
        public override void RemoveThumbs()
        {

            var thumbPath = Root.GetExistingThumbPath(Directory);
            if (thumbPath != null) System.IO.Directory.Delete(thumbPath, true);
            
        }

        #endregion

    }
}