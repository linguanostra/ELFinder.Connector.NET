using ELFinder.Connector.Commands.Operations.Common;
using ELFinder.Connector.Commands.Results.Image.Thumbnails;

namespace ELFinder.Connector.Commands.Operations.Image.Thumbnails
{

    /// <summary>
    /// Connector command: Generate thumbnails
    /// </summary>
    public class ConnectorGenerateThumbnailsCommand : ConnectorMultipleTargetsCommand<GenerateThumbnailsResult>
    {

        #region Overrides

        /// <summary>
        /// Execute command
        /// </summary>
        /// <returns>Result</returns>
        public override GenerateThumbnailsResult Execute()
        {
            return Driver.GenerateThumbnails(Targets);
        }

        #endregion
         
    }
}