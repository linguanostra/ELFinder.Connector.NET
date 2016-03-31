using System.IO;

namespace ELFinder.Connector.Drivers.Common.Data.Results
{

    /// <summary>
    /// Thumbnail generated result
    /// </summary>
    public class ThumbnailGeneratedResult
    {

        #region Properties

        /// <summary>
        /// Thumbnail file
        /// </summary>
        public FileInfo ThumbnailFile { get; } 

        /// <summary>
        /// Thumbnail bytes
        /// </summary>
        public byte[] ThumbnailBytes { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="thumbnailFile">Thumbnail file</param>
        /// <param name="thumbnailBytes">Thumbnail bytes</param>
        public ThumbnailGeneratedResult(FileInfo thumbnailFile, byte[] thumbnailBytes)
        {
            ThumbnailFile = thumbnailFile;
            ThumbnailBytes = thumbnailBytes;
        }

        #endregion
         
    }
}