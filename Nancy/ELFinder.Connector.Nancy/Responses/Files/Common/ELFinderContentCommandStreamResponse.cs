using ELFinder.Connector.Commands.Results.Content.Common;
using Nancy;
using Nancy.Responses;

namespace ELFinder.Connector.Nancy.Responses.Files.Common
{

    /// <summary>
    /// ELFinder content command stream response
    /// </summary>
    /// <typeparam name="TResult">Result type</typeparam>
    public abstract class ELFinderContentCommandStreamResponse<TResult> : StreamResponse
        where TResult : BaseFileContentResult
    {

        #region Properties

        /// <summary>
        /// Command result
        /// </summary>
        public TResult CommandResult { get; }

        /// <summary>
        /// Context
        /// </summary>
        public NancyContext Context { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="commandResult">Command result</param>
        /// <param name="context">Context</param>
        protected ELFinderContentCommandStreamResponse(TResult commandResult, NancyContext context) :
            base(commandResult.GetContentStream, null)
        {

            // Assign values
            CommandResult = commandResult;
            Context = context;

            // Init response
            InitResponse();

        }

        #endregion

        #region Methods

        /// <summary>
        /// Init response
        /// </summary>
        protected void InitResponse()
        {

            // Set content headers
            SetContentHeaders();

        }

        #endregion

        #region Virtual methods

        /// <summary>
        /// Set content headers
        /// </summary>
        protected virtual void SetContentHeaders()
        {

            // Set content type
            ContentType = CommandResult.ContentType;

        }

        #endregion

    }

}