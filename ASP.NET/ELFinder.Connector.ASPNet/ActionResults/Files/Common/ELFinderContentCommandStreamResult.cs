using System;
using System.Web;
using System.Web.Mvc;
using ELFinder.Connector.Commands.Results.Content.Common;

namespace ELFinder.Connector.ASPNet.ActionResults.Files.Common
{

    /// <summary>
    /// ELFinder content command stream result
    /// </summary>
    public abstract class ELFinderContentCommandStreamResult<TResult> : FileStreamResult
        where TResult : BaseFileContentResult
    {

        #region Properties

        /// <summary>
        /// Command result
        /// </summary>
        public TResult CommandResult { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="commandResult">Command result</param>
        protected ELFinderContentCommandStreamResult(TResult commandResult) :
            base(commandResult.GetContentStream(), commandResult.ContentType)
        {

            // Assign values
            CommandResult = commandResult;
            
        }

        #endregion

        #region Virtual methods

        /// <summary>
        /// Set content headers
        /// </summary>
        /// <param name="context">Controller context</param>
        /// <param name="response">Response</param>
        protected virtual void SetContentHeaders(ControllerContext context, HttpResponseBase response)
        {

            // Set content type
            response.ContentType = ContentType;

        }

        #endregion

        #region Overrides

        /// <summary>
        /// Execute result
        /// </summary>
        /// <param name="context">Controller context</param>
        public override void ExecuteResult(ControllerContext context)
        {

            // Validate context
            if (context == null) throw new ArgumentNullException(nameof(context));

            // Get response
            var response = context.HttpContext.Response;

            // Set content headers
            SetContentHeaders(context, response);
            
            // Write file
            //WriteFile(response);
            int chunkSize = 8192;
            byte[] buffer = new byte[chunkSize];
            int offset = 0;
            int read = 0;
            var fs = base.FileStream;
            
            while ((read = fs.Read(buffer, offset, chunkSize)) > 0)
            {
                if (!response.IsClientConnected)                    break;
                response.OutputStream.Write(buffer, 0, read);
                response.Flush();
            }
            

        }

        #endregion

    }

}
