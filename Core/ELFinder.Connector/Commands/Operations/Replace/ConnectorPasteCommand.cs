using ELFinder.Connector.Commands.Operations.Common;
using ELFinder.Connector.Commands.Results.Replace;

namespace ELFinder.Connector.Commands.Operations.Replace
{
    /// <summary>
    /// Connector command: Paste
    /// </summary>
    public class ConnectorPasteCommand : ConnectorMultipleTargetsCommand<PasteCommandResult>
    {

        #region Properties

        /// <summary>
        /// Source
        /// </summary>
        public string Src { get; set; }

        /// <summary>
        /// Destination
        /// </summary>
        public string Dst { get; set; }

        /// <summary>
        /// Is cut
        /// </summary>
        public bool Cut { get; set; } 

        #endregion

        #region Overrides

        /// <summary>
        /// Execute command
        /// </summary>
        /// <returns>Result</returns>
        public override PasteCommandResult Execute()
        {
            return Driver.Paste(Src, Dst, Targets, Cut);            
        }

        #endregion
        
    }
}