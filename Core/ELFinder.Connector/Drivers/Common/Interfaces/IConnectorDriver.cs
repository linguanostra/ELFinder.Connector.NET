using System.Collections.Generic;
using ELFinder.Connector.Commands.Results.Add;
using ELFinder.Connector.Commands.Results.Change;
using ELFinder.Connector.Commands.Results.Content;
using ELFinder.Connector.Commands.Results.Image;
using ELFinder.Connector.Commands.Results.Image.Thumbnails;
using ELFinder.Connector.Commands.Results.Open;
using ELFinder.Connector.Commands.Results.Remove;
using ELFinder.Connector.Commands.Results.Replace;
using ELFinder.Connector.Commands.Results.Search;
using ELFinder.Connector.Commands.Results.Tree;
using ELFinder.Connector.Streams;

namespace ELFinder.Connector.Drivers.Common.Interfaces
{

    /// <summary>
    /// ELFinder connector driver interface
    /// </summary>
    public interface IConnectorDriver
    {

        #region Methods        

        /// <summary>
        /// Open file
        /// </summary>
        /// <param name="target">Target</param>
        /// <param name="includeRootSubFolders">Include subfolders of root directories</param>
        /// <returns>Response result</returns>
        OpenCommandResult Open(string target, bool includeRootSubFolders);

        /// <summary>
        /// Initialize file/directory open
        /// </summary>
        /// <param name="target">Target</param>
        /// <returns>Response result</returns>
        InitCommandResult Init(string target);

        /// <summary>
        /// Get parents
        /// </summary>
        /// <param name="target">Target</param>
        /// <returns>Response results</returns>
        ParentsCommandResult Parents(string target);

        /// <summary>
        /// Get tree
        /// </summary>
        /// <param name="target">Target</param>
        /// <returns>Response results</returns>
        TreeCommandResult Tree(string target);

        /// <summary>
        /// Make directory
        /// </summary>
        /// <param name="target">Target</param>
        /// <param name="name">Name</param>
        /// <returns>Response results</returns>
        MakeDirCommandResult MakeDir(string target, string name);

        /// <summary>
        /// Remove items
        /// </summary>
        /// <param name="targets">Targets</param>
        /// <returns>Response results</returns>
        RemoveCommandResult Remove(List<string> targets);

        /// <summary>
        /// Make file
        /// </summary>
        /// <param name="target">Target</param>
        /// <param name="name">Name</param>
        /// <returns>Response results</returns>
        MakeFileCommandResult MakeFile(string target, string name);

        /// <summary>
        /// Get content
        /// </summary>
        /// <param name="target">Target</param>
        /// <returns>Response result</returns>
        GetCommandResult Get(string target);

        /// <summary>
        /// Rename item
        /// </summary>
        /// <param name="target">Target</param>
        /// <param name="name">Name</param>
        /// <returns>Response result</returns>
        RenameCommandResult Rename(string target, string name);

        /// <summary>
        /// Duplicate items
        /// </summary>
        /// <param name="targets">Targets</param>
        /// <returns>Response result</returns>
        DuplicateCommandResult Duplicate(IEnumerable<string> targets);

        /// <summary>
        /// Put file content
        /// </summary>
        /// <param name="target">Target</param>
        /// <param name="content">Content</param>
        /// <returns>Response result</returns>
        PutCommandResult Put(string target, string content);

        /// <summary>
        /// Get file content
        /// </summary>
        /// <param name="target">Target</param>
        /// <param name="isDownload">Download file</param>
        /// <returns>Response result</returns>
        FileCommandResult File(string target, bool isDownload);

        /// <summary>
        /// Paste items
        /// </summary>
        /// <param name="source">Source</param>
        /// <param name="dest">Destination</param>
        /// <param name="targets">Targets</param>
        /// <param name="isCut">Is cut</param>
        /// <returns>Response result</returns>
        PasteCommandResult Paste(string source, string dest, IEnumerable<string> targets, bool isCut);

        /// <summary>
        /// Search items
        /// </summary>
        /// <param name="query">Query</param>
        /// <returns>Response result</returns>
        SearchCommandResult Search(string query);

        /// <summary>
        /// Upload files
        /// </summary>
        /// <param name="target">Target</param>
        /// <param name="files">Files</param>
        /// <returns>Response result</returns>
        UploadCommandResult Upload(string target, IEnumerable<IFileStream> files);

        /// <summary>
        /// Rotate image
        /// </summary>
        /// <param name="target">Target</param>
        /// <param name="angle">Angle</param>
        /// <returns>Response result</returns>
        RotateImageResult Rotate(string target, int angle);

        /// <summary>
        /// Resize image
        /// </summary>
        /// <param name="target">Target</param>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
        /// <returns>Response result</returns>
        ResizeImageResult Resize(string target, int width, int height);

        /// <summary>
        /// Crop image
        /// </summary>
        /// <param name="target">Target</param>
        /// <param name="x">Start X</param>
        /// <param name="y">Start Y</param>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
        /// <returns>Response result</returns>
        CropImageResult Crop(string target, int x, int y, int width, int height);

        /// <summary>
        /// Generate thumbnails
        /// </summary>
        /// <param name="targets">Targets</param>
        /// <returns>Response result</returns>
        GenerateThumbnailsResult GenerateThumbnails(IEnumerable<string> targets);

        /// <summary>
        /// Get thumbnail
        /// </summary>
        /// <param name="target">Target</param>
        /// <returns>Result</returns>
        GetThumbnailResult GetThumbnail(string target);

        //dynamic List(string target);
        //dynamic Dim(string target);

        #endregion
    }
}