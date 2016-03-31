using ELFinder.Connector.Commands.Results.Content;
using ELFinder.Connector.Nancy.Extensions;
using ELFinder.Connector.Nancy.Responses.Files.Common;
using ELFinder.Connector.Web.Extensions;
using Nancy;
using Nancy.Helpers;

namespace ELFinder.Connector.Nancy.Responses.Files
{

    /// <summary>
    /// ELFinder file download response
    /// </summary>
    public class ELFinderFileDownloadResponse : ELFinderContentCommandStreamResponse<FileCommandResult>
    {
        
        #region Constructors

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="commandResult">Command result</param>
        /// <param name="context">Context</param>
        public ELFinderFileDownloadResponse(FileCommandResult commandResult, NancyContext context) 
            : base(commandResult, context)
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

            // Encode file name
            var encodedFileName = HttpUtility.UrlEncode(CommandResult.File.Name);

            // Compute file name disposition value
            // IE < 9 does not support RFC 6266 (RFC 2231/RFC 5987)
            var fileNameDisposition = Context.Request.IsFromMSIE()
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
            Headers["Content-Disposition"] = contentDisposition;
            Headers["Content-Location"] = CommandResult.File.Name;
            Headers["Content-Transfer-Encoding"] = "binary";
            Headers["Content-Length"] = CommandResult.File.Length.ToString();

        }

        #endregion

    }

}