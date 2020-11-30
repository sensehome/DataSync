using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SenseHome.DataSync.Services.RedisCache;
using StackExchange.Redis;

namespace SenseHome.DataSync.Configurations
{
    public static class RedisCacheDbConfiguration
    {
        public static void AddRedisCacheDatabaseService(this IServiceCollection services, IConfiguration configuration)
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(configuration.GetSection("RedisConnection").Value);
            services.AddSingleton(redis.GetDatabase());
            services.AddSingleton<IRedisCacheService, RedisCacheService>();
        }
    }
}
