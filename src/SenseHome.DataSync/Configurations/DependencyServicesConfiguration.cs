using Microsoft.Extensions.DependencyInjection;
using SenseHome.DataSync.Services.DbSync;
using SenseHome.DataSync.Services.RedisCache;
using SenseHome.DataSync.Services.Scheduler;
using SenseHome.Repositories.MotionDetection;
using SenseHome.Repositories.TemperatureHumidity;

namespace SenseHome.DataSync.Configurations
{
    public static class DependencyServicesConfiguration
    {
        public static void AddRequiredServices(this IServiceCollection services)
        {
            services.AddSingleton<IRedisCacheService, RedisCacheService>();
            services.AddSingleton<IDbSyncService, DbSyncService>();
            services.AddSingleton<SchedulerService>();
            services.AddHostedService<ISchedulerService>(serviceProvider =>
            {
                return serviceProvider.GetService<SchedulerService>();
            });
            services.AddSingleton<ITemperatureHumidityRepository, TemperatureHumidityRepository>();
            services.AddSingleton<IMotionDetectionRepository, MotionDetectionRepository>();
        }
    }
}
