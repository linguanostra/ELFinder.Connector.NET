using System.Linq;
using ELFinder.Connector.Commands.Extensions;
using ELFinder.Connector.Tests.Connector.FileSystem.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ELFinder.Connector.Tests.Connector.FileSystem.Commands.Operations
{

    /// <summary>
    /// Init command tests
    /// </summary>
    [TestClass]
    public class InitCommandTests : FileSystemDriverTests
    {

        #region Test methods

        /// <summary>
        /// Test init command
        /// </summary>
        [TestMethod]
        public void TestInit()
        {

            // Initialize without target
            var initResult = Driver.Init(null);

            // Validations
            Assert.IsNotNull(initResult);
            Assert.IsFalse(initResult.HasError());

        }

        /// <summary>
        /// Test open command
        /// </summary>
        [TestMethod]
        public void TestOpen()
        {

            // Initialize without target
            var initResult = Driver.Init(null);

            // Open
            var openResult = Driver.Open(initResult.CurrentWorkingDirectory.Hash, true);

            // Validations
            Assert.IsNotNull(openResult);
            Assert.IsTrue(openResult.Files.Any());

        }

        #endregion

    }
}