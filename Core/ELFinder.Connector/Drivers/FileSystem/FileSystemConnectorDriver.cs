using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using ELFinder.Connector.Commands.Results.Add;
using ELFinder.Connector.Commands.Results.Change;
using ELFinder.Connector.Commands.Results.Common.Data;
using ELFinder.Connector.Commands.Results.Content;
using ELFinder.Connector.Commands.Results.Image;
using ELFinder.Connector.Commands.Results.Image.Thumbnails;
using ELFinder.Connector.Commands.Results.Open;
using ELFinder.Connector.Commands.Results.Remove;
using ELFinder.Connector.Commands.Results.Replace;
using ELFinder.Connector.Commands.Results.Search;
using ELFinder.Connector.Commands.Results.Tree;
using ELFinder.Connector.Config;
using ELFinder.Connector.Config.Interfaces;
using ELFinder.Connector.Drivers.Common.Data.Extensions;
using ELFinder.Connector.Drivers.Common.Interfaces;
using ELFinder.Connector.Drivers.FileSystem.Constants;
using ELFinder.Connector.Drivers.FileSystem.Extensions;
using ELFinder.Connector.Drivers.FileSystem.Models.Directories;
using ELFinder.Connector.Drivers.FileSystem.Models.Files;
using ELFinder.Connector.Drivers.FileSystem.Utils;
using ELFinder.Connector.Drivers.FileSystem.Volumes;
using ELFinder.Connector.Drivers.FileSystem.Volumes.Info;
using ELFinder.Connector.Drivers.FileSystem.Volumes.Info.Extensions;
using ELFinder.Connector.Exceptions;
using ELFinder.Connector.Extensions;
using ELFinder.Connector.ImageProcessor;
using ELFinder.Connector.ImageProcessor.Imaging;
using ELFinder.Connector.Streams;
using ELFinder.Connector.Utils;

namespace ELFinder.Connector.Drivers.FileSystem
{

    /// <summary>
    /// ELFinder file system connector driver
    /// </summary>
    public class FileSystemConnectorDriver : IConnectorDriver
    {

        #region Constants

        /// <summary>
        /// Volume prefix
        /// </summary>
        private const string VolumePrefix = "v";

        #endregion

        #region Properties

        /// <summary>
        /// File system root volumes
        /// </summary>
        public List<FileSystemRootVolume> RootVolumes { get; } 

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="config">Configuration</param>
        public FileSystemConnectorDriver(IELFinderConfig config)
        {

            // Init root volumes
            RootVolumes = new List<FileSystemRootVolume>();

            // Parse configuration
            ParseConfig(config);

        }

        #endregion

        #region Methods

        #region IConnectorDriver members

        /// <summary>
        /// Open file
        /// </summary>
        /// <param name="target">Target</param>
        /// <param name="includeRootSubFolders">Include subfolders of root directories</param>
        /// <returns>Response result</returns>
        OpenCommandResult IConnectorDriver.Open(string target, bool includeRootSubFolders)
        {

            // Get directory target path info
            var fullPath = ParseDirectoryPath(target);

            // Create command result
            var commandResult = new OpenCommandResult(
                fullPath.CreateTargetEntryObjectModel(),
                new OptionsResultData(fullPath));

            // Add visible files
            foreach (var fileItem in fullPath.Directory.GetVisibleFiles())
            {

                // Check if file is supported for imaging
                if (ImagingUtils.CanProcessFile(fileItem))
                {

                    // Is supported
                    // Add file item as image entry
                    commandResult.Files.Add(
                        ImageEntryObjectModel.Create(fileItem, fullPath.Root));

                }
                else
                {

                    // Not supported
                    // Add file item as standard entry
                    commandResult.Files.Add(
                        FileEntryObjectModel.Create(fileItem, fullPath.Root));

                }

            }

            // Add visible directories
            foreach (var directoryItem in fullPath.Directory.GetVisibleDirectories())
            {
                commandResult.Files.Add(DirectoryEntryObjectModel.Create(directoryItem, fullPath.Root));
            }

            // Return it
            return commandResult;

        }

