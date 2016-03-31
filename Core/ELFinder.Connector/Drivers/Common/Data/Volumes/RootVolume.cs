namespace ELFinder.Connector.Drivers.Common.Data.Volumes
{

    /// <summary>
    /// Root volume
    /// </summary>
    public abstract class RootVolume
    {

        #region Properties

        /// <summary>
        /// Volume Id
        /// </summary>
        public string VolumeId { get; set; }

        /// <summary>
        /// Volume alias
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// Volume url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Read only (users can't change file)
        /// </summary>
        public bool IsReadOnly { get; set; }

        /// <summary>
        /// View files only (and cannot download).
        /// </summary>
        public bool IsShowOnly { get; set; }

        /// <summary>
        /// Locked (user can't remove, rename or delete files or subdirectories)  
        /// </summary>
        public bool IsLocked { get; set; }

        #endregion

    }
}