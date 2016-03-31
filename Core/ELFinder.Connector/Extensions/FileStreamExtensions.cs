using System.IO;
using ELFinder.Connector.Streams;

namespace ELFinder.Connector.Extensions
{

    /// <summary>
    /// File stream extensions
    /// </summary>
    public static class FileStreamExtensions
    {

        #region Extension methods

        /// <summary>
        /// Save file to given path
        /// </summary>
        /// <param name="file">File</param>
        /// <param name="path">Path</param>
        public static void SaveAs(this IFileStream file, string path)
        {

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                file.Stream.CopyTo(fileStream);
            }

        }

        #endregion
         
    }
}