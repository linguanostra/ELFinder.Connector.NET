namespace ELFinder.Connector.Commands.Results.Common.Errors
{

    /// <summary>
    /// Command errors
    /// </summary>
    public enum CommandErrorsEnum
    {

        /// <summary>
        /// Error
        /// </summary>
        Error,
        /// <summary>
        /// Unknown error.
        /// </summary>
        ErrUnknown,
        /// <summary>
        /// Unknown command.
        /// </summary>
        ErrUnknownCmd,
        /// <summary>
        /// Invalid elFinder configuration! URL option is not set.
        /// </summary>
        ErrUrl,
        /// <summary>
        /// Access denied.
        /// </summary>
        ErrAccess,
        /// <summary>
        /// Unable to connect to backend.
        /// </summary>
        ErrConnect,
        /// <summary>
        /// Connection aborted.
        /// </summary>
        ErrAbort,
        /// <summary>
        /// Connection timeout.
        /// </summary>
        ErrTimeout,
        /// <summary>
        /// Backend not found.
        /// </summary>
        ErrNotFound,
        /// <summary>
        /// Invalid backend response.
        /// </summary>
        ErrResponse,
        /// <summary>
        /// Invalid backend configuration.
        /// </summary>
        ErrConf,
        /// <summary>
        /// PHP JSON module not installed.
        /// </summary>
        ErrJson,
        /// <summary>
        /// Readable volumes not available.
        /// </summary>
        ErrNoVolumes,
        /// <summary>
        /// Invalid parameters for command "$1".
        /// </summary>
        ErrCmdParams,
        /// <summary>
        /// Data is not JSON.
        /// </summary>
        ErrDataNotJson,
        /// <summary>
        /// Data is empty.
        /// </summary>
        ErrDataEmpty,
        /// <summary>
        /// Backend request requires command name.
        /// </summary>
        ErrCmdReq,
        /// <summary>
        /// Unable to open "$1".
        /// </summary>
        ErrOpen,
        /// <summary>
        /// Object is not a folder.
        /// </summary>
        ErrNotFolder,
        /// <summary>
        /// Object is not a file.
        /// </summary>
        ErrNotFile,
        /// <summary>
        /// Unable to read "$1".
        /// </summary>
        ErrRead,
        /// <summary>
        /// Unable to write into "$1".
        /// </summary>
        ErrWrite,
        /// <summary>
        /// Permission denied.
        /// </summary>
        ErrPerm,
        /// <summary>
        /// "$1" is locked and can not be renamed, moved or removed.
        /// </summary>
        ErrLocked,
        /// <summary>
        /// File named "$1" already exists.
        /// </summary>
        ErrExists,
        /// <summary>
        /// Invalid file name.
        /// </summary>
        ErrInvName,
        /// <summary>
        /// Folder not found.
        /// </summary>
        ErrFolderNotFound,
        /// <summary>
        /// File not found.
        /// </summary>
        ErrFileNotFound,
        /// <summary>
        /// Target folder "$1" not found.
        /// </summary>
        ErrTrgFolderNotFound,
        /// <summary>
        /// Browser prevented opening popup window. To open file enable it in browser options.
        /// </summary>
        ErrPopup,
        /// <summary>
        /// Unable to create folder "$1".
        /// </summary>
        ErrMkdir,
        /// <summary>
        /// Unable to create file "$1".
        /// </summary>
        ErrMkfile,
        /// <summary>
        /// Unable to rename "$1".
        /// </summary>
        ErrRename,
        /// <summary>
        /// Copying files from volume "$1" not allowed.
        /// </summary>
        ErrCopyFrom,
        /// <summary>
        /// Copying files to volume "$1" not allowed.
        /// </summary>
        ErrCopyTo,
        /// <summary>
        /// Upload error.
        /// </summary>
        ErrUpload,
        /// <summary>
        /// Unable to upload "$1".
        /// </summary>
        ErrUploadFile,
        /// <summary>
        /// No files found for upload.
        /// </summary>
        ErrUploadNoFiles,
        /// <summary>
        /// Data exceeds the maximum allowed size.
        /// </summary>
        ErrUploadTotalSize,
        /// <summary>
        /// File exceeds maximum allowed size.
        /// </summary>
        ErrUploadFileSize,
        /// <summary>
        /// File type not allowed.
        /// </summary>
        ErrUploadMime,
        /// <summary>
        /// "$1" transfer error.
        /// </summary>
        ErrUploadTransfer,
        /// <summary>
        /// Object "$1" already exists at this location and can not be replaced by object with another type.
        /// </summary>
        ErrNotReplace,
        /// <summary>
        /// Unable to replace "$1".
        /// </summary>
        ErrReplace,
        /// <summary>
        /// Unable to save "$1".
        /// </summary>
        ErrSave,
        /// <summary>
        /// Unable to copy "$1".
        /// </summary>
        ErrCopy,
        /// <summary>
        /// Unable to move "$1".
        /// </summary>
        ErrMove,
        /// <summary>
        /// Unable to copy "$1" into itself.
        /// </summary>
        ErrCopyInItself,
        /// <summary>
        /// Unable to remove "$1".
        /// </summary>
        ErrRm,
        /// <summary>
        /// Unable remove source file(s).
        /// </summary>
        ErrRmSrc,
        /// <summary>
        /// Unable to extract files from "$1".
        /// </summary>
        ErrExtract,
        /// <summary>
        /// Unable to create archive.
        /// </summary>
        ErrArchive,
        /// <summary>
        /// Unsupported archive type.
        /// </summary>
        ErrArcType,
        /// <summary>
        /// File is not archive or has unsupported archive type.
        /// </summary>
        ErrNoArchive,
        /// <summary>
        /// Backend does not support this command.
        /// </summary>
        ErrCmdNoSupport,
        /// <summary>
        /// The folder "$1" can\
        /// </summary>
        ErrReplByChild,
        /// <summary>
        /// For security reason denied to unpack archives contains symlinks or files with not allowed names.
        /// </summary>
        ErrArcSymlinks,
        /// <summary>
        /// Archive files exceeds maximum allowed size.
        /// </summary>
        ErrArcMaxSize,
        /// <summary>
        /// Unable to resize "$1".
        /// </summary>
        ErrResize,
        /// <summary>
        /// Invalid rotate degree.
        /// </summary>
        ErrResizeDegree,
        /// <summary>
        /// Unable to rotate image.
        /// </summary>
        ErrResizeRotate,
        /// <summary>
        /// Invalid image size.
        /// </summary>
        ErrResizeSize,
        /// <summary>
        /// Image size not changed.
        /// </summary>
        ErrResizeNoChange,
        /// <summary>
        /// Unsupported file type.
        /// </summary>
        ErrUsupportType,
        /// <summary>
        /// File "$1" is not in UTF-8 and cannot be edited.
        /// </summary>
        ErrNotUtf8Content,
        /// <summary>
        /// Unable to mount "$1".
        /// </summary>
        ErrNetMount,
        /// <summary>
        /// Unsupported protocol.
        /// </summary>
        ErrNetMountNoDriver,
        /// <summary>
        /// Mount failed.
        /// </summary>
        ErrNetMountFailed,
        /// <summary>
        /// Host required.
        /// </summary>
        ErrNetMountHostReq,
        /// <summary>
        /// Your session has expired due to inactivity.
        /// </summary>
        ErrSessionExpires,
        /// <summary>
        /// Unable to create temporary directory: "$1"
        /// </summary>
        ErrCreatingTempDir,
        /// <summary>
        /// Unable to download file from FTP: "$1"
        /// </summary>
        ErrFtpDownloadFile,
        /// <summary>
        /// Unable to upload file to FTP: "$1"
        /// </summary>
        ErrFtpUploadFile,
        /// <summary>
        /// Unable to create remote directory on FTP: "$1"
        /// </summary>
        ErrFtpMkdir,
        /// <summary>
        /// Error while archiving files: "$1"
        /// </summary>
        ErrArchiveExec,
        /// <summary>
        /// Error while extracting files: "$1"
        /// </summary>
        ErrExtractExec,
    }
}