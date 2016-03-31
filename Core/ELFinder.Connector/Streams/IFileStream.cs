using System.IO;

namespace ELFinder.Connector.Streams
{

    /// <summary>
    /// File stream interface
    /// </summary>
    public interface IFileStream
    {

        #region Properties

        /// <summary>
        /// File name
        /// </summary>
        string FileName { get; } 

        /// <summary>
        /// Content type
        /// </summary>
        string ContentType { get; } 

        /// <summary>
        /// Stream instance
        /// </summary>
        Stream Stream { get; }

        #endregion
         
    }
}