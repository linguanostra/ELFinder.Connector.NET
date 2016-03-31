using ELFinder.Connector.Commands.Operations.Common;
using ELFinder.Connector.Commands.Results.Add;

namespace ELFinder.Connector.Commands.Operations.Add
{

    /// <summary>
    /// ELFinder connector command: Make file
    /// </summary>
    public class ConnectorMakeFileCommand : ConnectorSingleTargetNameCommand<MakeFileCommandResult>
    {

        #region Overrides

        /// <summary>
        /// Execute command
        /// </summary>
        /// <returns>Result</returns>
        public override MakeFileCommandResult Execute()
        {

            return Driver.MakeFile(Target, Name);

        }

        #endregion

    }
}