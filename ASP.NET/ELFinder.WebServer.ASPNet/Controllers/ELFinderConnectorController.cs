using ELFinder.Connector.ASPNet.Controllers;
using ELFinder.WebServer.ASPNet.Config;

namespace ELFinder.WebServer.ASPNet.Controllers
{

    /// <summary>
    /// ELFinder connector controller
    /// </summary>
    public class ELFinderConnectorController : ELFinderBaseConnectorController
    {

        #region Constructors

        /// <summary>
        /// Create a new instance
        /// </summary>
        public ELFinderConnectorController() : base(SharedConfig.ELFinder)
        {
        }

        #endregion

    }
}