﻿using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;


namespace steeltoe_helper
{
    public class SteeltoeHelper
    {
        static public void Start(string env)
        {
            AreaRegistration.RegisterAllAreas();
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Create applications configuration
            ApplicationConfig.Configure(env);

            // Create logging system using configuration
            LoggingConfig.Configure(ApplicationConfig.Configuration);

            // Add management endpoints to application
            ManagementConfig.ConfigureManagementActuators(
                ApplicationConfig.Configuration,
                LoggingConfig.LoggerProvider,
                GlobalConfiguration.Configuration.Services.GetApiExplorer(),
                LoggingConfig.LoggerFactory);

            // Uncomment if you want to enable metrics exporting to Cloud Foundry metrics exporter, it's not enabled by default
            // Also see ManagementConfig.ConfigureManagementActuators() for more configuration needs
            ManagementConfig.UseCloudFoundryMetricsExporter(ApplicationConfig.Configuration, LoggingConfig.LoggerFactory);

            // Start the management endpoints
            ManagementConfig.Start();
        }

        static public void Stop()
        {
            ManagementConfig.Stop();
        }
    }
}
