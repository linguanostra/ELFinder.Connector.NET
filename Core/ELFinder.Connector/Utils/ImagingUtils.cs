using System;
using System.Drawing;
using System.IO;
using ImageProcessor;

namespace ELFinder.Connector.Utils
{

    /// <summary>
    /// Imaging utilities
    /// </summary>
    public class ImagingUtils
    {

        #region Static methods

        /// <summary>
        /// Get if given file is supported for imaging
        /// </summary>
        /// <param name="file">File</param>
        /// <returns>True/False, based on result</returns>
        public static bool CanProcessFile(FileInfo file)
        {
            return CanProcessFile(file.Extension);
        }

        /// <summary>
        /// Get if given file is supported for imaging
        /// </summary>
        /// <param name="fileName">File name</param>
        /// <returns>True/False, based on result</returns>
        public static bool CanProcessFile(string fileName)
        {

            fileName = fileName.ToLower().TrimEnd();
            return
                fileName.EndsWith(".png")
                || fileName.EndsWith(".jpg")
                || fileName.EndsWith(".jpeg")
                || fileName.EndsWith(".gif")
                || fileName.EndsWith(".tiff")
                || fileName.EndsWith(".bmp");

        }

        /// <summary>
        /// Get image size
        /// </summary>
        /// <param name="path">Image path</param>
        /// <returns>Result</returns>
        public static Size GetImageSize(string path)
        {

            // Set image factory
            using (var imageFactory = new ImageFactory())
            {
                return imageFactory.Load(path).Image.Size;
            }

        }

        /// <summary>
        /// Generate thumbnail
        /// </summary>
        /// <param name="input">Input</param>
        /// <param name="size">Size</param>
        /// <param name="aspectRatio">Keep aspect ratio</param>
        /// <returns>Result output stream</returns>
        public static byte[] GenerateThumbnail(byte[] input, int size, bool aspectRatio)
        {

            // Set input stream
            using (var inStream = new MemoryStream(input))
            {

                // Set output stream
                using (var outStream = new MemoryStream())
                {

                    // Set image factory
                    using (var imageFactory = new ImageFactory())
                    {

                        // Load image
                        imageFactory.Load(inStream);

                        // Compute width/height
                        int width, height;

                        if (aspectRatio)
                        {
                            var originalWidth = (double) imageFactory.Image.Width;
                            var originalHeight = (double) imageFactory.Image.Height;
                            var percentWidth = originalWidth != 0 ? size/originalWidth : 0;
                            var percentHeight = originalHeight != 0 ? size/originalHeight : 0;
                            var percent = percentHeight < percentWidth ? percentHeight : percentWidth;
                            width = (int) (originalWidth*percent);
                            height = (int) (originalHeight*percent);
                        }
                        else
                        {
                            width = size;
                            height = size;
                        }

                        // Resize image
                        imageFactory.Resize(new Size(width, height));

                        // Save it to output stream
                        imageFactory.Save(outStream);

                    }

                    // Return bytes
                    return outStream.ToArray();

                }

            }

        }

        /// <summary>
        /// Convert thumnail extension for image
        /// </summary>
        /// <param name="imageExtension">Image extension</param>
        /// <returns>Result extension</returns>
        public static string ConvertThumbnailExtension(string imageExtension)
        {
            var ext = imageExtension.ToLower();
            switch (ext)
            {
                case ".tiff":
                    return ".png";
                case ".png":
                case ".jpg":
                case ".jpeg":
                case ".gif":
                    return ext;
                default:
                    throw new ArgumentException(nameof(imageExtension));
            }
        }

        #endregion

    }
}