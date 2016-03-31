using System.IO;

namespace ELFinder.Connector.Drivers.FileSystem.Volumes.Info
{
    /// <summary>
    /// File system volume file info
    /// </summary>
    public class FileSystemVolumeFileInfo : FileSystemVolumePathInfo
    {

        #region Properties

        /// <summary>
        /// File info
        /// </summary>
        public FileInfo File => Info as FileInfo;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="root">Root volume</param>
        /// <param name="fileInfo">File info</param>
        public FileSystemVolumeFileInfo(FileSystemRootVolume root, FileInfo fileInfo) : base(root, fileInfo)
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

            return File.Directory;

        }

        /// <summary>
        /// Remove thumbnails for target path
        /// </summary>
        public override void RemoveThumbs()
        {

            var thumbPath = Root.GetExistingThumbPath(File);
            if (thumbPath != null) System.IO.File.Delete(thumbPath);

        }

        #endregion

    }
}