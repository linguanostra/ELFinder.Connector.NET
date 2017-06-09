﻿

using Microsoft.AspNetCore.Mvc;

namespace ELFinder.WebServer.ASPNet.Controllers
{

    /// <summary>
    /// ELFinder controller
    /// IMPORTANT: If you use this controller in your existing application, make sure to register the custom ELFinder model binder (ELFinderModelBinder) with ASP.NET MVC.
    ///            Look at the Application_Start() method in Global.asax.cs on how to do so.
    /// </summary>
    public class ELFinderController : Controller
    {

        #region Methods

        /// <summary>
        /// Index page for V 2.1
        /// </summary>
        /// <returns>Result view</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Index page for V 2
        /// </summary>
        /// <returns>Result view</returns>
        public ActionResult Index2()
        {
            return View();
        }

        #endregion

    }
}