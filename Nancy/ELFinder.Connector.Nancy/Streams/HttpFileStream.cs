using System.IO;
using ELFinder.Connector.Streams;
using Nancy;

namespace ELFinder.Connector.Nancy.Streams
{

    /// <summary>
    /// Http file stream
    /// </summary>
    public class HttpFileStream : IFileStream
    {

        #region Properties

        /// <summary>
        /// Source Http file
        /// </summary>
        private HttpFile SourceHttpFile { get; }

        #endregion

        #region IFileStream members

        /// <summary>
        /// File name
        /// </summary>
        public string FileName => SourceHttpFile.Name;

        /// <summary>
        /// Content type
        /// </summary>
        public string ContentType => SourceHttpFile.ContentType;

        /// <summary>
        /// Stream instance
        /// </summary>
        public Stream Stream => SourceHttpFile.Value;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="sourceHttpFile">Source Http file</param>
        public HttpFileStream(HttpFile sourceHttpFile)
        {
            SourceHttpFile = sourceHttpFile;
        }

        #endregion

    }

}