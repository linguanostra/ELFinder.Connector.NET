using ELFinder.Connector.Nancy.Converters;
using Nancy;
using Nancy.Bootstrapper;

namespace ELFinder.Connector.Nancy.Bootstrap
{

    /// <summary>
    /// ELFinder base nancy bootstrapper
    /// </summary>
    public abstract class ELFinderBaseNancyBootstrapper : DefaultNancyBootstrapper
    {

        #region Properties

        /// <summary>
        /// Nancy custom configuration
        /// </summary>
        private NancyInternalConfiguration _configuration;

        #endregion

        #region Overrides

        /// <summary>
        /// Nancy internal configuration
        /// </summary>
        protected override NancyInternalConfiguration InternalConfiguration
            => _configuration ?? (_configuration = GetCustomConfiguration());

        #endregion

        #region Methods

        /// <summary>
        /// Get custom Nancy configuration
        /// </summary>
        /// <returns>Custom configuration</returns>
        protected virtual NancyInternalConfiguration GetCustomConfiguration()
        {

            // Get default configuration
            var config = NancyInternalConfiguration.Default;

            // Replace field name converter
            config.FieldNameConverter = typeof(ELFinderFieldNameConverter);

            // Return updated configuration
            return config;

        }

        #endregion

    }
}