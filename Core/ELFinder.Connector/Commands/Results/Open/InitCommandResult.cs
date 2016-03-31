using System.Collections.Generic;
using ELFinder.Connector.Commands.Results.Common.Data;
using ELFinder.Connector.Commands.Results.Open.Common;
using ELFinder.Connector.Drivers.FileSystem.Models.Directories.Common;
using Newtonsoft.Json;

namespace ELFinder.Connector.Commands.Results.Open
{

    /// <summary>
    /// Open/Init command result
    /// </summary>
    public class InitCommandResult : OpenBaseCommandResult
    {

        #region Properties

        /// <summary>
        /// API version
        /// </summary>
        [JsonProperty(PropertyName = "api")]
        public string Api => "2.0";

        /// <summary>
        /// Upload max size
        /// </summary>
        [JsonProperty(PropertyName = "uplMaxSize")]
        public string UploadMaxSize { get; set; }

        /// <summary>
        /// Drivers
        /// </summary>
        [JsonProperty(PropertyName = "netDrivers")]
        public IEnumerable<string> NetDrivers => Empty;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="currentWorkingDirectory">Current working directory</param>
        /// <param name="options">Options</param>
        public InitCommandResult(BaseDirectoryEntryObjectModel currentWorkingDirectory, OptionsResultData options = null) 
            : base(currentWorkingDirectory, options)
        {
        }

        #endregion

    }
}