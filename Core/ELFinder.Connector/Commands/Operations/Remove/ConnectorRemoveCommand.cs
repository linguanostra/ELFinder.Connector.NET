using ELFinder.Connector.Commands.Operations.Common;
using ELFinder.Connector.Commands.Results.Remove;

namespace ELFinder.Connector.Commands.Operations.Remove
{
    /// <summary>
    /// Connector command: Remove
    /// </summary>
    public class ConnectorRemoveCommand : ConnectorMultipleTargetsCommand<RemoveCommandResult>
    {

        #region Overrides

        /// <summary>
        /// Execute command
        /// </summary>
        /// <returns>Result</returns>
        public override RemoveCommandResult Execute()
        {

            return Driver.Remove(Targets);

        }

        #endregion
    }
}