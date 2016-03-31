using ELFinder.Connector.Commands.Results.Common.Data;
using ELFinder.Connector.Commands.Results.Open.Common;
using ELFinder.Connector.Drivers.FileSystem.Models.Directories.Common;

namespace ELFinder.Connector.Commands.Results.Open
{

    /// <summary>
    /// Open command result
    /// </summary>
    public class OpenCommandResult : OpenBaseCommandResult
    {

        #region Constructors

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="currentWorkingDirectory">Current working directory</param>
        /// <param name="options">Options</param>
        public OpenCommandResult(BaseDirectoryEntryObjectModel currentWorkingDirectory, OptionsResultData options = null) : base(currentWorkingDirectory, options)
        {

            // Add current working directory to files
            Files.Add(currentWorkingDirectory);

        }

        #endregion  

    }

}