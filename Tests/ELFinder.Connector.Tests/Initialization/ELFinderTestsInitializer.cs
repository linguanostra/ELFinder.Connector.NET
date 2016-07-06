using System;
using System.IO;
using ELFinder.Connector.Config;
using ELFinder.Connector.Tests.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ELFinder.Connector.Tests.Initialization
{

    /// <summary>
    /// ELFinder tests initializer
    /// </summary>
    [TestClass]
    public class ELFinderTestsInitializer
    {

        #region Tests initialization/cleanup methods

        /// <summary>
        /// Initialize tests
        /// </summary>
        /// <param name="context">Test context</param>
        [AssemblyInitialize]
        public static void InitializeTests(TestContext context)
        {

            // Init configuration
            InitConfiguration();

        }

        /// <summary>
        /// Cleanup tests
        /// </summary>
        [AssemblyCleanup]
        public static void CleanupTests()
        {

        }

        #endregion

        #region Static methods

        /// <summary>
        /// Init configuration
        /// </summary>
        static void InitConfiguration()
        {


            SharedTestsConfig.ELFinder = new ELFinderConfig(
                Path.Combine(Environment.CurrentDirectory, @"Data\Thumbnails"),
                thumbnailsUrl: "Thumbnails/"
                );

            SharedTestsConfig.ELFinder.RootVolumes.Add(
                new ELFinderRootVolumeConfigEntry(
                    Path.Combine(Environment.CurrentDirectory, @"Data\Files"),
                    isLocked: false,
                    isReadOnly: false,
                    isShowOnly: false,
                    maxUploadSizeKb: null,      // null = Unlimited upload size
                    uploadOverwrite: true,
                    startDirectory: ""));

        }

        #endregion

    }
}