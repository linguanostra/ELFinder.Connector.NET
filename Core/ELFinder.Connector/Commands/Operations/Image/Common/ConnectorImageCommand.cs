using ELFinder.Connector.Commands.Operations.Common;
using ELFinder.Connector.Commands.Results.Image.Common;

namespace ELFinder.Connector.Commands.Operations.Image.Common
{

    /// <summary>
    /// Connector image command
    /// </summary>
    /// <typeparam name="TResult">Result type</typeparam>
    public abstract class ConnectorImageCommand<TResult> : ConnectorSingleTargetCommand<TResult>
        where TResult : ChangeImageResult
    {

        #region Properties

        /// <summary>
        /// Mode
        /// </summary>
        public string Mode { get; set; } 

        #endregion
          
    }

}