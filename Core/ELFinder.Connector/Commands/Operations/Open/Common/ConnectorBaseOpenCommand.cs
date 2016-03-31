using ELFinder.Connector.Commands.Operations.Common;
using ELFinder.Connector.Commands.Results.Open.Common;

namespace ELFinder.Connector.Commands.Operations.Open.Common
{
    /// <summary>
    /// ELFinder connector base open command
    /// </summary>
    /// <typeparam name="TResult">Result type</typeparam>
    public abstract class ConnectorBaseOpenCommand<TResult> : ConnectorSingleTargetCommand<TResult>
        where TResult : OpenBaseCommandResult
    {

        #region Properties

        /// <summary>
        /// Include subfolders of root directories
        /// </summary>
        public bool IncludeRootSubFolders { get; set; }

        #endregion
                
    }
}