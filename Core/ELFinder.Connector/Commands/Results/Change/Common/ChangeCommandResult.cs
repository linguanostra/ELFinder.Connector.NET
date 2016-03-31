using System.Collections.Generic;
using ELFinder.Connector.Commands.Results.Common;
using ELFinder.Connector.Drivers.FileSystem.Models.Files.Common;
using Newtonsoft.Json;

namespace ELFinder.Connector.Commands.Results.Change.Common
{

    /// <summary>
    /// Change command result
    /// </summary>
    public abstract class ChangeCommandResult : CommandResult
    {

        #region Properties

        /// <summary>
        /// Change files
        /// </summary>
        [JsonProperty(PropertyName = "changed")]
        public List<BaseFileEntryObjectModel> Changed { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance
        /// </summary>
        protected ChangeCommandResult()
        {
            Changed = new List<BaseFileEntryObjectModel>();
        }

        #endregion

    }
}