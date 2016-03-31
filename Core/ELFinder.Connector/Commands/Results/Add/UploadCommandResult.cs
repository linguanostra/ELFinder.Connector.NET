using ELFinder.Connector.Commands.Results.Add.Common;
using ELFinder.Connector.Commands.Results.Common.Errors;

namespace ELFinder.Connector.Commands.Results.Add
{

    /// <summary>
    /// Upload command result
    /// </summary>
    public class UploadCommandResult : AddCommandResult
    {

        #region Methods

        /// <summary>
        /// Set error: MaxUploadFileSize
        /// </summary>
        public void SetMaxUploadFileSizeError()
        {
            SetError(CommandErrorsEnum.ErrUploadFileSize);
        }

        #endregion  
    }

}