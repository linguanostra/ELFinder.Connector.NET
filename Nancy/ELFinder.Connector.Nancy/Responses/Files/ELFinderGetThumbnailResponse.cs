using System;
using ELFinder.Connector.Commands.Results.Image.Thumbnails;
using ELFinder.Connector.Drivers.FileSystem.Utils;
using ELFinder.Connector.Nancy.Responses.Files.Common;
using Nancy;
using Nancy.Helpers;

namespace ELFinder.Connector.Nancy.Responses.Files
{

    /// <summary>
    /// ELFinder get thumbnail response
    /// </summary>
    public class ELFinderGetThumbnailResponse : ELFinderContentCommandStreamResponse<GetThumbnailResult>
    {

        #region Constructors

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="commandResult">Command result</param>
        /// <param name="context">Context</param>
        public ELFinderGetThumbnailResponse(GetThumbnailResult commandResult, NancyContext context) : 
            base(commandResult, context)
        {
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Set content headers
        /// </summary>
        protected override void SetContentHeaders()
        {

            // Call base method
            base.SetContentHeaders();

            // Get info
            var lastModified = CommandResult.File.LastWriteTimeUtc;
            var fileName = CommandResult.File.Name;
            var eTag = GetFileETag(fileName, lastModified);

            // Get if file is considered modified
            if (CacheHelpers.ReturnNotModified(eTag, lastModified, Context))
            {

                // Not modified
                StatusCode = HttpStatusCode.NotModified;
                ContentType = null;
                Contents = NoBody;

            }
            else
            {
                
                // Modified
                Headers["ETag"] = eTag;
                Headers["Last-Modified"] = lastModified.ToString("R");                

            }

        }

        #endregion

        #region Static methods

        /// <summary>
        /// Get file ETag
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <param name="modified">Modified date</param>
        /// <returns>Result ETag</returns>
        private static string GetFileETag(string fileName, DateTime modified)
        {
            return $"\"{FileSystemUtils.GetFileMd5(fileName, modified)}\"";
        }

        #endregion

    }

}