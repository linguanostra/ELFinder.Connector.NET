using System.Collections.Generic;
using ELFinder.Connector.Commands.Operations.Common.Interfaces;
using ELFinder.Connector.Commands.Results.Common;
using ELFinder.Connector.Drivers.Common.Interfaces;
using ELFinder.Connector.Streams;

namespace ELFinder.Connector.Commands.Operations.Common
{

    /// <summary>
    /// Connector base command
    /// </summary>
    /// <typeparam name="TResult">Result type</typeparam>
    public abstract class ConnectorCommand<TResult> : IConnectorCommand
        where TResult : CommandResult
    {

        #region Properties

        /// <summary>
        /// Connector driver
        /// </summary>
        public IConnectorDriver Driver { get; set; }

        /// <summary>
        /// Command
        /// </summary>
        public string Cmd { get; set; }

        /// <summary>
        /// Files sent with the command
        /// </summary>
        public IEnumerable<IFileStream> Files { get; set; }

        #endregion

        #region IConnectorBaseCommand members

        /// <summary>
        /// Execute command
        /// </summary>
        /// <returns>Result</returns>
        dynamic IConnectorCommand.Execute()
        {
            return Execute();
        }

        #endregion

        #region Abstract methods

        /// <summary>
        /// Execute command
        /// </summary>
        /// <returns>Result</returns>
        public abstract TResult Execute();

        #endregion

    }

}