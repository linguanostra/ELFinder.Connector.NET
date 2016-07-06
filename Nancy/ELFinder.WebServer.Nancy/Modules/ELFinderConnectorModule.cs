using ELFinder.Connector.Nancy.Modules;
using ELFinder.WebServer.Nancy.Config;

namespace ELFinder.WebServer.Nancy.Modules
{

    /// <summary>
    /// ELFinder connector module
    /// </summary>
    public class ELFinderConnectorModule : ELFinderBaseConnectorModule
    {

        #region Constructors

        /// <summary>
        /// Create a new instance
        /// </summary>
        public ELFinderConnectorModule() : base(SharedConfig.ELFinder)
        {
        }

        #endregion

    }

}