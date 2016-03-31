namespace ELFinder.Connector.Exceptions
{
    /// <summary>
    /// ELFinder volume not found exception
    /// </summary>
    public class ELFinderVolumeNotFoundException : ELFinderConnectorException
    {

        #region Properties

        /// <summary>
        /// Volume id
        /// </summary>
        public string VolumeId { get; } 

        #endregion

        #region Constructors
        
        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="volumeId">Volume id</param>
        public ELFinderVolumeNotFoundException(string volumeId)
        {
            VolumeId = volumeId;
        }

        #endregion
        
    }
}