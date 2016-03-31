using ELFinder.Connector.Commands.Operations.Common;
using ELFinder.Connector.Commands.Results.Add;

namespace ELFinder.Connector.Commands.Operations.Add
{
    /// <summary>
    /// ELFinder connector command: Make directory
    /// </summary>
    public class ConnectorMakeDirCommand : ConnectorSingleTargetNameCommand<MakeDirCommandResult>
    {

        #region Overrides

        /// <summary>
        /// Execute command
        /// </summary>
        /// <returns>Result</returns>
        public override MakeDirCommandResult Execute()
        {

            return Driver.MakeDir(Target, Name);

        }

        #endregion

    }
}