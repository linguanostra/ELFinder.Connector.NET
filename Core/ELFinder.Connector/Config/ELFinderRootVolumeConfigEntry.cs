using ELFinder.Connector.Config.Interfaces;

namespace ELFinder.Connector.Config
{
    /// <summary>
    /// ELFinder root volume configuration entry
    /// </summary>
    public class ELFinderRootVolumeConfigEntry : IELFinderRootVolumeConfigEntry
    {

        #region Properties

        /// <summary>
        /// Root volume local directory
        /// </summary>
        public string Directory { get; }

        /// <summary>
        /// Root volume local start directory
        /// </summary>
        public string StartDirectory { get; }

        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// Is locked
        /// </summary>
        public bool IsLocked { get; }

        /// <summary>
        /// Is read only
        /// </summary>
        public bool IsReadOnly { get; }

        /// <summary>
        /// Is show only
        /// </summary>
        public bool IsShowOnly { get; }

        /// <summary>
        /// Overwrite files when uploading
        /// </summary>
        public bool UploadOverwrite { get; }

        /// <summary>
        /// Max upload size in kilobytes
        /// </summary>
        public int? MaxUploadSizeKb { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="directory">Directory</param>
        /// <param name="url">Url</param>
        /// <param name="isLocked">Is locked</param>
        /// <param name="isReadOnly">Is read only</param>
        /// <param name="isShowOnly">Is show only</param>
        /// <param name="uploadOverwrite">Overwrite files when uploading</param>
        /// <param name="maxUploadSizeKb">Max upload size in kilobytes</param>        
        /// <param name="startDirectory">Start directory</param>
        public ELFinderRootVolumeConfigEntry(string directory, string url = null, bool isLocked = false, bool isReadOnly = false,
            bool isShowOnly = false, bool uploadOverwrite = false, int? maxUploadSizeKb = null, string startDirectory = null)
        {
            Directory = directory?.TrimEnd('/');
            Url = url;
            IsLocked = isLocked;
            IsReadOnly = isReadOnly;
            IsShowOnly = isShowOnly;
            UploadOverwrite = uploadOverwrite;
            MaxUploadSizeKb = maxUploadSizeKb;
            StartDirectory = startDirectory?.TrimEnd('/');
        }

        #endregion

    }
}