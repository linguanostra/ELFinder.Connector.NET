using System;
using System.IO;
using ELFinder.Connector.Commands.Results.Content.Common;

namespace ELFinder.Connector.Commands.Results.Image.Thumbnails
{
    /// <summary>
    /// Get thumbnail result
    /// </summary>
    public class GetThumbnailResult : BaseFileContentResult
    {

        #region Properties

        /// <summary>
        /// Thumbnail bytes
        /// </summary>
        public byte[] ThumbnailBytes { get; set; } 

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance
        /// </summary>
        public GetThumbnailResult()
        {
        }

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="file">File</param>
        /// <param name="contentType">Content type</param>
        public GetThumbnailResult(FileInfo file, string contentType) : base(file, contentType)
        {
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Get content stream 
        /// </summary>
        /// <returns>Result content stream</returns>
        public override Stream GetContentStream()
        {

            // Ensure thumbnail bytes are set
            if(ThumbnailBytes == null) throw new ArgumentNullException(nameof(ThumbnailBytes));
            
            // Return content from thumbnail bytes
            return new MemoryStream(ThumbnailBytes);

        }

        #endregion

    }
}