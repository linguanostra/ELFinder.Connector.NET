
using ELFinder.Connector.Config;

namespace ELFinder.WebServer.Nancy.Config
{

    /// <summary>
    /// Shared config
    /// </summary>
    public class SharedConfig
    {

        #region Properties

        /// <summary>
        /// ELFinder shared configuration
        /// </summary>
        public static ELFinderConfig ELFinder { get; set; }

        #endregion

    }

}