using ELFinder.Connector.Commands.Results.Common.Errors;
using ELFinder.Connector.Extensions;
using Newtonsoft.Json;

namespace ELFinder.Connector.Commands.Results.Common
{

    /// <summary>
    /// Command result
    /// </summary>
    public abstract class CommandResult
    {

        #region Constants

        /// <summary>
        /// Empty array
        /// </summary>
        protected static readonly string[] Empty = new string[0];

        #endregion

        #region Properties

        /// <summary>
        /// Errors list
        /// </summary>
        [JsonProperty(PropertyName = "error")]
        public dynamic Error { get; private set; } 

        /// <summary>
        /// Error data
        /// </summary>
        [JsonProperty(PropertyName = "errorData")]
        public dynamic ErrorData { get; private set; }

        #endregion

        #region Methods

        #region Internal

        /// <summary>
        /// Set error: Access denied
        /// </summary>
        internal void SetAccessDeniedError()
        {
            
            SetError(CommandErrorsEnum.ErrAccess);

        }

        /// <summary>
        /// Set error: Object not a file
        /// </summary>
        internal void SetObjectNotFileError()
        {
            SetError(CommandErrorsEnum.ErrNotFile);
        }

        /// <summary>
        /// Set error: File not found
        /// </summary>
        internal void SetFileNotFoundError()
        {
            SetError(CommandErrorsEnum.ErrFileNotFound);
        }

        #endregion

        #region Protected

        /// <summary>
        /// Set error
        /// </summary>
        /// <param name="error">Error</param>
        protected internal void SetError(CommandErrorsEnum error)
        {

            // Set error code with proper case
            Error = error.ToString().ToCamelCase();

        }

        /// <summary>
        /// Set error
        /// </summary>
        /// <param name="error">Error</param>
        /// <param name="errorData">Error data</param>
        protected internal void SetError(CommandErrorsEnum error, dynamic errorData)
        {

            // Set error
            SetError(error);

            // Set error data
            ErrorData = errorData;

        }

        #endregion

        #endregion

    }

}