namespace ELFinder.Connector.Commands.Operations.Common.Interfaces
{

    /// <summary>
    /// Connector single target command interface
    /// </summary>
    public interface IConnectorSingleTargetCommand : IConnectorCommand
    {

        #region Properties

        /// <summary>
        /// Hash of target
        /// </summary>
        string Target { get; set; }

        #endregion

    }
}