// -----------------------------------------------------------------------
// <copyright file="ResponseType.cs" company="James South">
//     Copyright (c) James South.
//     Licensed under the Apache License, Version 2.0.
// </copyright>
// -----------------------------------------------------------------------

using System.ComponentModel;

namespace ELFinder.Connector.ImageProcessor.Imaging
{
    #region Using

    

    #endregion

    /// <summary>
    /// Globally available enumeration which specifies the correct HTTP MIME type of
    /// the output stream for different response types.
    /// <para>
    /// http://en.wikipedia.org/wiki/Internet_media_type"/
    /// </para>
    /// </summary>
    public enum ResponseType
    {
        #region Image
        /// <summary>
        /// The correct HTTP MIME type of the output stream for bmp images.
        /// </summary>
        [Description("image/bmp")]
        Bmp,

        /// <summary>
        /// The correct HTTP MIME type of the output stream for gif images.
        /// </summary>
        [Description("image/gif")]
        Gif,

        /// <summary>
        /// The correct HTTP MIME type of the output stream for jpeg images.
        /// </summary>
        [Description("image/jpeg")]
        Jpeg,

        /// <summary>
        /// The correct HTTP MIME type of the output stream for png images.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Png", Justification = "File extension name")]
        [Description("image/png")]
        Png,

        /// <summary>
        /// The correct HTTP MIME type of the output stream for svg images.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Svg", Justification = "File extension name")]
        [Description("image/svg+xml")]
        Svg,

        /// <summary>
        /// The correct HTTP MIME type of the output stream for tiff images.
        /// </summary>
        [Description("image/tiff")]
        Tiff,
        #endregion
    }
}