        /// <summary>
        /// Initialize file/directory open
        /// </summary>
        /// <param name="target">Target</param>
        /// <returns>Response result</returns>
        InitCommandResult IConnectorDriver.Init(string target)
        {

            // Declare target directory path info
            FileSystemVolumeDirectoryInfo fullPath;

            // Check if target was defined
            if (string.IsNullOrEmpty(target))
            {

                // Target not defined

                // Get startup volume
                var startupVolume = RootVolumes.GetStartupVolume();

                // Get directory target path info
                fullPath = new FileSystemVolumeDirectoryInfo(
                    startupVolume, startupVolume.StartDirectory ?? startupVolume.Directory);

            }
            else
            {

                // Target defined
                
                // Get directory target path info
                fullPath = ParseDirectoryPath(target);

            }

            // Create command result
            var commandResult = new InitCommandResult(
                fullPath.CreateTargetEntryObjectModel(),
                new OptionsResultData(fullPath));

            // Add visible files
            foreach (var fileItem in fullPath.Directory.GetVisibleFiles())
            {
                
                // Check if file is supported for imaging
                if (ImagingUtils.CanProcessFile(fileItem))
                {

                    // Is supported
                    // Add file item as image entry
                    commandResult.Files.Add(
                        ImageEntryObjectModel.Create(fileItem, fullPath.Root));

                }
                else
                {
                    
                    // Not supported
                    // Add file item as standard entry
                    commandResult.Files.Add(
                        FileEntryObjectModel.Create(fileItem, fullPath.Root));

                }                

            }

            // Add visible directories
            foreach (var directoryItem in fullPath.Directory.GetVisibleDirectories())
            {
                commandResult.Files.Add(DirectoryEntryObjectModel.Create(directoryItem, fullPath.Root));
            }

            // Add root directories
            foreach (var rootVolume in RootVolumes)
            {
                commandResult.Files.Add(RootDirectoryEntryObjectModel.Create(rootVolume.Directory, rootVolume));
            }

            // Add root directories, if different from root
            if (!fullPath.IsDirectorySameAsRoot)
            {
                foreach (var directoryItem in fullPath.Root.Directory.GetVisibleDirectories())
                {
                    commandResult.Files.Add(DirectoryEntryObjectModel.Create(directoryItem, fullPath.Root));
                }
            }

            // Set max upload size
            if (fullPath.Root.MaxUploadSizeKb.HasValue)
            {
                commandResult.UploadMaxSize = $"{fullPath.Root.MaxUploadSizeKb.Value}K";
            }

            // Return it
            return commandResult;

        }

        /// <summary>
        /// Get parents
        /// </summary>
        /// <param name="target">Target</param>
        /// <returns>Response results</returns>
        ParentsCommandResult IConnectorDriver.Parents(string target)
        {

            // Create command result
            var commandResult = new ParentsCommandResult();

            // Parse target path
            var fullPath = ParseDirectoryPath(target);

            // Add directories
            if (fullPath.IsDirectorySameAsRoot)
            {

                // Root level
                commandResult.Tree.Add(
                    DirectoryEntryObjectModel.Create(
                        fullPath.Directory,
                        fullPath.Root));

            }
            else
            {

                // Not root level

                // Check that directory has a parent
                if (fullPath.Directory.Parent != null)
                {

                    // Add directories in the parent
                    foreach (var directoryItem in fullPath.Directory.Parent.GetDirectories())
                    {

                        commandResult.Tree.Add(
                            DirectoryEntryObjectModel.Create(
                                directoryItem, fullPath.Root));

                    }

                    // Go back to root
                    var parent = fullPath.Directory;

                    while (parent != null && parent.FullName != fullPath.Root.Directory.FullName)
                    {

                        // Update parent
                        parent = parent.Parent;

                        // Ensure it's a child of the root
                        if (parent != null
                            && parent.FullName.Length > fullPath.Root.Directory.FullName.Length)
                        {

                            commandResult.Tree.Add(
                                DirectoryEntryObjectModel.Create(
                                    parent, fullPath.Root));

                        }

                    }

                }

            }

            // Return it
            return commandResult;

        }

        /// <summary>
        /// Get tree
        /// </summary>
        /// <param name="target">Target</param>
        /// <returns>Response results</returns>
        TreeCommandResult IConnectorDriver.Tree(string target)
        {

            // Create command result
            var commandResult = new TreeCommandResult();

            // Parse target path
            var fullPath = ParseDirectoryPath(target);

            // Add visible directories
            foreach (var directoryInfo in fullPath.Directory.GetVisibleDirectories())
            {
                
                commandResult.Tree.Add(
                    DirectoryEntryObjectModel.Create(directoryInfo, fullPath.Root));

            }            

            // Return it
            return commandResult;

        }

        /// <summary>
        /// Make directory
        /// </summary>
        /// <param name="target">Target</param>
        /// <param name="name">Name</param>
        /// <returns>Response results</returns>
        MakeDirCommandResult IConnectorDriver.MakeDir(string target, string name)
        {

            // Create command result
            var commandResult = new MakeDirCommandResult();

            // Parse target path
            var fullPath = ParseDirectoryPath(target);

            // Create directory
            var newDirectory = Directory.CreateDirectory(Path.Combine(fullPath.Directory.FullName, name));

            // Add new directory to response
            commandResult.Added.Add(DirectoryEntryObjectModel.Create(newDirectory, fullPath.Root));

            // Return result
            return commandResult;

        }

