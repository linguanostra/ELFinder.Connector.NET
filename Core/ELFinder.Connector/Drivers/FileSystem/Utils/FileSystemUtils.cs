using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ELFinder.Connector.Drivers.FileSystem.Utils
{

    /// <summary>
    /// File system utils
    /// </summary>
    public class FileSystemUtils
    {

        #region Static methods

        /// <summary>
        /// Copy directories
        /// </summary>
        /// <param name="sourceDir">Source directory</param>
        /// <param name="destDirName">Destination directory name</param>
        /// <param name="copySubDirs">Copy sub directories</param>
        public static void DirectoryCopy(DirectoryInfo sourceDir, string destDirName, bool copySubDirs)
        {

            // Get directories
            var dirs = sourceDir.GetDirectories();

            // If the source directory does not exist, throw an exception.
            if (!sourceDir.Exists)
            {
                throw new DirectoryNotFoundException(sourceDir.FullName);
            }

            // If the destination directory does not exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the file contents of the directory to copy.
            var files = sourceDir.GetFiles();

            foreach (var file in files)
            {

                // Create the path to the new copy of the file.
                var tempPath = Path.Combine(destDirName, file.Name);

                // Copy the file.
                file.CopyTo(tempPath, false);

            }

            // If copySubDirs is true, copy the subdirectories.
            if (copySubDirs)
            {

                foreach (var subdir in dirs)
                {
                    // Create the subdirectory.
                    var temppath = Path.Combine(destDirName, subdir.Name);

                    // Copy the subdirectories.
                    DirectoryCopy(subdir, temppath, true);

                }
            }
        }

        /// <summary>
        /// Get name for duplicating file
        /// </summary>
        /// <param name="file">File</param>
        /// <returns>Duplicated name</returns>
        public static string GetDuplicatedName(FileInfo file)
        {

            var parentPath = file.DirectoryName;
            var name = Path.GetFileNameWithoutExtension(file.Name);
            var ext = file.Extension;

            var newName = $@"{parentPath}\{name} copy{ext}";
            if (!File.Exists(newName))
            {
                return newName;
            }
            else
            {
                var finded = false;
                for (var i = 1; i < 10 && !finded; i++)
                {
                    newName = $@"{parentPath}\{name} copy {i}{ext}";
                    if (!File.Exists(newName))
                        finded = true;
                }
                if (!finded)
                    newName = $@"{parentPath}\{name} copy {Guid.NewGuid()}{ext}";
            }

            return newName;

        }

        /// <summary>
        /// Get MD5 checksum for file
        /// </summary>
        /// <param name="info">File info</param>
        /// <returns>Result</returns>
        public static string GetFileMd5(FileInfo info)
        {
            return GetFileMd5(info.Name, info.LastWriteTimeUtc);
        }

        /// <summary>
        /// Get MD5 checksum for file
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <param name="modified">Date last modified</param>
        /// <returns>Checksum</returns>
        public static string GetFileMd5(string fileName, DateTime modified)
        {

            // Get encoder & crypto provider
            var encoder = Encoding.UTF8.GetEncoder();
            var cryptoProvider = new MD5CryptoServiceProvider();

            // Normalize filename
            fileName += modified.ToFileTimeUtc();

            // Get hash
            var fileNameChars = fileName.ToCharArray();
            var buffer = new byte[encoder.GetByteCount(fileNameChars, 0, fileName.Length, true)];
            encoder.GetBytes(fileNameChars, 0, fileName.Length, buffer, 0, true);
            return BitConverter.ToString(cryptoProvider.ComputeHash(buffer)).Replace("-", string.Empty);

        }

        #endregion

    }
}