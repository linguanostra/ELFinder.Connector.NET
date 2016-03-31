using ELFinder.Connector.Drivers.Common.Data.Models;
using Newtonsoft.Json;

namespace ELFinder.Connector.Drivers.FileSystem.Models.Files.Common
{

    /// <summary>
    /// Base file entry object model
    /// </summary>
    public abstract class BaseFileEntryObjectModel : EntryObjectModel
    {

        #region Properties

        /// <summary>
        /// Hash of parent directory. Required except roots dirs.
        /// </summary>
        [JsonProperty(PropertyName = "phash")]
        public string ParentHash { get; set; }

        #endregion

    }

}