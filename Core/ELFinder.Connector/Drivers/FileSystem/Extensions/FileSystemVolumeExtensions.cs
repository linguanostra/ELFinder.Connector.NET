using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ELFinder.Connector.Drivers.Common.Data.Models;
using ELFinder.Connector.Drivers.FileSystem.Models.Directories;
using ELFinder.Connector.Drivers.FileSystem.Models.Files;
using ELFinder.Connector.Drivers.FileSystem.Volumes;
using ELFinder.Connector.Exceptions;
using ELFinder.Connector.Utils;

namespace ELFinder.Connector.Drivers.FileSystem.Extensions
{

    /// <summary>
    /// File system volume extensions
    /// </summary>
    public static class FileSystemVolumeExtensions
    {

        #region Extension methods

        /// <summary>
        /// Search accessible files in given volume
        /// </summary>
        /// <typeparam name="TVolume">Volume type</typeparam>
        /// <param name="volume">Volume</param>
        /// <param name="searchTerm">Search term</param>
        /// <returns>Result list</returns>
        public static IEnumerable<EntryObjectModel> SearchAccessibleFilesAndDirectories<TVolume>(this TVolume volume, string searchTerm)
            where TVolume : FileSystemRootVolume
        {

            // Invoke overload using volume root path
            return volume.SearchAccessibleFilesAndDirectories(volume.Directory.FullName, searchTerm);

        }

        /// <summary>
        /// Search accessible files and directories in given volume
        /// </summary>
        /// <typeparam name="TVolume">Volume type</typeparam>
        /// <param name="volume">Volume</param>
        /// <param name="root">Root path</param>
        /// <param name="searchTerm">Search term</param>
        /// <returns>Result list</returns>
        public static IEnumerable<EntryObjectModel> SearchAccessibleFilesAndDirectories<TVolume>(this TVolume volume, string root,
            string searchTerm)
            where TVolume : FileSystemRootVolume
        {

            var entries = new List<EntryObjectModel>();

            foreach (var directory in Directory.EnumerateDirectories(root).Where(m => m.Contains(searchTerm)))
            {
                entries.Add(DirectoryEntryObjectModel.Create(new DirectoryInfo(directory), volume));
            }
            foreach (var file in Directory.EnumerateFiles(root).Where(m => m.Contains(searchTerm)))
            {

                // Check if file refers to an image or not
                if (ImagingUtils.CanProcessFile(file))
                {

                    // Add image to added entries
                    entries.Add(
                        ImageEntryObjectModel.Create(new FileInfo(file), volume));

                }
                else
                {

                    // Add file to added entries
                    entries.Add(
                        FileEntryObjectModel.Create(new FileInfo(file), volume));

                }
                
            }
            foreach (var subDir in Directory.EnumerateDirectories(root))
            {
                try
                {
                    entries.AddRange(volume.SearchAccessibleFilesAndDirectories(subDir, searchTerm));
                }
                catch (UnauthorizedAccessException)
                {
                    // Do nothing
                }
            }

            return entries;

        }

        /// <summary>
        /// Get parent path from volume
        /// </summary>
        /// <typeparam name="TVolume">Volume type</typeparam>
        /// <param name="volume">volume</param>
        /// <param name="info">File info</param>
        /// <returns>Parent path</returns>
        public static string GetParentPath<TVolume>(this TVolume volume, FileInfo info)
            where TVolume : FileSystemRootVolume
        {

            // Get directories names
            var parentDir = info.Directory?.FullName;
            var rootVolumeDir = volume.Directory?.FullName;

            // Compute parent path relative to volume
            if (parentDir != null
                && rootVolumeDir != null
                && parentDir.Length > rootVolumeDir.Length)
            {

                // Get it as substring
                return parentDir.Substring(rootVolumeDir.Length);

            }

            // No parent can be computed
            return string.Empty;

        }

        /// <summary>
        /// Get parent path from volume
        /// </summary>
        /// <typeparam name="TVolume">Volume type</typeparam>
        /// <param name="volume">Volume</param>
        /// <param name="directoryInfo">Directory info</param>
        /// <returns>Parent path</returns>
        public static string GetParentPath<TVolume>(this TVolume volume, DirectoryInfo directoryInfo)
            where TVolume : FileSystemRootVolume
        {

            // Get directories names
            var rootVolumeDir = volume.Directory?.FullName;

            // Compute parent path relative to volume
            if (rootVolumeDir != null 
                && directoryInfo.FullName.Length > rootVolumeDir.Length)
            {
                
                // Get it as substring
                return directoryInfo.FullName.Substring(rootVolumeDir.Length);

            }

            // No parent can be computed
            return string.Empty;

        }

        /// <summary>
        /// Get startup volume (Priority for first with a start path)
        /// </summary>
        /// <typeparam name="TVolume">Volume type</typeparam>
        /// <param name="volumes">Volumes list</param>
        /// <returns>Result root volume</returns>
        public static TVolume GetStartupVolume<TVolume>(this IEnumerable<TVolume> volumes)
            where TVolume : FileSystemRootVolume
        {

            // Normalize volumes list
            var normalizedVolumes = volumes as IList<TVolume> ?? volumes.ToList();

            // Validate volumes list            
            if (!normalizedVolumes.Any()) throw new ELFinderNoVolumesDefinedException();

            // Get first volume with a start path defined
            var volume = normalizedVolumes.FirstOrDefault(x => x.StartDirectory != null);

            // Check if it was found, otherwise use the first one
            return volume ?? normalizedVolumes.First();

        }

        #endregion

    }
}