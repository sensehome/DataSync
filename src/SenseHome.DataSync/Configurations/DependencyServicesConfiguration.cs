using Microsoft.Extensions.DependencyInjection;
using SenseHome.DataSync.Services.RedisCache;

namespace SenseHome.DataSync.Configurations
{
    public static class DependencyServicesConfiguration
    {
        public static void AddRequiredServices(this IServiceCollection services)
        {
            services.AddSingleton<IRedisCacheService, RedisCacheService>();
        }
    }
}
