using Newtonsoft.Json;

namespace ELFinder.Connector.Commands.Results.Common.Data
{

    /// <summary>
    /// Debug result data
    /// </summary>
    public class DebugResultData : CommandResult
    {

        #region Properties

        /// <summary>
        /// Connector
        /// </summary>
        [JsonProperty(PropertyName = "connector")]
        public string Connector => ".net";

        #endregion

    }
}