using System.IO;
using ELFinder.Connector.Drivers.Common.Data.Volumes.Info;

namespace ELFinder.Connector.Drivers.FileSystem.Volumes.Info
{

    /// <summary>
    /// File system volume path info
    /// </summary>
    public abstract class FileSystemVolumePathInfo : VolumePathInfo<FileSystemRootVolume>
    {

        #region Properties

        /// <summary>
        /// File system info object
        /// </summary>
        public FileSystemInfo Info { get; }

        /// <summary>
        /// Relative path
        /// </summary>
        public string RelativePath { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="root">Root file system volume</param>
        /// <param name="fileSystemInfo">File system info</param>
        protected FileSystemVolumePathInfo(FileSystemRootVolume root, FileSystemInfo fileSystemInfo) 
            : base(root)
        {

            // Assign values
            Info = fileSystemInfo;

            // Check if file system info name matches the root directory full name
            if (Info.FullName.StartsWith(root.Directory.FullName))
            {
                
                // Is relative to root
                if (Info.FullName.Length <= root.Directory.FullName.Length)
                {

                    // Empty relative path
                    RelativePath = string.Empty;

                }
                else
                {

                    // Get relative path
                    RelativePath = Info.FullName.Substring(root.Directory.FullName.Length + 1);

                }

            }

        }

        #endregion

        #region Abstract methods

        /// <summary>
        /// Get directory of file system info object
        /// </summary>
        /// <returns>Result</returns>
        public abstract DirectoryInfo GetDirectory();

        /// <summary>
        /// Remove thumbnails for target path
        /// </summary>
        public abstract void RemoveThumbs();

        #endregion

        #region Methods
                
        /// <summary>
        /// Get parent directory of file system info object
        /// </summary>
        /// <returns>Result</returns>
        public DirectoryInfo GetParentDirectory()
        {

            return GetDirectory().Parent;

        }

        #endregion

    }
}