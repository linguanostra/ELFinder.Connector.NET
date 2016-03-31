namespace ELFinder.Connector.Exceptions
{
    /// <summary>
    /// ELFinder parent directory not found exception
    /// </summary>
    public class ELFinderParentDirectoryNotFoundException : ELFinderTargetNotFoundException
    {

        #region Constructors

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="target">Target</param>
        public ELFinderParentDirectoryNotFoundException(string target) : base(target)
        {
        }

        #endregion

    }
}