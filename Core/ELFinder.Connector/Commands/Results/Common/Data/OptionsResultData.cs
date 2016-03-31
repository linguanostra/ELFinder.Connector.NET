using System.Collections.Generic;
using ELFinder.Connector.Drivers.FileSystem.Volumes.Info;
using Newtonsoft.Json;

namespace ELFinder.Connector.Commands.Results.Common.Data
{

    /// <summary>
    /// Options result data
    /// </summary>
    public class OptionsResultData : CommandResult
    {

        #region Properties

        /// <summary>
        /// Copy overwrite
        /// </summary>
        [JsonProperty(PropertyName = "copyOverwrite")]
        public bool IsCopyOverwrite => true;

        /// <summary>
        /// Separator
        /// </summary>
        [JsonProperty(PropertyName = "separator")]
        public string Separator => "/";

        /// <summary>
        /// Path
        /// </summary>
        [JsonProperty(PropertyName = "path")]
        public string Path { get; set; }

        /// <summary>
        /// Thumbnails url
        /// </summary>
        [JsonProperty(PropertyName = "tmbUrl")]
        public string ThumbnailsUrl { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        /// <summary>
        /// Archivers
        /// </summary>
        [JsonProperty(PropertyName = "archivers")]
        public ArchiveResultData Archivers { get; set; }

        /// <summary>
        /// Disabled options
        /// </summary>
        [JsonProperty(PropertyName = "disabled")]
        public IEnumerable<string> Disabled => new[] { "extract", "create" };

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="fullPath">Path info</param>
        public OptionsResultData(FileSystemVolumeDirectoryInfo fullPath)
        {

            // Assign values
            
            // Root path alias
            Path = fullPath.Root.Alias;

            // Relative path
            if (!string.IsNullOrEmpty(fullPath.RelativePath))
            {
                Path += Separator + fullPath.RelativePath.Replace(@"\", Separator);
            }

            // Url
            Url = fullPath.Root.Url ?? string.Empty;
            ThumbnailsUrl = fullPath.Root.ThumbnailsUrl ?? string.Empty;

            // Archivers
            Archivers = new ArchiveResultData();

        }

        #endregion

    }
}