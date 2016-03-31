using System;
using System.IO;
using ELFinder.Connector.Config;
using ELFinder.WebServer.Nancy.Bootstrap;
using ELFinder.WebServer.Nancy.Config;
using Nancy.Hosting.Self;

namespace ELFinder.WebServer.Nancy
{

    /// <summary>
    /// Program module
    /// </summary>
    class Program
    {

        /// <summary>
        /// Main entry pont
        /// </summary>
        /// <param name="args">Arguments</param>
        static void Main(string[] args)
        {

            // Init ELFInder configuration
            InitELFinderConfiguration();

            // Start web server
            StartWebServer();

        }

        /// <summary>
        /// Initialize ELFinder configuration
        /// </summary>
        static void InitELFinderConfiguration()
        {

            SharedConfig.ELFinder = new ELFinderConfig(
                Path.Combine(Environment.CurrentDirectory, @"Data\Thumbnails"),
                thumbnailsUrl: "Thumbnails/"                
                );

            SharedConfig.ELFinder.RootVolumes.Add(
                new ELFinderRootVolumeConfigEntry(
                    Path.Combine(Environment.CurrentDirectory, @"Data\Files"),
                    isLocked: false,
                    isReadOnly: false,
                    isShowOnly: false,
                    maxUploadSizeKb: 0,
                    uploadOverwrite: true,
                    startDirectory: ""));

        }

        /// <summary>
        /// Start web engine
        /// </summary>
        static void StartWebServer()
        {

            // Set host uri
            var hostUri = new Uri("http://localhost:3434/");

            // Start web application
            using (var host = new NancyHost(
                new ELFinderNancyBootstrapper(), hostUri))
            {

                // Start web application                
                host.Start();

                // Info
                Console.WriteLine(@"Listening on: " + hostUri);

                Console.WriteLine();
                Console.WriteLine(@"Press ENTER to exit...");
                Console.ReadLine();

            }

        }

    }
}
