using System.IO;
using System.Web;

namespace ELFinder.Connector.Utils
{

    /// <summary>
    /// Url path utilities
    /// </summary>
    public class UrlPathUtils
    {

        #region Static methods

        /// <summary>
        /// Encode path
        /// </summary>
        /// <param name="path">Path</param>
        /// <returns>Encode path hash</returns>
        public static string EncodePath(string path)
        {
            return HttpServerUtility.UrlTokenEncode(System.Text.Encoding.UTF8.GetBytes(path));
        }

        /// <summary>
        /// Decode path hash
        /// </summary>
        /// <param name="pathHash">Path hash</param>
        /// <returns>Decoded result</returns>
        public static string DecodePathHash(string pathHash)
        {

            // Validate path hash
            if (string.IsNullOrEmpty(pathHash)) return null;

            // Decode url token bytes
            var urlTokenBytes = HttpServerUtility.UrlTokenDecode(pathHash);
            
            // Return result
            return urlTokenBytes != null
                ? System.Text.Encoding.UTF8.GetString(urlTokenBytes)
                : null;

        }

        /// <summary>
        /// Get MIME type for given file
        /// </summary>
        /// <param name="file">File</param>
        /// <returns>Result MIME type</returns>
        public static string GetMimeType(FileInfo file)
        {
            return MimeTypes.GetMimeType(file.Name);
        }

        #endregion

    }
}