using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ELFinder.WebServer.ASPNetCore.Config;
using ELFinder.Connector.Config;
using Microsoft.AspNetCore.Mvc;
using ELFinder.Connector.ASPNetCore.ModelBinders;
using ELFinder.Connector.ASPNetCore.ActionResults.Data;

namespace ELFinder.WebServer.ASPNetCore
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc().Services.Configure<MvcOptions>(options => {
                // Use custom ELFinder model binder
                options.ModelBinderProviders.Insert(0, new ELFinderModelBinderProvider());
                
            });
            // Use custom ELFinder Json Result Executer
            services.AddSingleton<ELFinderJsonResultExecutor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                // Commands
                routes.MapRoute("Connector", "ELFinderConnector",
                    new { controller = "ELFinderConnector", action = "Main" });

                // Thumbnails
                routes.MapRoute("Thumbnauls", "Thumbnails/{target}",
                    new { controller = "ELFinderConnector", action = "Thumbnails" });

                // Index view
                routes.MapRoute("Default", "{controller}/{action}",
                    new { controller = "ELFinder", action = "Index" }
                );
            });

            // Initialize ELFinder configuration
            InitELFinderConfiguration(env);
        }

        // Initialize ELFinder configuration
        protected void InitELFinderConfiguration(IHostingEnvironment env)
        {

            SharedConfig.ELFinder = new ELFinderConfig(
                env.ContentRootPath + @"\App_Data",
                thumbnailsUrl: "Thumbnails/"
                );

            SharedConfig.ELFinder.RootVolumes.Add(
                new ELFinderRootVolumeConfigEntry(
                    env.ContentRootPath + @"\App_Data\Files",
                    isLocked: false,
                    isReadOnly: false,
                    isShowOnly: false,
                    maxUploadSizeKb: null,      // null = Unlimited upload size
                    uploadOverwrite: true,
                    startDirectory: ""));

        }
    }
}
