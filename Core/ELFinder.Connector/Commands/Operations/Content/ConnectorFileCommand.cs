using ELFinder.Connector.Commands.Operations.Common;
using ELFinder.Connector.Commands.Results.Content;

namespace ELFinder.Connector.Commands.Operations.Content
{
    /// <summary>
    /// ELFinder connector command: File
    /// </summary>
    public class ConnectorFileCommand : ConnectorSingleTargetCommand<FileCommandResult>
    {

        #region Properties

        /// <summary>
        /// Download file
        /// </summary>
        public bool Download { get; set; }

        #endregion

        #region Overrides

        /// <summary>
        /// Execute command
        /// </summary>
        /// <returns>Result</returns>
        public override FileCommandResult Execute()
        {

            return Driver.File(Target, Download);

        }

        #endregion

    }

}