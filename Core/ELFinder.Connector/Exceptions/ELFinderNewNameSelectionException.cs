namespace ELFinder.Connector.Exceptions
{

    /// <summary>
    /// ELFinder new name selection exception
    /// </summary>
    public class ELFinderNewNameSelectionException : ELFinderConnectorException
    {

        #region Properties

        /// <summary>
        /// New name
        /// </summary>
        public string NewName { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="newName">New name</param>
        public ELFinderNewNameSelectionException(string newName)
        {
            NewName = newName;
        }

        #endregion

    }

}