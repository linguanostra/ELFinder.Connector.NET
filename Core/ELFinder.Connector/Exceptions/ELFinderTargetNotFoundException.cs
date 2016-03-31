namespace ELFinder.Connector.Exceptions
{
    /// <summary>
    /// ELFinder target not found exception
    /// </summary>
    public class ELFinderTargetNotFoundException : ELFinderTargetException
    {

        #region Constructors

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="target">Target</param>
        public ELFinderTargetNotFoundException(string target) : base(target)
        {
        }

        #endregion
         
    }
}