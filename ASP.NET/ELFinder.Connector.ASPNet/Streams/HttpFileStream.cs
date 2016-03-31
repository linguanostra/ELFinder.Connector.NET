using System.IO;
using System.Web;
using ELFinder.Connector.Streams;

namespace ELFinder.Connector.ASPNet.Streams
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
        private HttpPostedFileBase SourceHttpFile { get; }

        #endregion

        #region IFileStream members

        /// <summary>
        /// File name
        /// </summary>
        public string FileName => SourceHttpFile.FileName;

        /// <summary>
        /// Content type
        /// </summary>
        public string ContentType => SourceHttpFile.ContentType;

        /// <summary>
        /// Stream instance
        /// </summary>
        public Stream Stream => SourceHttpFile.InputStream;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="sourceHttpFile">Source Http file</param>
        public HttpFileStream(HttpPostedFileBase sourceHttpFile)
        {
            SourceHttpFile = sourceHttpFile;
        }

        #endregion

    }

}