        /// <summary>
        /// Remove items
        /// </summary>
        /// <param name="targets">Targets</param>
        /// <returns>Response results</returns>
        RemoveCommandResult IConnectorDriver.Remove(List<string> targets)
        {

            // Create command result
            var commandResult = new RemoveCommandResult();

            // Ensure targets are defined
            if (targets != null)
            {

                // Loop targets
                foreach (var targetItem in targets)
                {

                    // Parse target path
                    var fullPath = ParsePath(targetItem);

                    // Proceed if it's a file or directory
                    if (fullPath.IsDirectory())
                    {

                        // Directory                    

                        // Remove thumbnails
                        fullPath.RemoveThumbs();

                        // Remove directory
                        Directory.Delete(fullPath.Info.FullName, true);

                    }
                    else if (fullPath.IsFile())
                    {

                        // File

                        // Remove thumbnails
                        fullPath.RemoveThumbs();

                        // Remove file
                        File.Delete(fullPath.Info.FullName);

                    }
                    else
                    {

                        // Not supported
                        throw new NotSupportedException();

                    }

                    // Add it to removed entries list
                    commandResult.Removed.Add(targetItem);

                }

            }

            // Return result
            return commandResult;

        }

        /// <summary>
        /// Make file
        /// </summary>
        /// <param name="target">Target</param>
        /// <param name="name">Name</param>
        /// <returns>Response results</returns>
        MakeFileCommandResult IConnectorDriver.MakeFile(string target, string name)
        {

            // Create command result
            var commandResult = new MakeFileCommandResult();

            // Parse target path
            var fullPath = ParseDirectoryPath(target);

            // Create file
            var newFileInfo = new FileInfo(Path.Combine(fullPath.Info.FullName, name));
            newFileInfo.Create().Close();

            // Add new file to response
            commandResult.Added.Add(FileEntryObjectModel.Create(newFileInfo, fullPath.Root));

            // Return result
            return commandResult;

        }

        /// <summary>
        /// Get content
        /// </summary>
        /// <param name="target">Target</param>
        /// <returns>Response result</returns>
        GetCommandResult IConnectorDriver.Get(string target)
        {

            // Create command result
            var commandResult = new GetCommandResult();

            // Parse target path
            var fullPath = ParseFilePath(target);

            // Get content
            using (var reader = new StreamReader(fullPath.File.OpenRead()))
            {
                commandResult.Content = reader.ReadToEnd();
            }

            // Return result
            return commandResult;

        }

        /// <summary>
        /// Rename item
        /// </summary>
        /// <param name="target">Target</param>
        /// <param name="name">Name</param>
        /// <returns>Response result</returns>
        RenameCommandResult IConnectorDriver.Rename(string target, string name)
        {

            // Create command result
            var commandResult = new RenameCommandResult();

            // Parse target path
            var fullPath = ParsePath(target);

            // Add item to removed entries list
            commandResult.Removed.Add(target);

            // Proceed if it's a file or directory
            if (fullPath.IsDirectory())
            {
                // Directory                    

                // Remove thumbnails
                fullPath.RemoveThumbs();

                // Get parent path info
                var directoryInfo = fullPath.GetParentDirectory();
                if (directoryInfo != null)
                {

                    // Get new path
                    var newPath = Path.Combine(directoryInfo.FullName, name);

                    // Move path
                    Directory.Move(((FileSystemVolumeDirectoryInfo)fullPath).Directory.FullName, newPath);

                    // Add it to added entries list
                    commandResult.Added.Add(
                        DirectoryEntryObjectModel.Create(new DirectoryInfo(newPath), fullPath.Root));

                }

            }
            else if (fullPath.IsFile())
            {

                // File

                // Remove thumbnails
                fullPath.RemoveThumbs();

                // Get new path
                var newPath = Path.Combine(((FileSystemVolumeFileInfo)fullPath).File.DirectoryName??string.Empty, name);

                // Move file
                File.Move(((FileSystemVolumeFileInfo)fullPath).File.FullName, newPath);

                // Add it to added entries list
                commandResult.Added.Add(
                    FileEntryObjectModel.Create(new FileInfo(newPath), fullPath.Root));

            }
            else
            {

                // Not supported
                throw new NotSupportedException();

            }

            // Return result
            return commandResult;

        }

