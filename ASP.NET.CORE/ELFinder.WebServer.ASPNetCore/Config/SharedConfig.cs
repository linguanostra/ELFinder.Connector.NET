﻿using ELFinder.Connector.Config;

namespace ELFinder.WebServer.ASPNetCore.Config
{

    /// <summary>
    /// Shared config
    /// </summary>
    public static class SharedConfig
    {

        #region Properties

        /// <summary>
        /// ELFinder shared configuration
        /// </summary>
        public static ELFinderConfig ELFinder { get; set; }

        #endregion

    }

}