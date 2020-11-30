using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace SenseHome.DataSync.Configurations
{
    public static class RedisCacheDbConfiguration
    {
        public static void AddRedisCacheDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(configuration.GetSection("RedisConnection").Value);
            services.AddSingleton(redis.GetDatabase());
        }
    }
}
