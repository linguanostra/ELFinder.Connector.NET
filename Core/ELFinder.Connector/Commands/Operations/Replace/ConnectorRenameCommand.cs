using ELFinder.Connector.Commands.Operations.Common;
using ELFinder.Connector.Commands.Results.Replace;

namespace ELFinder.Connector.Commands.Operations.Replace
{
    /// <summary>
    /// ELFinder connector command: Rename
    /// </summary>
    public class ConnectorRenameCommand : ConnectorSingleTargetNameCommand<RenameCommandResult>
    {

        #region Overrides

        /// <summary>
        /// Execute command
        /// </summary>
        /// <returns>Result</returns>
        public override RenameCommandResult Execute()
        {

            return Driver.Rename(Target, Name);

        }

        #endregion

    }
}