        /// <summary>
        /// Duplicate items
        /// </summary>
        /// <param name="targets">Targets</param>
        /// <returns>Response result</returns>
        DuplicateCommandResult IConnectorDriver.Duplicate(IEnumerable<string> targets)
        {

            // Create command result
            var commandResult = new DuplicateCommandResult();

            // Loop targets
            foreach (var targetItem in targets)
            {

                // Parse target path
                var fullPath = ParsePath(targetItem);

                // Proceed if it's a file or directory
                if (fullPath.IsDirectory())
                {

                    // Directory

                    // Remove thumbnails
                    fullPath.RemoveThumbs();

                    // Get path info
                    var parentPath = fullPath.GetParentDirectory().FullName;
                    var name = fullPath.GetDirectory().Name;

                    // Compute new name
                    var newName = $@"{parentPath}\{name} copy";

                    // Check if directory already exists
                    if (!Directory.Exists(newName))
                    {

                        // Doesn't exist
                        FileSystemUtils.DirectoryCopy(fullPath.GetDirectory(), newName, true);

                    }
                    else
                    {

                        // Already exists, create numbered copy
                        var newNameFound = false;
                        for (var i = 1; i < 100; i++)
                        {

                            // Compute new name
                            newName = $@"{parentPath}\{name} copy {i}";

                            // Test that it doesn't exist
                            if (!Directory.Exists(newName))
                            {
                                FileSystemUtils.DirectoryCopy(fullPath.GetDirectory(), newName, true);
                                newNameFound = true;
                                break;
                            }

                        }

                        // Check if new name was found
                        if (!newNameFound) throw new ELFinderNewNameSelectionException($@"{parentPath}\{name} copy");

                    }

                    // Add entry to added items
                    commandResult.Added.Add(
                        DirectoryEntryObjectModel.Create(new DirectoryInfo(newName), fullPath.Root));

                }
                else if (fullPath.IsFile())
                {

                    // File

                    // Remove thumbnails
                    fullPath.RemoveThumbs();

                    // Get path info
                    var parentPath = fullPath.GetDirectory().FullName;
                    var name = fullPath.Info.Name.Substring(0, fullPath.Info.Name.Length - fullPath.Info.Extension.Length);
                    var ext = fullPath.Info.Extension;

                    // Compute new name
                    var newName = $@"{parentPath}\{name} copy{ext}";

                    // Check if file already exists
                    if (!File.Exists(newName))
                    {

                        // Doesn't exist
                        ((FileSystemVolumeFileInfo)fullPath).File.CopyTo(newName);

                    }
                    else
                    {

                        // Already exists, create numbered copy
                        var newNameFound = false;
                        for (var i = 1; i < 100; i++)
                        {

                            // Compute new name
                            newName = $@"{parentPath}\{name} copy {i}{ext}";

                            // Test that it doesn't exist
                            if (!File.Exists(newName))
                            {
                                ((FileSystemVolumeFileInfo)fullPath).File.CopyTo(newName);
                                newNameFound = true;
                                break;
                            }
                        }

                        // Check if new name was found
                        if (!newNameFound) throw new ELFinderNewNameSelectionException($@"{parentPath}\{name} copy{ext}");

                    }

                    // Check if file refers to an image or not
                    if (ImagingUtils.CanProcessFile(newName))
                    {

                        // Add image to added entries
                        commandResult.Added.Add(
                            ImageEntryObjectModel.Create(new FileInfo(newName), fullPath.Root));

                    }
                    else
                    {

                        // Add file to added entries
                        commandResult.Added.Add(
                            FileEntryObjectModel.Create(new FileInfo(newName), fullPath.Root));

                    }

                }
                else
                {

                    // Not supported
                    throw new NotSupportedException();

                }

            }
            
            // Return result
            return commandResult;

        }

        /// <summary>
        /// Put file content
        /// </summary>
        /// <param name="target">Target</param>
        /// <param name="content">Content</param>
        /// <returns>Response result</returns>
        PutCommandResult IConnectorDriver.Put(string target, string content)
        {

            // Create command result
            var commandResult = new PutCommandResult();

            // Parse target file path
            var fullPath = ParseFilePath(target);

            // Write content
            using (var writer = new StreamWriter(fullPath.File.FullName, false))
            {
                writer.Write(content);
            }

            // Add entry to changed items list
            commandResult.Changed.Add(
                FileEntryObjectModel.Create(fullPath.File, fullPath.Root));

            // Return result
            return commandResult;

        }

