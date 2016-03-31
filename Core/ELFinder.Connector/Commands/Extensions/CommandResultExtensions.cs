using ELFinder.Connector.Commands.Results.Common;

namespace ELFinder.Connector.Commands.Extensions
{

    /// <summary>
    /// Command result extensions
    /// </summary>
    public static class CommandResultExtensions
    {

        #region Extension methods

        /// <summary>
        /// Get if given command result has an error defined or not
        /// </summary>
        /// <param name="result">Result</param>
        /// <returns>True/False, based on result</returns>
        public static bool HasError(this CommandResult result)
        {

            return result.Error != null;
        } 

        #endregion
         
    }
}