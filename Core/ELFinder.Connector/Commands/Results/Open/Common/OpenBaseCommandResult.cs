using System.Collections.Generic;
using ELFinder.Connector.Commands.Results.Common;
using ELFinder.Connector.Commands.Results.Common.Data;
using ELFinder.Connector.Drivers.Common.Data.Models;
using ELFinder.Connector.Drivers.FileSystem.Models.Directories.Common;
using Newtonsoft.Json;

namespace ELFinder.Connector.Commands.Results.Open.Common
{

    /// <summary>
    /// Open command base result
    /// </summary>
    public abstract class OpenBaseCommandResult : CommandResult
    {

        #region Properties

        /// <summary>
        /// Files
        /// </summary>
        [JsonProperty(PropertyName = "files")]
        public List<EntryObjectModel> Files { get; }

        /// <summary>
        /// Current working directory
        /// </summary>
        [JsonProperty(PropertyName = "cwd")]
        public EntryObjectModel CurrentWorkingDirectory { get; }

        /// <summary>
        /// Options
        /// </summary>
        [JsonProperty(PropertyName = "options")]
        public OptionsResultData Options { get; }

        /// <summary>
        /// Debug info
        /// </summary>
        [JsonProperty(PropertyName = "debug")]
        public DebugResultData Debug { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="currentWorkingDirectory">Current working directory</param>
        /// <param name="options">Options</param>
        protected OpenBaseCommandResult(BaseDirectoryEntryObjectModel currentWorkingDirectory, OptionsResultData options = null)
        {
            
            // Assign params
            CurrentWorkingDirectory = currentWorkingDirectory;
            Options = options;

            // Initialize values
            Files = new List<EntryObjectModel>();
            Debug = new DebugResultData();

        }

        #endregion

    }
}