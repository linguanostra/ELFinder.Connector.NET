using ELFinder.Connector.Commands.Operations.Common.Interfaces;
using ELFinder.Connector.Commands.Results.Common;

namespace ELFinder.Connector.Commands.Operations.Common
{
    /// <summary>
    /// ELFinder single target connector command
    /// </summary>
    public abstract class ConnectorSingleTargetCommand<TResult> : ConnectorCommand<TResult>, IConnectorSingleTargetCommand
        where TResult : CommandResult
    {

        #region Properties

        /// <summary>
        /// Hash of target
        /// </summary>
        public string Target { get; set; }

        #endregion

    }
}