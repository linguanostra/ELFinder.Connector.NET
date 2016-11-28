using System.IO;
using System.Linq;

namespace ELFinder.Connector.Drivers.FileSystem.Extensions
{

    /// <summary>
    /// File system info extensions
    /// </summary>
    public static class FIleSystemInfoExtensions
    {

        #region Extension methods

        /// <summary>
        /// Get visible files in directory
        /// </summary>
        /// <param name="info">Directory info</param>
        /// <returns>Result directories</returns>
        public static FileInfo[] GetVisibleFiles(this DirectoryInfo info)
        {
            try
            {
                return info.GetFiles().Where(x => !x.IsHidden()).ToArray();
            }
            catch (System.Exception)
            {
                return new FileInfo[0];
            }
        }

        /// <summary>
        /// Get visible directories
        /// </summary>
        /// <param name="info">Directory info</param>
        /// <returns>Result directories</returns>
        public static DirectoryInfo[] GetVisibleDirectories(this DirectoryInfo info)
        {
            try
            {
                return info.GetDirectories().Where(x => !x.IsHidden()).ToArray();
            }
            catch (System.Exception)
            {
                return new DirectoryInfo[0];
            }
        }

        /// <summary>
        /// Get if file is hidden
        /// </summary>
        /// <param name="info">File info</param>
        /// <returns>True/False based on result</returns>
        public static bool IsHidden(this FileSystemInfo info)
        {
            return (info.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden;
        }

        #endregion

    }
}