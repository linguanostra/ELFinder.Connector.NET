using ELFinder.Connector.Commands.Operations.Image.Common;
using ELFinder.Connector.Commands.Results.Image;

namespace ELFinder.Connector.Commands.Operations.Image
{
    /// <summary>
    /// ELFinder connector command: Crop
    /// </summary>
    public class ConnectorCropImageCommand : ConnectorImageCommand<CropImageResult>
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

        /// <summary>
        /// X coordinate
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Y coordinate
        /// </summary>
        public int Y { get; set; }

        #endregion

        #region Overrides

        /// <summary>
        /// Execute command
        /// </summary>
        /// <returns>Result</returns>
        public override CropImageResult Execute()
        {
            return Driver.Crop(Target, X, Y, Width, Height);
        }

        #endregion

    }
}