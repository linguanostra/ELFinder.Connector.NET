using ELFinder.Connector.Commands.Operations.Common;
using ELFinder.Connector.Commands.Results.Tree;

namespace ELFinder.Connector.Commands.Operations.Tree
{
    /// <summary>
    /// ELFinder connector command: Parents
    /// </summary>
    public class ConnectorParentsCommand : ConnectorSingleTargetCommand<ParentsCommandResult>
    {

        #region Overrides

        /// <summary>
        /// Execute command
        /// </summary>
        /// <returns>Result</returns>
        public override ParentsCommandResult Execute()
        {

            return Driver.Parents(Target);

        }

        #endregion
        
    }
}