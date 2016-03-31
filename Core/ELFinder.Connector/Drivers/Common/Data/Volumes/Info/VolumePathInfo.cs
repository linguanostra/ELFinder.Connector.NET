namespace ELFinder.Connector.Drivers.Common.Data.Volumes.Info
{

    /// <summary>
    /// Volume path info
    /// </summary>
    public abstract class VolumePathInfo<TVolume>
        where TVolume : RootVolume
    {

        #region Properties

        /// <summary>
        /// Root volume
        /// </summary>
        public TVolume Root { get;}

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="root">Root volume</param>
        protected VolumePathInfo(TVolume root)
        {
            Root = root;
        }

        #endregion

    }
}