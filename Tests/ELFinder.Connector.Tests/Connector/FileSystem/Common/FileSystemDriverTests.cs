using ELFinder.Connector.Commands.Results.Common;
using ELFinder.Connector.Drivers.Common.Interfaces;
using ELFinder.Connector.Drivers.FileSystem;
using ELFinder.Connector.Tests.Config;
using ELFinder.Connector.Tests.Connector.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ELFinder.Connector.Tests.Connector.FileSystem.Common
{

    /// <summary>
    /// ELFinder file system driver tests
    /// </summary>
    public abstract class FileSystemDriverTests : ELFinderConnectorTests
    {

        #region Overrides

        /// <summary>
        /// Create driver instance
        /// </summary>
        /// <returns>Result driver</returns>
        protected override IConnectorDriver CreateDriver()
        {
            return new FileSystemConnectorDriver(SharedTestsConfig.ELFinder);
        }

        #endregion

    }
}