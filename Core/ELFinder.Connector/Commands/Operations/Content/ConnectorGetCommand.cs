using ELFinder.Connector.Commands.Operations.Common;
using ELFinder.Connector.Commands.Results.Content;

namespace ELFinder.Connector.Commands.Operations.Content
{
    /// <summary>
    /// ELFinder connector command: Get
    /// </summary>
    public class ConnectorGetCommand : ConnectorSingleTargetCommand<GetCommandResult>
    {

        #region Overrides

        /// <summary>
        /// Execute command
        /// </summary>
        /// <returns>Result</returns>
        public override GetCommandResult Execute()
        {

            return Driver.Get(Target);

        }

        #endregion

    }
}