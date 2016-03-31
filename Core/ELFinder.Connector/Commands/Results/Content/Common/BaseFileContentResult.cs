using System;
using System.IO;
using ELFinder.Connector.Commands.Results.Common;

namespace ELFinder.Connector.Commands.Results.Content.Common
{

    /// <summary>
    /// Base file content result
    /// </summary>
    public abstract class BaseFileContentResult : CommandResult
    {

        #region Properties

        /// <summary>
        /// File info
        /// </summary>
        public FileInfo File { get; set; }

        /// <summary>
        /// Content type
        /// </summary>
        public string ContentType { get; set; }

        #endregion

        #region Constructors
        
        /// <summary>
        /// Create a new instance
        /// </summary>
        protected BaseFileContentResult()
        {
        }

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="file">File</param>
        /// <param name="contentType">Content type</param>
        protected BaseFileContentResult(FileInfo file, string contentType)
        {
            File = file;
            ContentType = contentType;
        }

        #endregion

        #region Virtual methods

        /// <summary>
        /// Get content stream 
        /// </summary>
        /// <returns>Result content stream</returns>
        public virtual Stream GetContentStream()
        {

            // Check that file info is defined
            if(File == null) throw new ArgumentNullException(nameof(File));

            // Check that file exists
            if(!File.Exists) throw new FileNotFoundException();

            // Return memory stream from file
            return new FileStream(File.FullName, FileMode.Open);

        }

        #endregion

    }
}