using System.Collections.Generic;
using ELFinder.Connector.Commands.Results.Common;
using ELFinder.Connector.Drivers.Common.Data.Models;
using Newtonsoft.Json;

namespace ELFinder.Connector.Commands.Results.Tree.Common
{
    /// <summary>
    /// Tree base command result
    /// </summary>
    public abstract class TreeBaseCommandResult : CommandResult
    {

        #region Properties

        /// <summary>
        /// Tree
        /// </summary>
        [JsonProperty(PropertyName = "tree")]
        public List<EntryObjectModel> Tree { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance
        /// </summary>
        protected TreeBaseCommandResult()
        {
            Tree = new List<EntryObjectModel>();
        }

        #endregion

    }
}