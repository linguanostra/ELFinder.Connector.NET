using ELFinder.Connector.Commands.Operations.Open.Common;
using ELFinder.Connector.Commands.Results.Open;

namespace ELFinder.Connector.Commands.Operations.Open
{
    /// <summary>
    /// ELFinder connector command: Init
    /// </summary>
    public class ConnectorInitCommand : ConnectorBaseOpenCommand<InitCommandResult>
    {

        #region Overrides

        /// <summary>
        /// Execute command
        /// </summary>
        /// <returns>Result</returns>
        public override InitCommandResult Execute()
        {

            return Driver.Init(Target);

        }

        #endregion

    }
}