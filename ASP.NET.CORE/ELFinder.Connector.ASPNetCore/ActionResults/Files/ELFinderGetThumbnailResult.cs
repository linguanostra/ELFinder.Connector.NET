using ELFinder.Connector.ASPNetCore.ActionResults.Files.Common;
using ELFinder.Connector.Commands.Results.Image.Thumbnails;
using ELFinder.Connector.Drivers.FileSystem.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ELFinder.Connector.ASPNetCore.ActionResults.Files
{

    /// <summary>
    /// ELFinder get thumbnail result
    /// </summary>
    public class ELFinderGetThumbnailResult : ELFinderContentCommandStreamResult<GetThumbnailResult>
    {

        #region Constructors

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="commandResult">Command result</param>
        public ELFinderGetThumbnailResult(GetThumbnailResult commandResult) : 
            base(commandResult)
        {
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Set content headers
        /// </summary>
        /// <param name="context">Controller context</param>
        /// <param name="response">Response</param>
        protected override void SetContentHeaders(ActionContext context, HttpResponse response)
        {

            // Call base method
            base.SetContentHeaders(context, response);

            // Get info
            var lastModified = CommandResult.File.LastWriteTimeUtc;
            var fileName = CommandResult.File.Name;
            var eTag = GetFileETag(fileName, lastModified);

            // Get if file is considered modified
            //if (HttpCacheHelper.ReturnNotModified(eTag, lastModified, context))
            //{

            //    // Not modified
            //    response.StatusCode = (int)HttpStatusCode.NotModified;
            //    response.StatusDescription = "Not Modified";
            //    response.Headers.Add("Content-Length", "0");
            //    response.Cache.SetCacheability(HttpCacheability.Public);
            //    response.Cache.SetLastModified(lastModified);
            //    response.Cache.SetETag(eTag);

            //}
            //else
            //{
                
            //    // Modified
            //    response.Headers["ETag"] = eTag;
            //    response.Headers["Last-Modified"] = lastModified.ToString("R");                

            //}

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