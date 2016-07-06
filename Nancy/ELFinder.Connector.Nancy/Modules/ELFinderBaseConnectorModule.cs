using System;
using System.Linq;
using ELFinder.Connector.Commands.Operations.Add;
using ELFinder.Connector.Commands.Operations.Change;
using ELFinder.Connector.Commands.Operations.Common.Interfaces;
using ELFinder.Connector.Commands.Operations.Content;
using ELFinder.Connector.Commands.Operations.Image;
using ELFinder.Connector.Commands.Operations.Image.Thumbnails;
using ELFinder.Connector.Commands.Operations.Open;
using ELFinder.Connector.Commands.Operations.Remove;
using ELFinder.Connector.Commands.Operations.Replace;
using ELFinder.Connector.Commands.Operations.Search;
using ELFinder.Connector.Commands.Operations.Tree;
using ELFinder.Connector.Config.Interfaces;
using ELFinder.Connector.Drivers.FileSystem;
using ELFinder.Connector.Nancy.Config;
using ELFinder.Connector.Nancy.Responses.Data;
using ELFinder.Connector.Nancy.Responses.Files;
using ELFinder.Connector.Nancy.Streams;
using Nancy;
using Nancy.ModelBinding;

namespace ELFinder.Connector.Nancy.Modules
{
    /// <summary>
    /// ELFinder connector module
    /// </summary>
    public abstract class ELFinderBaseConnectorModule : NancyModule
    {

        #region Properties

