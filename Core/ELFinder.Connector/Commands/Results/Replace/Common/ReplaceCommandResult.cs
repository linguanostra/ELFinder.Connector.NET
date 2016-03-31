using System.Collections.Generic;
using ELFinder.Connector.Commands.Results.Add.Common;
using Newtonsoft.Json;

namespace ELFinder.Connector.Commands.Results.Replace.Common
{
    /// <summary>
    /// Replace base command result
    /// </summary>
    public abstract class ReplaceCommandResult : AddCommandResult
    {

        #region Properties

        /// <summary>
        /// Removed entries
        /// </summary>
        [JsonProperty(PropertyName = "removed")]
        public List<string> Removed { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance
        /// </summary>
        protected ReplaceCommandResult()
        {
            Removed = new List<string>();
        }

        #endregion

    }
}