using System.IO;
using System.Text;
using System.Web.Mvc;
using ELFinder.Connector.Web.Serialization;
using ELFinder.Connector.Web.Serialization.Values;
using Newtonsoft.Json;

namespace ELFinder.Connector.ASPNet.ActionResults.Data
{

    /// <summary>
    /// ELFInder Json data result
    /// </summary>
    public class ELFinderJsonDataResult : JsonResult
    {

        #region Constructors

        /// <summary>
        /// Create a new instance
        /// </summary>
        public ELFinderJsonDataResult()
        {
        }

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="data">Data</param>
        public ELFinderJsonDataResult(object data)
        {
            Data = data;
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Enables processing of the result of an action method by a custom type that inherits from the <see cref="T:System.Web.Mvc.ActionResult"/> class.
        /// </summary>
        /// <param name="context">The context within which the result is executed.</param><exception cref="T:System.ArgumentNullException">The <paramref name="context"/> parameter is null.</exception>
        public override void ExecuteResult(ControllerContext context)
        {

            // Get response
            var response = context.HttpContext.Response;

            // Set content type
            response.ContentType = "application/json";

            // Set content encoding
            response.ContentEncoding = Encoding.UTF8;

            // Set status code
            response.StatusCode = 200;

            // Assign data content
            if (Data != null)
            {
                using (var writer = new StreamWriter(response.OutputStream))
                {

                    // Init Json text writer
                    using (var jsonWriter = new JsonTextWriter(writer))
                    {

                        // Init Json serializer
                        var serializer = GetJsonSerializer();

                        // Serialize model
                        serializer.Serialize(jsonWriter, Data);

                    }

                }

            }

        }

        #endregion

        #region Methods

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

    }
}