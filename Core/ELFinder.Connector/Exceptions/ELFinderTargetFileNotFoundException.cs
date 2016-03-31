using System.IO;

namespace ELFinder.Connector.Exceptions
{
    /// <summary>
    /// ELFinder target file not found exception
    /// </summary>
    public class ELFinderTargetFileNotFoundException : ELFinderTargetNotFoundException
    {

        #region Constructors

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="target">Target</param>
        public ELFinderTargetFileNotFoundException(string target) : base(target)
        {
        }

        #endregion
        
    }
}