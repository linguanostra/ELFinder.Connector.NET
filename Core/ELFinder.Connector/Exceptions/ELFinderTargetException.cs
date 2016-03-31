namespace ELFinder.Connector.Exceptions
{
    /// <summary>
    /// ELFinder target exception
    /// </summary>
    public abstract class ELFinderTargetException : ELFinderConnectorException
    {

        #region Properties

        /// <summary>
        /// Target
        /// </summary>
        public string Target { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="target">Target</param>
        protected ELFinderTargetException(string target)
        {
            Target = target;
        }

        #endregion

    }
}