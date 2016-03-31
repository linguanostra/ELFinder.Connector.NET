using ELFinder.Connector.Commands.Results.Common;

namespace ELFinder.Connector.Commands.Operations.Common
{
    /// <summary>
    /// ELFinder single target connector with name command
    /// </summary>
    public abstract class ConnectorSingleTargetNameCommand<TResult> : ConnectorSingleTargetCommand<TResult>
        where TResult : CommandResult
    {

        #region Properties

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        #endregion

    }
}