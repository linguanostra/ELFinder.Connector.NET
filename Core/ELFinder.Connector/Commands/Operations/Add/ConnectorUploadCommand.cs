using System.Linq;
using ELFinder.Connector.Commands.Operations.Common;
using ELFinder.Connector.Commands.Results.Add;

namespace ELFinder.Connector.Commands.Operations.Add
{
    /// <summary>
    /// Connector command: Upload
    /// </summary>
    public class ConnectorUploadCommand : ConnectorSingleTargetCommand<UploadCommandResult>
    {

        #region Overrides

        /// <summary>
        /// Execute command
        /// </summary>
        /// <returns>Result</returns>
        public override UploadCommandResult Execute()
        {

            return Driver.Upload(Target, Files.ToArray());

        }

        #endregion
         
    }
}