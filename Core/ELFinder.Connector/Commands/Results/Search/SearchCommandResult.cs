using System.Collections.Generic;
using ELFinder.Connector.Commands.Results.Common;
using ELFinder.Connector.Drivers.Common.Data.Models;
using Newtonsoft.Json;

namespace ELFinder.Connector.Commands.Results.Search
{

    /// <summary>
    /// Search command result
    /// </summary>
    public class SearchCommandResult : CommandResult
    {

        #region Properties

        /// <summary>
        /// Matching entries
        /// </summary>
        [JsonProperty(PropertyName = "files")]
        public List<EntryObjectModel> Files { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance
        /// </summary>
        public SearchCommandResult()
        {
            Files = new List<EntryObjectModel>();
        }

        #endregion

    }
}