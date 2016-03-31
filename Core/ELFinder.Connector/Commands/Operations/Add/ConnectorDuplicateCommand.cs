using ELFinder.Connector.Commands.Operations.Common;
using ELFinder.Connector.Commands.Results.Add;

namespace ELFinder.Connector.Commands.Operations.Add
{
    /// <summary>
    /// Connector command: Duplicate
    /// </summary>
    public class ConnectorDuplicateCommand : ConnectorMultipleTargetsCommand<DuplicateCommandResult>
    {

        #region Overrides

        /// <summary>
        /// Execute command
        /// </summary>
        /// <returns>Result</returns>
        public override DuplicateCommandResult Execute()
        {

            return Driver.Duplicate(Targets);

        }

        #endregion

    }
}