        /// <summary>
        /// Get file content
        /// </summary>
        /// <param name="target">Target</param>
        /// <param name="isDownload">Download file</param>
        /// <returns>Response result</returns>
        FileCommandResult IConnectorDriver.File(string target, bool isDownload)
        {

            // Parse path
            var fullPath = ParsePath(target);

            // Create command result
            var commandResult = new FileCommandResult(
                fullPath.Info.FullName,
                MimeTypes.GetMimeType(fullPath.Info.Name),
                isDownload
                );

            // Check if path is directory
            if (fullPath.IsDirectory())
            {
                
                // Cannot download directory, must be a file
                commandResult.SetObjectNotFileError();

                // End processing
                return commandResult;

            }
            
            // Check if path exists
            if (!fullPath.Info.Exists)
            {
                
                // Doesn't exist
                commandResult.SetFileNotFoundError();

                // End processing
                return commandResult;

            }

            // Check if access is allowed
            if (fullPath.Root.IsShowOnly)
            {

                // Access denied, volume is for show only
                commandResult.SetAccessDeniedError();

                // End processing
                return commandResult;

            }

            // Return result
            return commandResult;

        }

        /// <summary>
        /// Paste items
        /// </summary>
        /// <param name="source">Source</param>
        /// <param name="dest">Destination</param>
        /// <param name="targets">Targets</param>
        /// <param name="isCut">Is cut</param>
        /// <returns>Response result</returns>
        PasteCommandResult IConnectorDriver.Paste(string source, string dest, IEnumerable<string> targets, bool isCut)
        {
            
            // Parse destination path
            var destPath = ParsePath(dest);
            
            // Create command result
            var commandResult = new PasteCommandResult();
            
            // Loop target items
            foreach (var item in targets)
            {

                // Parse target item path, which will be the source
                var src = ParsePath(item);

                // Proceed if it's a file or directory
                if (src.IsDirectory())
                {

                    // Directory

                    // Compute new directory
                    var newDir = new DirectoryInfo(Path.Combine(destPath.Info.FullName, src.Info.Name));

                    // Check if it already exists
                    if (newDir.Exists)
                    {

                        // Exists
                        Directory.Delete(newDir.FullName, true);

                    }

                    // Check if content is being cut
                    if (isCut)
                    {

                        // Remove thumbnails
                        src.RemoveThumbs();

                        // Move directory
                        src.GetDirectory().MoveTo(newDir.FullName);

                        // Add to removed entries
                        commandResult.Removed.Add(item);

                    }
                    else
                    {

                        // Copy directory
                        FileSystemUtils.DirectoryCopy(src.GetDirectory(), newDir.FullName, true);

                    }

                    // Add it to added items
                    commandResult.Added.Add(
                        DirectoryEntryObjectModel.Create(newDir, destPath.Root));

                }
                else if (src.IsFile())
                {

                    // File

                    // Compute new file path
                    var newFilePath = Path.Combine(destPath.Info.FullName, src.Info.Name);

                    // Check if file exists                
                    if (File.Exists(newFilePath))
                    {

                        // Exists
                        File.Delete(newFilePath);

                    }

                    // Check if content is being cut
                    if (isCut)
                    {

                        // Remove thumbnails
                        src.RemoveThumbs();

                        // Move file
                        File.Move(src.Info.FullName, newFilePath);

                        // Add it to removed items
                        commandResult.Removed.Add(item);
                        
                    }
                    else
                    {

                        // Copy file
                        File.Copy(src.Info.FullName, newFilePath);
                    }

                    // Check if file refers to an image or not
                    if (ImagingUtils.CanProcessFile(newFilePath))
                    {

                        // Add image to added entries
                        commandResult.Added.Add(
                            ImageEntryObjectModel.Create(new FileInfo(newFilePath), destPath.Root));

                    }
                    else
                    {

                        // Add file to added entries
                        commandResult.Added.Add(
                            FileEntryObjectModel.Create(new FileInfo(newFilePath), destPath.Root));

                    }

                }
                else
                {
                    
                    // Not supported
                    throw new NotSupportedException();

                }
             
            }

            // Return result
            return commandResult;            

        }

        /// <summary>
        /// Search items
        /// </summary>
        /// <param name="query">Query</param>
        /// <returns>Response result</returns>
        SearchCommandResult IConnectorDriver.Search(string query)
        {
            
            // Create command result
            var commandResult = new SearchCommandResult();

            // Loop through root volumes
            foreach (var rootVolume in RootVolumes)
            {
                
                // Search accessible files and directories in current root volume
                var accessibleFiles = rootVolume.SearchAccessibleFilesAndDirectories(query);

                // Add entries to result
                commandResult.Files.AddRange(accessibleFiles);

            }

            // Return result
            return commandResult;

        }

