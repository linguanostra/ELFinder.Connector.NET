using System.Collections.Generic;

namespace ELFinder.Connector.Commands.Operations.Common.Interfaces
{

    /// <summary>
    /// Connector multiple targets command interface
    /// </summary>
    public interface IConnectorMultipleTargetsCommand : IConnectorCommand
    {

        #region Properties

        /// <summary>
        /// Hash of targets
        /// </summary>
        List<string> Targets { get; set; }

        #endregion

    }
}