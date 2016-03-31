using System.IO;
using ELFinder.Connector.Commands.Results.Content.Common;

namespace ELFinder.Connector.Commands.Results.Content
{

    /// <summary>
    /// Download file command result
    /// </summary>
    public class FileCommandResult : BaseFileContentResult
    {

        #region Properties

        /// <summary>
        /// Download file
        /// </summary>
        public bool IsDownload { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="filePath">File path</param>
        /// <param name="contentType">Content type</param>
        /// <param name="isDownload">Download file</param>
        public FileCommandResult(string filePath, string contentType, bool isDownload) : 
            this(new FileInfo(filePath), contentType, isDownload)
        {
        }

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="file">File info</param>
        /// <param name="contentType">Content type</param>
        /// <param name="isDownload">Download file</param>
        public FileCommandResult(FileInfo file, string contentType, bool isDownload) : base(file, contentType)
        {
            IsDownload = isDownload;
        }

        #endregion

    }

}