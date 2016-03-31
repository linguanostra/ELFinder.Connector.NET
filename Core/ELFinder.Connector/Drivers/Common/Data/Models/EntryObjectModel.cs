using System;
using Newtonsoft.Json;

namespace ELFinder.Connector.Drivers.Common.Data.Models
{

    /// <summary>
    /// File/Directory entry object base model
    /// </summary>
    public abstract class EntryObjectModel
    {

        #region Constructors

        /// <summary>
        /// Unix origin date time
        /// </summary>
        protected static readonly DateTime UnixOrigin = new DateTime(1970, 1, 1, 0, 0, 0);

        #endregion

        #region Properties

        /// <summary>
        /// Name of file/directory
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; protected set; }

        /// <summary>
        /// Hash of current file/dir path, first symbol must be letter, symbols before _underline_ - volume id
        /// </summary>
        [JsonProperty(PropertyName = "hash")]
        public string Hash { get; protected set; }

        /// <summary>
        /// MIME type
        /// </summary>
        [JsonProperty(PropertyName = "mime")]
        public string Mime { get; protected set; }

        /// <summary>
        /// File modification time in unix timestamp
        /// </summary>
        [JsonProperty(PropertyName = "ts")]
        public long UnixTimeStamp { get; protected set; }

        /// <summary>
        /// File size in bytes
        /// </summary>
        [JsonProperty(PropertyName = "size")]
        public long Size { get; protected set; }

        /// <summary>
        /// Is readable
        /// </summary>
        [JsonProperty(PropertyName = "read")]
        public bool Read { get; protected set; }

        /// <summary>
        /// Is writable
        /// </summary>
        [JsonProperty(PropertyName = "write")]
        public bool Write { get; protected set; }

        /// <summary>
        /// Is file locked. If locked that object cannot be deleted and renamed.
        /// </summary>
        [JsonProperty(PropertyName = "locked")]
        public bool Locked { get; protected set; }

        #endregion

    }
}