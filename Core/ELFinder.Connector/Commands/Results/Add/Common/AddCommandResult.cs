using System.Collections.Generic;
using ELFinder.Connector.Commands.Results.Common;
using ELFinder.Connector.Drivers.Common.Data.Models;
using Newtonsoft.Json;

namespace ELFinder.Connector.Commands.Results.Add.Common
{

    /// <summary>
    /// Add base command result
    /// </summary>
    public abstract class AddCommandResult : CommandResult
    {

        #region Properties

        /// <summary>
        /// Added entries
        /// </summary>
        [JsonProperty(PropertyName = "added")]
        public List<EntryObjectModel> Added { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance
        /// </summary>
        protected AddCommandResult()
        {
            Added = new List<EntryObjectModel>();
        }

        #endregion

    }
}