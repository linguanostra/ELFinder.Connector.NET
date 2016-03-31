using ELFinder.Connector.Commands.Results.Common;
using Newtonsoft.Json;

namespace ELFinder.Connector.Commands.Results.Content
{
    /// <summary>
    /// Command result: Get
    /// </summary>
    public class GetCommandResult : CommandResult
    {

        #region Properties

        /// <summary>
        /// Content
        /// </summary>
        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }

        #endregion

    }
}