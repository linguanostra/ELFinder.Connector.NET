using System;
using System.IO;
using ELFinder.Connector.Drivers.Common.Data.Results;
using ELFinder.Connector.Drivers.Common.Data.Volumes;
using ELFinder.Connector.Drivers.FileSystem.Constants;
using ELFinder.Connector.Drivers.FileSystem.Utils;
using ELFinder.Connector.Drivers.FileSystem.Volumes.Info;
using ELFinder.Connector.Utils;

namespace ELFinder.Connector.Drivers.FileSystem.Volumes
{

    /// <summary>
    /// File system root volume
    /// </summary>
    public class FileSystemRootVolume : RootVolume
    {

        #region Properties

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Root directory
        /// </summary>
        public DirectoryInfo Directory { get; set; }

        /// <summary>
        /// Start directory
        /// </summary>
        public DirectoryInfo StartDirectory { get; set; }

        /// <summary>
        /// Maximum upload file size. This size is per files in kilobytes. 
        /// </summary>
        public int? MaxUploadSizeKb { get; set; }

        /// <summary>
        /// Files on upload will replace or give them new names. 
        /// True: Replace old files. 
        /// False: Give new names like original_name-number.ext
        /// </summary>
        public bool UploadOverwrite { get; set; }

        /// <summary>
        /// Thumbnails size
        /// </summary>
        public int ThumbnailsSize { get; set; }

        /// <summary>
        /// Thumbnals url
        /// </summary>
        public string ThumbnailsUrl { get; set; }

