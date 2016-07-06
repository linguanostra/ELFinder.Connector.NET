using System;
using System.Linq;
using System.Web.Mvc;
using ELFinder.Connector.ASPNet.ActionResults.Data;
using ELFinder.Connector.ASPNet.ActionResults.Files;
using ELFinder.Connector.ASPNet.Config;
using ELFinder.Connector.ASPNet.Streams;
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

namespace ELFinder.Connector.ASPNet.Controllers
{

    /// <summary>
    /// ELFinder connector controller
    /// </summary>
    public abstract class ELFinderBaseConnectorController : Controller
    {

        #region Properties

        /// <summary>
        /// Driver
        /// </summary>
        protected FileSystemConnectorDriver Driver { get; set; }

        #endregion

        #region Constructors


        /// <summary>
        /// Create a new instance using default config
        /// </summary>
        protected ELFinderBaseConnectorController() : this(DefaultASPNetConnectorConfig.Create())
        {
        }

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="config">ELFinder config</param>
        protected ELFinderBaseConnectorController(IELFinderConfig config)
        {

            // Initialize driver using config
            Driver = new FileSystemConnectorDriver(config);

        }

        #endregion

        #region Methods

        #region Public

        /// <summary>
        /// Main handler
        /// </summary>
        /// <param name="cmd">Command</param>
        /// <returns>Result</returns>
        public virtual ActionResult Main(string cmd)
        {

            // Dispatch command

            // Init
            if (cmd == "open" && Request.QueryString["init"] == "1")
            {
                return DispatchCommand<ConnectorInitCommand>();
            }

            // Open
            if (cmd == "open" && Request.QueryString["init"] != "1")
            {
                return DispatchCommand<ConnectorOpenCommand>();
            }

            // Parents
            if (cmd == "parents")
            {
                return DispatchCommand<ConnectorParentsCommand>();
            }

            // Tree
            if (cmd == "tree")
            {
                return DispatchCommand<ConnectorTreeCommand>();
            }

            // MakeDir
            if (cmd == "mkdir")
            {
                return DispatchCommand<ConnectorMakeDirCommand>();
            }

            // MakeFile
            if (cmd == "mkfile")
            {
                return DispatchCommand<ConnectorMakeFileCommand>();
            }

            // Remove
            if (cmd == "rm")
            {
                return DispatchCommand<ConnectorRemoveCommand>();
            }

            // Get
            if (cmd == "get")
            {
                return DispatchCommand<ConnectorGetCommand>();
            }

            // Rename
            if (cmd == "rename")
            {
                return DispatchCommand<ConnectorRenameCommand>();
            }

            // Duplicate
            if (cmd == "duplicate")
            {
                return DispatchCommand<ConnectorDuplicateCommand>();
            }

            // Paste
            if (cmd == "paste")
            {
                return DispatchCommand<ConnectorPasteCommand>();
            }

            // File
            if (cmd == "file")
            {
                return DispatchCommand<ConnectorFileCommand>(ExecuteFileDownloadCommand);
            }

            // Search
            if (cmd == "search")
            {
                return DispatchCommand<ConnectorSearchCommand>();
            }

            // Put
            if (cmd == "put")
            {
                return DispatchCommand<ConnectorPutCommand>();
            }

            // Upload
            if (cmd == "upload")
            {
                return DispatchCommand<ConnectorUploadCommand>();
            }

            // Resize image
            if (cmd == "resize" && Request.QueryString["mode"] == "resize")
            {
                return DispatchCommand<ConnectorResizeImageCommand>();
            }

            // Rotate image
            if (cmd == "resize" && Request.QueryString["mode"] == "rotate")
            {
                return DispatchCommand<ConnectorRotateImageCommand>();
            }

            // Crop image
            if (cmd == "resize" && Request.QueryString["mode"] == "crop")
            {
                return DispatchCommand<ConnectorCropImageCommand>();
            }

            // Generate thumbnails
            if (cmd == "tmb")
            {
                return DispatchCommand<ConnectorGenerateThumbnailsCommand>();
            }

            // Command not found
            return new HttpNotFoundResult();

        }

        /// <summary>
        /// Thumbnails handler
        /// </summary>
        /// <param name="target">Target</param>
        /// <returns>Result</returns>
        public virtual ActionResult Thumbnails(string target)
        {

            // Thumbnails
            return DispatchCommand<ConnectorGetThumbnailCommand>(ExecuteGetThumbnailCommand);

        }

        #endregion

        #region Protected

        /// <summary>
        /// Dispatch command
        /// </summary>
        /// <typeparam name="TCommand">Command type</typeparam>
        /// <returns>Result</returns>
        protected virtual ActionResult DispatchCommand<TCommand>()
            where TCommand : class, IConnectorCommand, new()
        {

            // Use default command execution handler
            return DispatchCommand<TCommand>(ExecuteCommand);

        }

        /// <summary>
        /// Dispatch command
        /// </summary>
        /// <typeparam name="TCommand">Command type</typeparam>
        /// <param name="executeCommandHandler">Execute command handler</param>
        /// <returns>Result</returns>
        protected virtual ActionResult DispatchCommand<TCommand>(Func<TCommand, ActionResult> executeCommandHandler)
            where TCommand : class, IConnectorCommand, new()
        {

            // Create a command instance
            var command = new TCommand();

            // Bind it (Don't care about validation exceptions)
            TryUpdateModel(command);

            // Set driver
            command.Driver = Driver;

            // Assign files
            command.Files = Request.Files.AllKeys.Select(x => new HttpFileStream(Request.Files.Get(x)));

            // Execute it
            return executeCommandHandler(command);

        }

        /// <summary>
        /// Execute command
        /// </summary>
        /// <typeparam name="TCommand">Command type</typeparam>
        /// <param name="command">Command</param>
        /// <returns>Result</returns>
        protected virtual ActionResult ExecuteCommand<TCommand>(TCommand command)
            where TCommand : class, IConnectorCommand
        {

            // Execute it
            var resultData = command.Execute();

            // Return it as Json-encoded
            return new ELFinderJsonDataResult(resultData);

        }

        /// <summary>
        /// Execute file download command
        /// </summary>
        /// <typeparam name="TCommand">Command type</typeparam>
        /// <param name="command">Command</param>
        /// <returns>Result</returns>
        protected virtual ActionResult ExecuteFileDownloadCommand<TCommand>(TCommand command)
            where TCommand : ConnectorFileCommand
        {

            // Execute it
            var resultData = command.Execute();

            // Return it as file
            return new ELFinderFileDownloadResult(resultData);

        }

        /// <summary>
        /// Execute get thumbnail command
        /// </summary>
        /// <typeparam name="TCommand">Command type</typeparam>
        /// <param name="command">Command</param>
        /// <returns>Result</returns>
        protected virtual ActionResult ExecuteGetThumbnailCommand<TCommand>(TCommand command)
            where TCommand : ConnectorGetThumbnailCommand
        {

            // Execute it
            var resultData = command.Execute();

            // Return it as thumbnail
            return new ELFinderGetThumbnailResult(resultData);

        }

        #endregion

        #endregion

    }
}