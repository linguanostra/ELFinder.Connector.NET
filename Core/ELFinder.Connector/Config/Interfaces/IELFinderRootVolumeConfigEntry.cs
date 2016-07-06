namespace ELFinder.Connector.Config.Interfaces
{

    /// <summary>
    /// ELFinder root volume configuration entry interface
    /// </summary>
    public interface IELFinderRootVolumeConfigEntry
    {

        #region Properties

        /// <summary>
        /// Root volume local directory
        /// </summary>
        string Directory { get; }

        /// <summary>
        /// Root volume local start directory
        /// </summary>
        string StartDirectory { get; }

        /// <summary>
        /// Url
        /// </summary>
        string Url { get; }

        /// <summary>
        /// Is locked
        /// </summary>
        bool IsLocked { get; }

        /// <summary>
        /// Is read only
        /// </summary>
        bool IsReadOnly { get; }

        /// <summary>
        /// Is show only
        /// </summary>
        bool IsShowOnly { get; }

        /// <summary>
        /// Overwrite files when uploading
        /// </summary>
        bool UploadOverwrite { get; }

        /// <summary>
        /// Max upload size in kilobytes. 
        /// </summary>
        int? MaxUploadSizeKb { get; }

        #endregion

    }
}