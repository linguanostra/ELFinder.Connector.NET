using ELFinder.Connector.Commands.Operations.Common;
using ELFinder.Connector.Commands.Results.Image.Thumbnails;

namespace ELFinder.Connector.Commands.Operations.Image.Thumbnails
{

    /// <summary>
    /// Connector command: Get thumbnail
    /// </summary>
    public class ConnectorGetThumbnailCommand : ConnectorSingleTargetCommand<GetThumbnailResult>
    {

        #region Overrides

        /// <summary>
        /// Execute command
        /// </summary>
        /// <returns>Result</returns>
        public override GetThumbnailResult Execute()
        {

            return Driver.GetThumbnail(Target);

        }

        #endregion   

    }
}