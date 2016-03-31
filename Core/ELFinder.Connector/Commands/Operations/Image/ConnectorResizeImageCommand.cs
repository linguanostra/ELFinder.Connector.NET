using ELFinder.Connector.Commands.Operations.Image.Common;
using ELFinder.Connector.Commands.Results.Image;

namespace ELFinder.Connector.Commands.Operations.Image
{
    /// <summary>
    /// ELFinder connector command: Resize
    /// </summary>
    public class ConnectorResizeImageCommand : ConnectorImageCommand<ResizeImageResult>
    {

        #region Properties

        /// <summary>
        /// Width
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Height
        /// </summary>
        public int Height { get; set; }

        #endregion

        #region Overrides

        /// <summary>
        /// Execute command
        /// </summary>
        /// <returns>Result</returns>
        public override ResizeImageResult Execute()
        {
            return Driver.Resize(Target, Width, Height);
        }

        #endregion

    }
}