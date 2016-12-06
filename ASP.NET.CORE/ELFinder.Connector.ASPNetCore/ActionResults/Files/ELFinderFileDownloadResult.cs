using ELFinder.Connector.ASPNetCore.ActionResults.Files.Common;
using ELFinder.Connector.ASPNetCore.Extensions;
using ELFinder.Connector.Commands.Results.Content;
using ELFinder.Connector.Web.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ELFinder.Connector.ASPNetCore.ActionResults.Files
{

    /// <summary>
    /// ELFinder file download result
    /// </summary>
    public class ELFinderFileDownloadResult : ELFinderContentCommandStreamResult<FileCommandResult>
    {
        
        #region Constructors

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="commandResult">Command result</param>
        public ELFinderFileDownloadResult(FileCommandResult commandResult) 
            : base(commandResult)
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

            // Encode file name
            //var encodedFileName = HttpUtility.UrlEncode(CommandResult.File.Name);
            var encodedFileName = WebUtility.UrlEncode(CommandResult.File.Name);

            // Compute file name disposition value
            // IE < 9 does not support RFC 6266 (RFC 2231/RFC 5987)
            var fileNameDisposition = context.HttpContext.Request.IsFromMSIE()
                ? $"filename=\"{encodedFileName}\""
                : $"filename*=UTF-8\'\'{encodedFileName}";

            // Compute content disposition
            string contentDisposition;
            if (CommandResult.IsDownload)
            {

                // Download attachment disposition
                contentDisposition = "attachment; " + fileNameDisposition;
            }
            else
            {

                // Check if content is inline or should be set as attachment
                contentDisposition =
                    (CommandResult.IsInlineFile()
                        ? "inline; "
                        : "attachment; ") + fileNameDisposition;

            }

            // Set headers
            response.Headers["Content-Disposition"] = contentDisposition;
            response.Headers["Content-Location"] = CommandResult.File.Name;
            response.Headers["Content-Transfer-Encoding"] = "binary";
            response.Headers["Content-Length"] = CommandResult.File.Length.ToString();

        }

        #endregion

    }

}