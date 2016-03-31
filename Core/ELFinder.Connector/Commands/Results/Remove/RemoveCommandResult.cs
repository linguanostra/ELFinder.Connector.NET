using System.Collections.Generic;
using ELFinder.Connector.Commands.Results.Common;
using Newtonsoft.Json;

namespace ELFinder.Connector.Commands.Results.Remove
{
    /// <summary>
    /// Remove command result
    /// </summary>
    public class RemoveCommandResult : CommandResult
    {

        #region Properties

        /// <summary>
        /// Removed entries
        /// </summary>
        [JsonProperty(PropertyName = "removed")]
        public List<string> Removed { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance
        /// </summary>
        public RemoveCommandResult()
        {
            Removed = new List<string>();
        }

        #endregion

    }
}