        /// <summary>
        /// Driver
        /// </summary>
        protected FileSystemConnectorDriver Driver { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance using default configuration
        /// </summary>
        protected ELFinderBaseConnectorModule() : this(DefaultNancyConnectorConfig.Create())
        {
        }

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="config">ELFinder config</param>
        protected ELFinderBaseConnectorModule(IELFinderConfig config)
        {

            // Initialize driver
            Driver = new FileSystemConnectorDriver(config);

            // Init routes
            InitRoutes();

        }

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="config">ELFinder config</param>
        /// <param name="modulePath">Module path</param>
        protected ELFinderBaseConnectorModule(IELFinderConfig config, string modulePath) : base(modulePath)
        {

            // Initialize driver
            Driver = new FileSystemConnectorDriver(config);

            // Init routes
            InitRoutes();

        }

        #endregion

        #region Methods

        /// <summary>
        /// Init routes
        /// </summary>
        protected void InitRoutes()
        {

            // Register connector command routes

            // Init
            RegisterGETConnectorRoute<ConnectorInitCommand>(
                "open",
                ctx => ctx.Request.Query["cmd"] == "open" && ctx.Request.Query["init"] == "1");

            // Open
            RegisterGETConnectorRoute<ConnectorOpenCommand>(
                "open",
                ctx => ctx.Request.Query["cmd"] == "open" && ctx.Request.Query["init"] != "1");

            // Parents
            RegisterGETConnectorRoute<ConnectorParentsCommand>("parents");

            // Tree
            RegisterGETConnectorRoute<ConnectorTreeCommand>("tree");

            // MakeDir
            RegisterGETConnectorRoute<ConnectorMakeDirCommand>("mkdir");

            // MakeFile
            RegisterGETConnectorRoute<ConnectorMakeFileCommand>("mkfile");

            // Remove
            RegisterGETConnectorRoute<ConnectorRemoveCommand>("rm");

            // Get
            RegisterGETConnectorRoute<ConnectorGetCommand>("get");

            // Rename
            RegisterGETConnectorRoute<ConnectorRenameCommand>("rename");

            // Duplicate
            RegisterGETConnectorRoute<ConnectorDuplicateCommand>("duplicate");

            // Paste
            RegisterGETConnectorRoute<ConnectorPasteCommand>("paste");

            // File
            RegisterGETFileDownloadConnectorRoute<ConnectorFileCommand>("file");

            // Search
            RegisterGETConnectorRoute<ConnectorSearchCommand>("search");

            // Put
            RegisterPOSTConnectorRoute<ConnectorPutCommand>("put");

            // Upload
            RegisterPOSTUploadFilesConnectorRoute<ConnectorUploadCommand>("upload");

            // Resize image
            RegisterGETConnectorRoute<ConnectorResizeImageCommand>(
                "resize",
                ctx => ctx.Request.Query["cmd"] == "resize" && ctx.Request.Query["mode"] == "resize");

            // Rotate image
            RegisterGETConnectorRoute<ConnectorRotateImageCommand>(
                "resize",
                ctx => ctx.Request.Query["cmd"] == "resize" && ctx.Request.Query["mode"] == "rotate");

            // Crop image
            RegisterGETConnectorRoute<ConnectorCropImageCommand>(
                "resize",
                ctx => ctx.Request.Query["cmd"] == "resize" && ctx.Request.Query["mode"] == "crop");

            // Generate thumbnails
            RegisterGETConnectorRoute<ConnectorGenerateThumbnailsCommand>("tmb");

            // Thumbnails
            Get["/Thumbnails/{Target}"] = x =>
                ConnectorRouteHandler<ConnectorGetThumbnailCommand>(ExecuteGetThumbnailCommand);

        }

        /// <summary>
        /// Register HTTP GET ELFinder connector route
        /// </summary>
        /// <param name="commandName">Command name</param>
        protected virtual void RegisterGETConnectorRoute<TCommand>(string commandName)
            where TCommand : IConnectorCommand
        {

            // Invoke overload with default condition
            RegisterGETConnectorRoute<TCommand>(
                commandName,
                ctx => ctx.Request.Query["cmd"] == commandName);

        }

        /// <summary>
        /// Register HTTP GET ELFinder connector route
        /// </summary>
        /// <param name="commandName">Command name</param>
        /// <param name="condition">Route condition</param>
        protected virtual void RegisterGETConnectorRoute<TCommand>(string commandName, Func<NancyContext, bool> condition)
            where TCommand : IConnectorCommand
        {

            Get[
                "/ELFFinderConnector/", condition] =
                x => ConnectorRouteHandler<TCommand>(ExecuteCommand);

        }

        /// <summary>
        /// Register HTTP GET file download ELFinder connector route
        /// </summary>
        /// <param name="commandName">Command name</param>
        protected virtual void RegisterGETFileDownloadConnectorRoute<TCommand>(string commandName)
            where TCommand : ConnectorFileCommand
        {

            Get[
                "/ELFFinderConnector/",
                ctx => ctx.Request.Query["cmd"] == commandName] =
                    x => ConnectorRouteHandler<TCommand>(ExecuteFileDownloadCommand);

        }

        /// <summary>
        /// Register HTTP POST ELFinder connector route
        /// </summary>
        /// <typeparam name="TCommand">Command type</typeparam>
        /// <param name="commandName">Command name</param>
        protected virtual void RegisterPOSTConnectorRoute<TCommand>(string commandName)
            where TCommand : IConnectorCommand
        {

            Post[
                "/ELFFinderConnector/",
                ctx => ctx.Request.Form["cmd"] == commandName] =
                    x => ConnectorRouteHandler<TCommand>(ExecuteCommand);

        }

        /// <summary>
        /// Register HTTP POST file upload ELFinder connector route
        /// </summary>
        /// <param name="commandName">Command name</param>
        protected virtual void RegisterPOSTUploadFilesConnectorRoute<TCommand>(string commandName)
            where TCommand : ConnectorUploadCommand
        {

            Post[
                "/ELFFinderConnector/",
                ctx => ctx.Request.Form["cmd"] == commandName] =
                x => ConnectorRouteHandler<TCommand>(ExecuteCommand);

        }

        /// <summary>
        /// Connector route handler
        /// </summary>
        /// <typeparam name="TCommand">Command type</typeparam>
        /// <param name="executeCommandHandler">Connector command execution handler</param>
        /// <returns>Response result</returns>
        protected virtual dynamic ConnectorRouteHandler<TCommand>(Func<TCommand, dynamic> executeCommandHandler)
            where TCommand : IConnectorCommand
        {

            // Bind command model
            var command = this.Bind<TCommand>();

            // Set driver
            command.Driver = Driver;

            // Assign files streams
            command.Files = Context.Request.Files.Select(x => new HttpFileStream(x));

            // Execute it
            return executeCommandHandler.Invoke(command);

        }

        /// <summary>
        /// Execute command
        /// </summary>
        /// <typeparam name="TCommand">Command type</typeparam>
        /// <param name="command">Command</param>
        /// <returns>Result</returns>
        protected virtual dynamic ExecuteCommand<TCommand>(TCommand command)
            where TCommand : IConnectorCommand
        {

            // Execute it
            var resultData = command.Execute();

            // Return it as Json-encoded
            return new ELFinderJsonDataResponse(resultData);

        }

        /// <summary>
        /// Execute file download command
        /// </summary>
        /// <typeparam name="TCommand">Command type</typeparam>
        /// <param name="command">Command</param>
        /// <returns>Result</returns>
        protected virtual dynamic ExecuteFileDownloadCommand<TCommand>(TCommand command)
            where TCommand : ConnectorFileCommand
        {

            // Execute it
            var resultData = command.Execute();

            // Return it as file
            return new ELFinderFileDownloadResponse(resultData, Context);

        }

        /// <summary>
        /// Execute get thumbnail command
        /// </summary>
        /// <typeparam name="TCommand">Command type</typeparam>
        /// <param name="command">Command</param>
        /// <returns>Result</returns>
        protected virtual dynamic ExecuteGetThumbnailCommand<TCommand>(TCommand command)
            where TCommand : ConnectorGetThumbnailCommand
        {

            // Execute it
            var resultData = command.Execute();

            // Return it as thumbnail
            return new ELFinderGetThumbnailResponse(resultData, Context);

        }

        #endregion

    }
}