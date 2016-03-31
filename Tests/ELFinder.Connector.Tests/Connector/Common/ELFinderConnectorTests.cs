using ELFinder.Connector.Drivers.Common.Interfaces;

namespace ELFinder.Connector.Tests.Connector.Common
{

    /// <summary>
    /// ELFinder connector tests
    /// </summary>
    public abstract class ELFinderConnectorTests
    {

        #region Properties

        /// <summary>
        /// Driver
        /// </summary>
        private IConnectorDriver _driver;

        /// <summary>
        /// Driver
        /// </summary>
        protected IConnectorDriver Driver => _driver ?? (_driver = CreateDriver());

        #endregion

        #region Abstract methods

        /// <summary>
        /// Create driver instance
        /// </summary>
        /// <returns>Result driver</returns>
        protected abstract IConnectorDriver CreateDriver();

        #endregion

    }

}