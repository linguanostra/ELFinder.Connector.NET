using System.Collections.Generic;
using ELFinder.Connector.Commands.Results.Common;
using Newtonsoft.Json;

namespace ELFinder.Connector.Commands.Results.Image.Thumbnails
{
    /// <summary>
    /// Generate thumbnails result
    /// </summary>
    public class GenerateThumbnailsResult : CommandResult
    {

        #region Properties

        /// <summary>
        /// Images
        /// </summary>
        [JsonProperty(PropertyName = "images")]
        public Dictionary<string, string> Images { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance
        /// </summary>
        public GenerateThumbnailsResult()
        {
            Images = new Dictionary<string, string>();
        }

        #endregion

    }
}