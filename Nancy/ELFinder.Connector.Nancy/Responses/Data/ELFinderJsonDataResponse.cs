using System;
using System.IO;
using ELFinder.Connector.Web.Serialization;
using ELFinder.Connector.Web.Serialization.Values;
using Nancy;
using Nancy.IO;
using Nancy.Json;
using Newtonsoft.Json;

namespace ELFinder.Connector.Nancy.Responses.Data
{

    /// <summary>
    /// ELFinder Json data response
    /// </summary>
    /// <typeparam name="TModel">Model type</typeparam>
    public class ELFinderJsonDataResponse<TModel> : Response
        where TModel : class
    {

        #region Constructors

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="content">Content</param>
        public ELFinderJsonDataResponse(TModel content = null)
        {

            // Assign content type
            ContentType = ResponseContentType;

            // Assign status code
            StatusCode = HttpStatusCode.OK;

            // Assign data content
            Contents = content == null ? NoBody : GetJsonContents(content);

        }

        #endregion

        #region Methods

        /// <summary>
        /// Get Json content
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>Result content</returns>
        protected Action<Stream> GetJsonContents(TModel model)
        {

            // Return output stream as the action parameter
            return outputStream =>
            {

                // Init stream writer                
                using (var writer = new StreamWriter(new UnclosableStreamWrapper(outputStream)))
                {

                    // Init Json text writer
                    using (var jsonWriter = new JsonTextWriter(writer))
                    {

                        // Init Json serializer
                        var serializer = GetJsonSerializer();

                        // Serialize model
                        serializer.Serialize(jsonWriter, model);

                    }

                }

            };

        }

        #endregion

        #region Virtual methods

        /// <summary>
        /// Get the Json serializer
        /// </summary>
        /// <returns>Result</returns>
        protected virtual JsonSerializer GetJsonSerializer()
        {

            // Get default serializer
            var serializer = new JsonSerializer();

            // Add converters
            serializer.Converters.Add(new ELFinderResponseBooleanValueConverter());

            // Set contract resolver
            serializer.ContractResolver = new ELFinderResponseContractResolver();

            // Return it
            return serializer;

        }

        #endregion

        #region Static properties

        /// <summary>
        /// Response content type
        /// </summary>
        private static string ResponseContentType =>
            string.Concat("application/json", ResponseEncoding);

        /// <summary>
        /// Response encoding
        /// </summary>
        private static string ResponseEncoding =>
            !string.IsNullOrWhiteSpace(JsonSettings.DefaultEncoding.EncodingName)
                ? string.Concat("; charset=", JsonSettings.DefaultEncoding)
                : string.Empty;

        #endregion        

    }

    /// <summary>
    /// ELFinder Json data response
    /// </summary>
    public class ELFinderJsonDataResponse : ELFinderJsonDataResponse<object>
    {

        #region Constructors

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="content">Data</param>
        public ELFinderJsonDataResponse(object content = null) : base(content)
        {
        }

        #endregion

    }

}