        /// <summary>
        /// Upload files
        /// </summary>
        /// <param name="target">Target</param>
        /// <param name="files">Files</param>
        /// <returns>Response result</returns>
        UploadCommandResult IConnectorDriver.Upload(string target, IEnumerable<IFileStream> files)
        {

            // Parse target path
            var dest = ParsePath(target);
            
            // Create command result
            var commandResult = new UploadCommandResult();

            // Normalize file streams
            var fileStreams = files as IFileStream[] ?? files.ToArray();

            // Check if max upload size is set and that no files exceeds it            
            if (dest.Root.MaxUploadSizeKb.HasValue
                && !fileStreams.Any(x => (x.Stream.Length/1024) > dest.Root.MaxUploadSizeKb))
            {

                // Max upload size exceeded
                commandResult.SetMaxUploadFileSizeError();

                // End processing
                return commandResult;

            }
            
            // Loop files
            foreach (var file in fileStreams)
            {

                // Validate file name
                if(string.IsNullOrWhiteSpace(file.FileName)) throw new ArgumentNullException(nameof(IFileStream.FileName));

                // Get path
                var path = new FileInfo(Path.Combine(dest.Info.FullName, Path.GetFileName(file.FileName)));

                // Check if it already exists
                if (path.Exists)
                {

                    // Check if overwrite on upload is supported
                    if (dest.Root.UploadOverwrite)
                    {

                        // If file already exist we rename the current file.
                        // If upload is successful, delete temp file. Otherwise, we restore old file.
                        var tmpPath = path.FullName + Guid.NewGuid();

                        // Save file
                        var fileSaved = false;
                        try
                        {
                            file.SaveAs(tmpPath);
                            fileSaved = true;
                        }
                        catch (Exception) { }
                        finally
                        {

                            // Check that file was saved correctly
                            if (fileSaved)
                            {

                                // Delete file
                                File.Delete(path.FullName);

                                // Move file
                                File.Move(tmpPath, path.FullName);

                                // Remove thumbnails
                                dest.RemoveThumbs();

                            }
                            else
                            {

                                // Delete temporary file
                                File.Delete(tmpPath);

                            }

                        }

                    }
                    else
                    {

                        // Ensure directy name is set
                        if(string.IsNullOrEmpty(path.DirectoryName)) throw new ArgumentNullException(nameof(FileInfo.DirectoryName));

                        // Save file
                        file.SaveAs(Path.Combine(path.DirectoryName, FileSystemUtils.GetDuplicatedName(path)));

                    }

                }
                else
                {

                    // Save file
                    file.SaveAs(path.FullName);

                }

                // Check if file refers to an image or not
                if (ImagingUtils.CanProcessFile(path))
                {

                    // Add image to added entries
                    commandResult.Added.Add(
                        ImageEntryObjectModel.Create(new FileInfo(path.FullName), dest.Root));

                }
                else
                {

                    // Add file to added entries
                    commandResult.Added.Add(
                        FileEntryObjectModel.Create(new FileInfo(path.FullName), dest.Root));

                }

            }

            // Return result
            return commandResult;

        }

        /// <summary>
        /// Rotate image
        /// </summary>
        /// <param name="target">Target</param>
        /// <param name="angle">Angle</param>
        /// <returns>Response result</returns>
        RotateImageResult IConnectorDriver.Rotate(string target, int angle)
        {

            // Parse target file path
            var path = ParseFilePath(target);
            
            // Remove thumbnails
            path.RemoveThumbs();

            // Initialize the ImageFactory using the overload to preserve EXIF metadata.
            using (var imageFactory = new ImageFactory())
            {

                // Load, rotate and save an image.
                imageFactory.Load(path.Info.FullName)
                    .Rotate(new RotateLayer(angle))
                    .Save(path.Info.FullName);

            }

            // Create command result
            var commandResult = new RotateImageResult();

            // Add image to changed items
            commandResult.Changed.Add(ImageEntryObjectModel.Create(path.File, path.Root));

            // Return result
            return commandResult;

        }

        /// <summary>
        /// Resize image
        /// </summary>
        /// <param name="target">Target</param>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
        /// <returns>Response result</returns>
        ResizeImageResult IConnectorDriver.Resize(string target, int width, int height)
        {

            // Parse target file path
            var path = ParseFilePath(target);

            // Remove thumbnails
            path.RemoveThumbs();

            // Initialize the ImageFactory using the overload to preserve EXIF metadata.
            using (var imageFactory = new ImageFactory())
            {

                // Load, resize and save an image.
                imageFactory.Load(path.Info.FullName)
                    .Resize(new Size(width, height))
                    .Save(path.Info.FullName);

            }

            // Create command result
            var commandResult = new ResizeImageResult();

            // Add image to changed items
            commandResult.Changed.Add(ImageEntryObjectModel.Create(path.File, path.Root));

            // Return result
            return commandResult;

        }

