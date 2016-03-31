using System.Web.Mvc;

namespace ELFinder.WebServer.ASPNet.Controllers
{

    /// <summary>
    /// ELFinder controller
    /// </summary>
    public class ELFinderController : Controller
    {

        #region Methods

        /// <summary>
        /// Index page
        /// </summary>
        /// <returns>Result view</returns>
        public ActionResult Index()
        {
            return View();
        }

        #endregion

    }
}