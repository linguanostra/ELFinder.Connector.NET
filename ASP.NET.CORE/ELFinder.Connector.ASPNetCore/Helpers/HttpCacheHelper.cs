using System;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Mvc;
using ELFinder.Connector.Drivers.FileSystem.Utils;

namespace ELFinder.Connector.ASPNet.Helpers
{

    /// <summary>
    /// Http cache helper
    /// </summary>
    public class HttpCacheHelper
    {

        #region Static methods

        /// <summary>
        /// Returns whether to return a not modified response, based on the etag and last modified date
        /// of the resource, and the current controller context
        /// </summary>
        /// <param name="etag">Current resource etag, or null</param>
        /// <param name="lastModified">Current resource last modified, or null</param>
        /// <param name="context">Current controller context</param>
        /// <returns>True if not modified should be sent, false otherwise</returns>
        public static bool ReturnNotModified(string etag, DateTime? lastModified, ControllerContext context)
        {

            // Check context
            if (context == null || context.RequestContext == null)
            {
                return false;
            }

            // Get ETag header
            var requestEtag = context.HttpContext.Request.Headers["If-None-Match"];

            // Get Last-Modified-Since header
            var requestDate = ParseDateTime(context.HttpContext.Request.Headers["If-Modified-Since"]);

            // Check if file ETag has changed
            if (requestEtag != null && !string.IsNullOrEmpty(etag) && requestEtag.Equals(etag, StringComparison.Ordinal))
            {
                return true;
            }

            // Check if request date has changed
            if (requestDate.HasValue && lastModified.HasValue && ((int)(lastModified.Value - requestDate.Value).TotalSeconds) <= 0)
            {
                return true;
            }

            return false;

        }

        /// <summary>
        /// Parse request date time
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns>Result, null if parsing cannot be done.</returns>
        private static DateTime? ParseDateTime(string value)
        {

            DateTime result;

            // Note CultureInfo.InvariantCulture is ignored
            if (DateTime.TryParseExact(value, "R", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                return result;
            }

            return null;

        }

        /// <summary>
        /// Get if given file 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public static bool IsFileFromCache(FileInfo info, HttpRequestBase request, HttpResponseBase response)
        {
            DateTime updated = info.LastWriteTimeUtc;
            string filename = info.Name;
            DateTime modifyDate;
            if (!DateTime.TryParse(request.Headers["If-Modified-Since"], out modifyDate))
            {
                modifyDate = DateTime.UtcNow;
            }
            string eTag = GetFileETag(filename, updated);
            if (!IsFileModified(updated, eTag, request))
            {
                response.StatusCode = (int)System.Net.HttpStatusCode.NotModified;
                response.StatusDescription = "Not Modified";
                response.AddHeader("Content-Length", "0");
                response.Cache.SetCacheability(HttpCacheability.Public);
                response.Cache.SetLastModified(updated);
                response.Cache.SetETag(eTag);
                return true;
            }
            else
            {
                response.Cache.SetAllowResponseInBrowserHistory(true);
                response.Cache.SetCacheability(HttpCacheability.Public);
                response.Cache.SetLastModified(updated);
                response.Cache.SetETag(eTag);
                return false;
            }
        }

        private static string GetFileETag(string fileName, DateTime modified)
        {
            return "\"" + FileSystemUtils.GetFileMd5(fileName, modified) + "\"";
        }

        private static bool IsFileModified(DateTime modifyDate, string eTag, HttpRequestBase request)
        {
            DateTime modifiedSince;
            bool fileDateModified = true;

            //Check If-Modified-Since request header, if it exists 
            if (!string.IsNullOrEmpty(request.Headers["If-Modified-Since"]) && DateTime.TryParse(request.Headers["If-Modified-Since"], out modifiedSince))
            {
                fileDateModified = false;
                if (modifyDate > modifiedSince)
                {
                    TimeSpan modifyDiff = modifyDate - modifiedSince;
                    //ignore time difference of up to one seconds to compensate for date encoding
                    fileDateModified = modifyDiff > TimeSpan.FromSeconds(1);
                }
            }

            //check the If-None-Match header, if it exists, this header is used by FireFox to validate entities based on the etag response header 
            bool eTagChanged = false;
            if (!string.IsNullOrEmpty(request.Headers["If-None-Match"]))
            {
                eTagChanged = request.Headers["If-None-Match"] != eTag;
            }
            return (eTagChanged || fileDateModified);
        }

        #endregion

    }
}