using ELFinder.Connector.Commands.Results.Content.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ELFinder.Connector.ASPNetCore.ActionResults.Files.Common
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
        protected virtual void SetContentHeaders(ActionContext context, HttpResponse response)
        {

            // Set content type
            //context.HttpContext.Response.ContentType= ContentType;
            response.ContentType = ContentType;

        }

        #endregion

        #region Overrides

        /// <summary>
        /// Execute result
        /// </summary>
        /// <param name="context">Controller context</param>
        public override void ExecuteResult(ActionContext context)
        {

            // Validate context
            if (context == null) throw new ArgumentNullException(nameof(context));

            // Get response
            var response = context.HttpContext.Response;

            // Set content headers
            SetContentHeaders(context, response);
            
            // Write file
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var executor = context.HttpContext.RequestServices.GetRequiredService<FileStreamResultExecutor>();
            executor.ExecuteAsync(context, this);


        }

        #endregion

    }

}