using Microsoft.AspNetCore.Http;
using System.Linq;

namespace ELFinder.Connector.ASPNetCore.Extensions
{

    /// <summary>
    /// Http request extensions
    /// </summary>
    public static class HttpRequestExtensions
    {

        #region Extension methods

        /// <summary>
        /// Gets whether request was made by MSIE browser or not
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>True/False, based on result</returns>
        public static bool IsFromMSIE(this HttpRequest request)
        {

            return request.Headers["User-Agent"].Contains("MSIE");

        } 

        #endregion
         
    }
}