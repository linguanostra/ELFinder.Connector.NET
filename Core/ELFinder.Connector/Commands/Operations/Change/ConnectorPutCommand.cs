using ELFinder.Connector.Commands.Operations.Common;
using ELFinder.Connector.Commands.Results.Change;

namespace ELFinder.Connector.Commands.Operations.Change
{

    /// <summary>
    /// Connector command: Put
    /// </summary>
    public class ConnectorPutCommand : ConnectorSingleTargetCommand<PutCommandResult>
    {

        #region Properties

        /// <summary>
        /// Content
        /// </summary>
        public string Content { get; set; } 

        #endregion

        #region Overrides

        /// <summary>
        /// Execute command
        /// </summary>
        /// <returns>Result</returns>
        public override PutCommandResult Execute()
        {

            return Driver.Put(Target, Content);

        }

        #endregion
         
    }
}