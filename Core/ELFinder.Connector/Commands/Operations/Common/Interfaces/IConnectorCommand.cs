using System.Collections.Generic;
using ELFinder.Connector.Drivers.Common.Interfaces;
using ELFinder.Connector.Streams;

namespace ELFinder.Connector.Commands.Operations.Common.Interfaces
{
    /// <summary>
    /// Connector command interface
    /// </summary>
    public interface IConnectorCommand
    {

        #region Properties

        /// <summary>
        /// Connector driver
        /// </summary>
        IConnectorDriver Driver { get; set; }

        /// <summary>
        /// Command
        /// </summary>
        string Cmd { get; set; }

        /// <summary>
        /// Files sent with the command
        /// </summary>
        IEnumerable<IFileStream> Files { get; set; }

        #endregion

        #region Abstract methods

        /// <summary>
        /// Execute command
        /// </summary>
        /// <returns>Result</returns>
        dynamic Execute();

        #endregion

    }
}