        /// <summary>
        /// Directory for storing all thumbnails.
        /// </summary>
        public DirectoryInfo ThumbnailsDirectory { get; protected set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="directory">Directory</param>
        /// <param name="thumbnailsRootDirectory">Thumbnails root directory</param>
        /// <param name="url">Url</param>
        protected FileSystemRootVolume(DirectoryInfo directory, DirectoryInfo thumbnailsRootDirectory, string url = null)
        {

            // Assign values
            Directory = directory;
            Alias = directory.Name;
            Url = url;
            UploadOverwrite = true;
            ThumbnailsSize = 48;

            // Initialize volume thumbnails storage
            InitThumbnailsStorage(thumbnailsRootDirectory);

        }

        #endregion

        #region Methods

        #region Public

        /// <summary>
        /// Get existing thumbnails hash for file
        /// </summary>
        /// <param name="fileInfo">File info</param>
        /// <returns>Result</returns>
        public string GetExistingThumbHash(FileInfo fileInfo)
        {

            // Get thumbnails path
            var thumbPath = GetExistingThumbPath(fileInfo);
            if (thumbPath == null) return null;

            // Get result paths
            var relativePath = thumbPath.Substring(this.GetThumbnailStoragePath().Length);
            return VolumeId + UrlPathUtils.EncodePath(relativePath);

        }

        /// <summary>
        /// Get existing thumbnails path for file
        /// </summary>
        /// <param name="fileInfo">File info</param>
        /// <returns>Result</returns>
        public string GetExistingThumbPath(FileInfo fileInfo)
        {
            var thumbPath = GenerateThumbPath(fileInfo);
            return thumbPath != null && File.Exists(thumbPath) ? thumbPath : null;
        }

        /// <summary>
        /// Get existing thumbnails path for directory
        /// </summary>
        /// <param name="directoryInfo">Directory info</param>
        /// <returns>Result</returns>
        public string GetExistingThumbPath(DirectoryInfo directoryInfo)
        {

            // Validate thumbnails config
            if (string.IsNullOrEmpty(GetThumbnailStoragePath()))                
            {
                return null;
            }

            var relativePath = directoryInfo.FullName.Substring(Directory.FullName.Length);
            var thumbDir = GetThumbnailStoragePath() + relativePath;
            return System.IO.Directory.Exists(thumbDir) ? thumbDir : null;

        }

        /// <summary>
        /// Generate thumbnails path for image file
        /// </summary>
        /// <param name="fileInfo">File info</param>
        /// <returns>Result path</returns>
        public string GenerateThumbPath(FileInfo fileInfo)
        {

            // Validate thumbnails config
            if (string.IsNullOrEmpty(GetThumbnailStoragePath())
                || !CanCreateThumbnail(fileInfo))
            {
                return null;
            }

            // Get paths
            var relativePath =
                fileInfo.FullName.Substring(Directory.FullName.Length);
            var thumbDir =
                Path.GetDirectoryName(GetThumbnailStoragePath() + relativePath);
            var thumbName =
                Path.GetFileNameWithoutExtension(fileInfo.Name) + "_" +
                FileSystemUtils.GetFileMd5(fileInfo) + fileInfo.Extension;

            return Path.Combine(thumbDir ?? "", thumbName);

        }

        /// <summary>
        /// Get if thumnail can be created for file
        /// </summary>
        /// <param name="fileInfo">File info</param>
        /// <returns>True/False, based on result</returns>
        public bool CanCreateThumbnail(FileInfo fileInfo)
        {

            return !string.IsNullOrEmpty(ThumbnailsUrl)
                && ImagingUtils.CanProcessFile(fileInfo.Extension);

        }

        /// <summary>
        /// Generate thumbnail hash
        /// </summary>
        /// <param name="fileInfo">File info</param>
        /// <returns>Result</returns>
        public string GenerateThumbHash(FileInfo fileInfo)
        {

            if (ThumbnailsDirectory == null)
            {
                var thumbName = Path.GetFileNameWithoutExtension(fileInfo.Name) + "_" + FileSystemUtils.GetFileMd5(fileInfo) + fileInfo.Extension;
                var relativePath = fileInfo.DirectoryName?.Substring(Directory.FullName.Length);
                return VolumeId + UrlPathUtils.EncodePath(relativePath + "\\" + thumbName);
            }
            else
            {
                var thumbPath = GenerateThumbPath(fileInfo);
                var relativePath = thumbPath.Substring(ThumbnailsDirectory.FullName.Length);
                return VolumeId + UrlPathUtils.EncodePath(relativePath);
            }

        }

        /// <summary>
        /// Get target full path for given root volume
        /// </summary>
        /// <param name="target">Target</param>
        /// <returns>Result target full path</returns>
        public string GetTargetFullPath(string target)
        {

            // Define path hash
            string pathHash = null;

            // Check if separator char is set in target path
            if (target.Contains(ConnectorFileSystemDriverConstants.VolumeSeparator))
            {

                // Get path hash
                pathHash =
                    target.Substring(
                        target.IndexOf(
                            ConnectorFileSystemDriverConstants.VolumeSeparator,
                            StringComparison.InvariantCulture) + 1);

            }

            // Decode path hash
            var decodedPath = UrlPathUtils.DecodePathHash(pathHash);

            // Normalize it to remove leading \
            decodedPath = decodedPath.TrimStart('\\');

            // Get directory url
            var directoryUrl = decodedPath != Directory.Name ? decodedPath : string.Empty;

            // Get target full path
            var targetFullPath = Path.Combine(Directory.FullName, directoryUrl);

            // Return it
            return targetFullPath;

        }

        /// <summary>
        /// Generate thumbnail for image
        /// </summary>
        /// <param name="originalImage">Image</param>
        /// <returns>Thumbnail generated result</returns>
        public ThumbnailGeneratedResult GenerateThumbnail(FileSystemVolumeFileInfo originalImage)
        {

            // Normalize file name
            var name =
                originalImage.File.Name.Substring(0,
                    originalImage.File.Name.LastIndexOf(
                        ConnectorFileSystemDriverConstants.VolumeSeparator,
                        StringComparison.InvariantCulture));

            // Compute full path
            var fullPath = $@"{originalImage.File.DirectoryName}\{name}{originalImage.File.Extension}";

            // Check that thumbnails directory is set
            if (ThumbnailsDirectory != null)
            {

                // Is set

                // Get thumbnail path
                var thumbPath = originalImage.File.FullName.StartsWith(ThumbnailsDirectory.FullName)
                    ? originalImage.File
                    : new FileInfo(Path.Combine(ThumbnailsDirectory.FullName, originalImage.RelativePath));

                // Check if it exists
                if (!thumbPath.Exists)
                {

                    // Doesn't exist

                    // Check if directory exists
                    if (thumbPath.Directory != null && !thumbPath.Directory.Exists)
                    {

                        // Doesn't exist, create it
                        System.IO.Directory.CreateDirectory(thumbPath.Directory.FullName);

                    }

                    // Generate thumbnail
                    byte[] thumbBytes;

                    using (var thumbFile = thumbPath.Create())
                    {

                        // Get thumnail bytes
                        thumbBytes = ImagingUtils.GenerateThumbnail(File.ReadAllBytes(fullPath), ThumbnailsSize, true);

                        // Write thumbnail bytes to file
                        thumbFile.Write(thumbBytes, 0, thumbBytes.Length);

                    }

                    // Return result
                    return new ThumbnailGeneratedResult(new FileInfo(thumbPath.FullName), thumbBytes);

                }

                // Exists
                return new ThumbnailGeneratedResult(thumbPath, File.ReadAllBytes(thumbPath.FullName));

            }
            
            // Is not set

            // Return thumbnail bytes
            return new ThumbnailGeneratedResult(
                new FileInfo(fullPath), 
                ImagingUtils.GenerateThumbnail(File.ReadAllBytes(fullPath), ThumbnailsSize, true)
                );

        }

        #endregion

        #region Protected

        /// <summary>
        /// Initialize thumbnails storage
        /// </summary>
        /// <param name="thumbnailsRootDirectory">Thumbnails root directory</param>
        protected void InitThumbnailsStorage(DirectoryInfo thumbnailsRootDirectory)
        {

            // Init volume target thumbnails directory
            var targetDirectory = new DirectoryInfo(Path.Combine(thumbnailsRootDirectory.FullName, ".tmb_" + VolumeId + Alias));

            // Check if it exists
            if (!targetDirectory.Exists)
            {

                // Doesn't exist
                ThumbnailsDirectory = System.IO.Directory.CreateDirectory(targetDirectory.FullName);
                ThumbnailsDirectory.Attributes |= FileAttributes.Hidden;

            }
            else
            {
                
                // Exists
                ThumbnailsDirectory = targetDirectory;

            }

        }

        /// <summary>
        /// Get thumbnails storage path
        /// </summary>
        /// <returns>Result</returns>
        protected string GetThumbnailStoragePath()
        {

            return ThumbnailsDirectory?.FullName ?? "";

        }

        #endregion

        #endregion

        #region Static methods

        /// <summary>
        /// Create file system root volume instance
        /// </summary>
        /// <param name="directory">Directory</param>
        /// <param name="thumbnailsRootDirectory">Thumbnails root directory</param>
        /// <param name="url">Url</param>
        /// <returns>Result instance</returns>
        public static FileSystemRootVolume Create(DirectoryInfo directory, DirectoryInfo thumbnailsRootDirectory, string url)
        {

            // Check that directory exists
            if(!directory.Exists) throw new DirectoryNotFoundException();

            // Create instance
            return new FileSystemRootVolume(directory, thumbnailsRootDirectory, url);

        }

        /// <summary>
        /// Create file system root volume instance
        /// </summary>
        /// <param name="directory">Directory</param>
        /// <param name="thumbnailsRootDirectory">Thumbnails root directory</param>
        /// <param name="url">Url</param>
        /// <returns>Result instance</returns>
        public static FileSystemRootVolume Create(string directory, string thumbnailsRootDirectory, string url = null)
        {

            // Check that directory is defined
            if(string.IsNullOrEmpty(directory)) throw new ArgumentNullException(nameof(directory));

            // Call overload
            return Create(
                new DirectoryInfo(directory), 
                new DirectoryInfo(thumbnailsRootDirectory),  
                url);

        }

        #endregion

    }

}