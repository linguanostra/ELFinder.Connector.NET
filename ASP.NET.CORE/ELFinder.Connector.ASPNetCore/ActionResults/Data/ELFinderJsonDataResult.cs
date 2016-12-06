using ELFinder.Connector.Web.Serialization;
using ELFinder.Connector.Web.Serialization.Values;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters.Json.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ELFinder.Connector.ASPNetCore.ActionResults.Data
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
        //public ELFinderJsonDataResult()
        //{
        //}

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="data">Data</param>
        public ELFinderJsonDataResult(object data):base(data)
        {
        }

        #endregion

        #region Overrides

        public override Task ExecuteResultAsync(ActionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // Get response
            var response = context.HttpContext.Response;

            // Set content type
            var mediaType = new MediaTypeHeaderValue("application/json");
            // Set content encoding
            mediaType.Encoding = Encoding.UTF8;

            response.ContentType = mediaType.ToString();

            // Set status code
            response.StatusCode = 200;

            // Assign data content
            if (Value != null)
            {
                

            }
            var services = context.HttpContext.RequestServices;
            var executor = services.GetRequiredService<ELFinderJsonResultExecutor>();
            return executor.ExecuteAsync(context, this);
        }

        /// <summary>
        /// Enables processing of the result of an action method by a custom type that inherits from the <see cref="T:System.Web.Mvc.ActionResult"/> class.
        /// </summary>
        /// <param name="context">The context within which the result is executed.</param><exception cref="T:System.ArgumentNullException">The <paramref name="context"/> parameter is null.</exception>
        public override void ExecuteResult(ActionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // Get response
            var response = context.HttpContext.Response;

            // Set content type
            var mediaType = new MediaTypeHeaderValue("application/json");
            // Set content encoding
            mediaType.Encoding = Encoding.UTF8;

            response.ContentType = mediaType.ToString();

            // Set status code
            response.StatusCode = 200;

            var services = context.HttpContext.RequestServices;
            var executor = services.GetRequiredService<ELFinderJsonResultExecutor>();
            executor.ExecuteAsync(context, this);
        }

        #endregion

    }
}