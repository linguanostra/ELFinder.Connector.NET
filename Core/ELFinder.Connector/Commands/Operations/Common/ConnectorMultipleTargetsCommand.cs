using System.Collections.Generic;
using ELFinder.Connector.Commands.Operations.Common.Interfaces;
using ELFinder.Connector.Commands.Results.Common;

namespace ELFinder.Connector.Commands.Operations.Common
{

    /// <summary>
    /// ELFinder multiple targets connector command
    /// </summary>
    public abstract class ConnectorMultipleTargetsCommand<TResult> : ConnectorCommand<TResult>, IConnectorMultipleTargetsCommand
        where TResult : CommandResult
    {

        #region Properties

        /// <summary>
        /// Hash of targets
        /// </summary>
        public List<string> Targets { get; set; }

        #endregion

    }
}