using ELFinder.Connector.Commands.Operations.Image.Common;
using ELFinder.Connector.Commands.Results.Image;

namespace ELFinder.Connector.Commands.Operations.Image
{
    /// <summary>
    /// ELFinder connector command: Rotate
    /// </summary>
    public class ConnectorRotateImageCommand : ConnectorImageCommand<RotateImageResult>
    {

        #region Properties

        /// <summary>
        /// Degree
        /// </summary>
        public int Degree { get; set; }

        #endregion

        #region Overrides

        /// <summary>
        /// Execute command
        /// </summary>
        /// <returns>Result</returns>
        public override RotateImageResult Execute()
        {
            return Driver.Rotate(Target, Degree);
        }

        #endregion

    }
}