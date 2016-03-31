using ELFinder.Connector.Commands.Operations.Common;
using ELFinder.Connector.Commands.Results.Tree;

namespace ELFinder.Connector.Commands.Operations.Tree
{
    /// <summary>
    /// ELFinder connector command: Tree
    /// </summary>
    public class ConnectorTreeCommand : ConnectorSingleTargetCommand<TreeCommandResult>
    {

        #region Overrides

        /// <summary>
        /// Execute command
        /// </summary>
        /// <returns>Result</returns>
        public override TreeCommandResult Execute()
        {

            return Driver.Tree(Target);

        }

        #endregion
    }
}