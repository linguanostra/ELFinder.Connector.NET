using ELFinder.Connector.Commands.Operations.Common;
using ELFinder.Connector.Commands.Results.Search;

namespace ELFinder.Connector.Commands.Operations.Search
{

    /// <summary>
    /// Connector command: Search
    /// </summary>
    public class ConnectorSearchCommand : ConnectorCommand<SearchCommandResult>
    {

        #region Properties

        /// <summary>
        /// Query
        /// </summary>
        public string Q { get; set; }

        #endregion

        #region Overrides

        /// <summary>
        /// Execute command
        /// </summary>
        /// <returns>Result</returns>
        public override SearchCommandResult Execute()
        {

            return Driver.Search(Q);

        }

        #endregion

    }
}