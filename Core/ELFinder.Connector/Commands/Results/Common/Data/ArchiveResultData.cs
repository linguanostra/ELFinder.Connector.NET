using System.Collections.Generic;
using Newtonsoft.Json;

namespace ELFinder.Connector.Commands.Results.Common.Data
{
    /// <summary>
    /// Archive result data
    /// </summary>
    public class ArchiveResultData : CommandResult
    {

        #region Properties

        /// <summary>
        /// Create
        /// </summary>
        [JsonProperty(PropertyName = "create")]
        public IEnumerable<string> Create => Empty;

        /// <summary>
        /// Extract
        /// </summary>
        [JsonProperty(PropertyName = "extract")]
        public IEnumerable<string> Extract => Empty;

        #endregion

    }
}