        /// <summary>
        /// Crop image
        /// </summary>
        /// <param name="target">Target</param>
        /// <param name="x">Start X</param>
        /// <param name="y">Start Y</param>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
        /// <returns>Response result</returns>
        CropImageResult IConnectorDriver.Crop(string target, int x, int y, int width, int height)
        {

            // Parse target file path
            var path = ParseFilePath(target);

            // Remove thumbnails
            path.RemoveThumbs();

            // Initialize the ImageFactory using the overload to preserve EXIF metadata.
            using (var imageFactory = new ImageFactory())
            {

                // Load, crop and save an image.
                imageFactory.Load(path.Info.FullName)
                    .Crop(new Rectangle(x, y, width, height))
                    .Save(path.Info.FullName);

            }

            // Create command result
            var commandResult = new CropImageResult();

            // Add image to changed items
            commandResult.Changed.Add(ImageEntryObjectModel.Create(path.File, path.Root));

            // Return result
            return commandResult;

        }

        /// <summary>
        /// Generate thumbnails
        /// </summary>
        /// <param name="targets">Targets</param>
        /// <returns>Response result</returns>
        GenerateThumbnailsResult IConnectorDriver.GenerateThumbnails(IEnumerable<string> targets)
        {

            // Create command result
            var commandResult = new GenerateThumbnailsResult();

            // Loop each target
            foreach (var targetItem in targets)
            {
                
                // Parse target path
                var path = ParsePath(targetItem);

                // Ensure it's a file
                if (!path.IsFile())
                {
                    
                    // Not a file
                    commandResult.SetObjectNotFileError();
                    return commandResult;

                }

                // Ensure it exists
                if (!path.Exists())
                {

                    // File doesn't exist
                    commandResult.SetFileNotFoundError();
                    return commandResult;

                }

                // Add it to images entries
                commandResult.Images.Add(
                    targetItem, path.Root.GenerateThumbHash(path.Info as FileInfo));

            }
            
            // Return result
            return commandResult;

        }

        /// <summary>
        /// Get thumbnail
        /// </summary>
        /// <param name="target">Target</param>
        /// <returns>Result</returns>
        GetThumbnailResult IConnectorDriver.GetThumbnail(string target)
        {

            // Create command result
            var commandResult = new GetThumbnailResult();

            // Validate target            
            if (string.IsNullOrEmpty(target)) throw new ArgumentNullException(nameof(target));

            // Parse target path
            var path = ParsePath(target);

            // Ensure it's not a directory and that we can create thumbnails for file
            if (!path.IsDirectory()
                && path.Root.CanCreateThumbnail(((FileSystemVolumeFileInfo) path).File))
            {

                // Generate thumbnail
                var thumnailDetails = path.Root.GenerateThumbnail((FileSystemVolumeFileInfo) path);

                // Set result
                commandResult.File = thumnailDetails.ThumbnailFile;
                commandResult.ThumbnailBytes = thumnailDetails.ThumbnailBytes;
                commandResult.ContentType = MimeTypes.GetMimeType(path.Info.Name);
                
                // TODO: Manage caching
                //if (!HttpCacheHelper.IsFileFromCache(path.File, request, response))
                //{
                //    ImageWithMime thumb = path.Root.GenerateThumbnail(path);
                //    return new FileStreamResult(thumb.ImageStream, thumb.Mime);
                //}
                //else
                //{
                //    response.ContentType = Helper.GetMimeType(path.Root.PicturesEditor.ConvertThumbnailExtension(path.File.Extension));
                //    response.End();
                //}

            }
            else
            {

                // Not supported
                throw new NotSupportedException();

            }

            // Return result
            return commandResult;
           
        }

        #endregion

        #region Public

        /// <summary>
        /// Add root file system volume
        /// </summary>
        /// <param name="volume">Volume to add</param>
        public void AddRootVolume(FileSystemRootVolume volume)
        {

            // Add item
            RootVolumes.Add(volume);

            // Set volume id
            volume.VolumeId = VolumePrefix + RootVolumes.Count + ConnectorFileSystemDriverConstants.VolumeSeparator;

        }

        #endregion

        #region Protected

