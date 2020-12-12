using Microsoft.Extensions.DependencyInjection;
using SenseHome.DataSync.Services.DbSync;
using SenseHome.DataSync.Services.RedisCache;
using SenseHome.DataSync.Services.Scheduler;

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
        }
    }
}
