using Nancy;

namespace ELFinder.Connector.Nancy.Extensions
{

    /// <summary>
    /// Nancy request extensions
    /// </summary>
    public static class NancyRequestExtensions
    {

        #region Extension methods

        /// <summary>
        /// Gets whether request was made by MSIE browser or not
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>True/False, based on result</returns>
        public static bool IsFromMSIE(this Request request)
        {

            return request.Headers.UserAgent.Contains("MSIE");

        } 

        #endregion
         
    }
}