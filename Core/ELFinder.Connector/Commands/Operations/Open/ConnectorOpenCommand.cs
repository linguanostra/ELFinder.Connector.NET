using ELFinder.Connector.Commands.Operations.Open.Common;
using ELFinder.Connector.Commands.Results.Open;

namespace ELFinder.Connector.Commands.Operations.Open
{
    /// <summary>
    /// ELFinder connector command: Open
    /// </summary>
    public class ConnectorOpenCommand : ConnectorBaseOpenCommand<OpenCommandResult>
    {

        #region Overrides

        /// <summary>
        /// Execute command
        /// </summary>
        /// <returns>Result</returns>
        public override OpenCommandResult Execute()
        {

            return Driver.Open(Target, IncludeRootSubFolders);

        }

        #endregion
        
    }
}