        /// <summary>
        /// Parse configuration
        /// </summary>
        /// <param name="config"></param>
        protected void ParseConfig(IELFinderConfig config)
        {

            // Create root volumes
            foreach (var rootVolumeConfig in config.RootVolumes)
            {

                // Create instance
                var rootVolume = FileSystemRootVolume.Create(
                    rootVolumeConfig.Directory,
                    config.ThumbnailsStorageDirectory);

                // Assign config values

                // Start directory
                if (!string.IsNullOrWhiteSpace(rootVolumeConfig.StartDirectory))
                {
                    rootVolume.StartDirectory = new DirectoryInfo(rootVolumeConfig.StartDirectory);
                }

                // Max upload size in kilobytes
                rootVolume.MaxUploadSizeKb = rootVolumeConfig.MaxUploadSizeKb;

                // Overwrite files when uploading
                rootVolume.UploadOverwrite = rootVolumeConfig.UploadOverwrite;

                // Thumbnails size
                rootVolume.ThumbnailsSize = config.ThumbnailsSize;

                // Thumbnails url
                rootVolume.ThumbnailsUrl = config.ThumbnailsUrl;

                // Url                        
                rootVolume.Url = rootVolumeConfig.Url;

                // Is read-only
                rootVolume.IsReadOnly = rootVolumeConfig.IsReadOnly;

                // Is show only
                rootVolume.IsShowOnly = rootVolumeConfig.IsShowOnly;

                // Is locked
                rootVolume.IsLocked = rootVolumeConfig.IsLocked;

                // Add to the driver
                AddRootVolume(rootVolume);

            }

        }

        /// <summary>
        /// Parse target path
        /// </summary>
        /// <param name="target">Target</param>
        /// <returns>Target</returns>
        protected FileSystemVolumePathInfo ParsePath(string target)
        {

            // Get target root volume
            var targetRootVolume = GetTargetRootVolume(target, RootVolumes);

            // Get target full path
            var targetFullPath = targetRootVolume.GetTargetFullPath(target);

            // Check if it exists as a directory
            if (Directory.Exists(targetFullPath))
            {

                // Directory exists
                return new FileSystemVolumeDirectoryInfo(targetRootVolume, new DirectoryInfo(targetFullPath));

            }

            // Return it as a file
            return new FileSystemVolumeFileInfo(targetRootVolume, new FileInfo(targetFullPath));

        }

        /// <summary>
        /// Parse target directory path
        /// </summary>
        /// <param name="target">Target</param>
        /// <returns>Target</returns>
        protected FileSystemVolumeDirectoryInfo ParseDirectoryPath(string target)
        {

            // Get target root volume
            var targetRootVolume = GetTargetRootVolume(target, RootVolumes);

            // Get target full path
            var targetFullPath = targetRootVolume.GetTargetFullPath(target);

            // Check if it exists as a directory
            if (Directory.Exists(targetFullPath))
            {

                // Directory exists
                return new FileSystemVolumeDirectoryInfo(targetRootVolume, new DirectoryInfo(targetFullPath));

            }

            // Directory not found
            throw new ELFinderTargetDirectoryNotFoundException(target);

        }

        /// <summary>
        /// Parse target file path
        /// </summary>
        /// <param name="target">Target</param>
        /// <returns>Target</returns>
        protected FileSystemVolumeFileInfo ParseFilePath(string target)
        {

            // Get target root volume
            var targetRootVolume = GetTargetRootVolume(target, RootVolumes);

            // Get target full path
            var targetFullPath = targetRootVolume.GetTargetFullPath(target);

            // Check if it exists as file
            if (File.Exists(targetFullPath))
            {

                // File exists
                return new FileSystemVolumeFileInfo(targetRootVolume, new FileInfo(targetFullPath));

            }

            // File not found
            throw new ELFinderTargetFileNotFoundException(target);

        }

        #endregion

        #endregion

        #region Static methods

        /// <summary>
        /// Get target root volume
        /// </summary>
        /// <param name="target">Target</param>
        /// <param name="volumes">Volumes</param>
        /// <returns>Result target root volume</returns>
        private static FileSystemRootVolume GetTargetRootVolume(string target, List<FileSystemRootVolume> volumes)
        {

            // Define volume id
            string volumeId = null;

            // Check if separator char is set in target path
            if (target.Contains(ConnectorFileSystemDriverConstants.VolumeSeparator))
            {

                // Get volume id
                volumeId =
                    target.Substring(
                        0,
                        target.IndexOf(
                            ConnectorFileSystemDriverConstants.VolumeSeparator,
                            StringComparison.InvariantCulture) + 1);

            }

            // Get volume from volume id
            var rootVolume = volumes.GetVolume(volumeId);

            // Return it
            return rootVolume;

        }

        #endregion

    }
}