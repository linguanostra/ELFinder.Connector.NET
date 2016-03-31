using Nancy;

namespace ELFinder.WebServer.Nancy.Modules
{

    /// <summary>
    /// ELFinder module
    /// </summary>
    public class ELFinderModule : NancyModule
    {

        #region Constructors

        /// <summary>
        /// Create a new instance
        /// </summary>
        public ELFinderModule()
        {

            // Init routes
            InitRoutes();

        }

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="modulePath">Module path</param>
        public ELFinderModule(string modulePath) : base(modulePath)
        {

            // Init routes
            InitRoutes();

        }

        #endregion

        #region Methods

        /// <summary>
        /// Init routes
        /// </summary>
        protected void InitRoutes()
        {

            // ELFinder main view
            Get["/"] = GetELFinderMainView;

        }

        #endregion

        #region Methods

        /// <summary>
        /// Get ELFinder main view
        /// </summary>
        /// <param name="parameters">Parameters</param>
        /// <returns>Resullt</returns>
        private dynamic GetELFinderMainView(dynamic parameters)
        {

            // Return view
            return View["ELFinder/Main"];

        }

        #endregion

    }

}