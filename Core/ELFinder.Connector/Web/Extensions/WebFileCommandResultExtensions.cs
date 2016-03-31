using ELFinder.Connector.Commands.Results.Content.Common;

namespace ELFinder.Connector.Web.Extensions
{

    /// <summary>
    /// Web file command result extensions
    /// </summary>
    public static class WebFileCommandResultExtensions
    {

        #region Static methods

        /// <summary>
        /// Get if content refers to an inline file
        /// </summary>
        /// <param name="commandResult">Command result</param>
        /// <returns>True/False, based on result</returns>
        public static bool IsInlineFile(this BaseFileContentResult commandResult)
        {

            return
                commandResult.ContentType.Contains("image")
                || commandResult.ContentType.Contains("text")
                || commandResult.ContentType == "application/x-shockwave-flash";

        }

        #endregion
         
    }
}