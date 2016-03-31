namespace ELFinder.Connector.Exceptions
{
    /// <summary>
    /// ELFinder target directory not found exception
    /// </summary>
    public class ELFinderTargetDirectoryNotFoundException : ELFinderTargetNotFoundException
    {

        #region Constructors

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="target">Target</param>
        public ELFinderTargetDirectoryNotFoundException(string target) : base(target)
        {
        }

        #endregion
        
    }
}