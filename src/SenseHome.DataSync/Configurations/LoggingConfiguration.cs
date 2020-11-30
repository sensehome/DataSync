using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace SenseHome.DataSync.Configurations
{
    public static class LoggingConfiguration
    {
        public static void AddSenseHomeDataLoggingService(this IServiceCollection services, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration, "Serilog").CreateLogger();
            AppDomain.CurrentDomain.ProcessExit += (s, e) => Log.CloseAndFlush();
            services.AddSingleton(Log.Logger);
        }
    }
}
