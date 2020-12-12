using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SenseHome.DB.Mongo;
using SenseHome.DB.Redis;

namespace SenseHome.DataSync.Configurations
{
    public static class DataContextConfiguration
    {
        public static void AddRedisCacheContext(this IServiceCollection services, IConfiguration configuration)
        {
            var redisDBSettings = new RedisDBSettings();
            configuration.GetSection(nameof(RedisDBSettings)).Bind(redisDBSettings);
            services.AddSingleton(redisDBSettings);
            // injecting RedisDBContext
            services.AddSingleton<RedisDBContext>();
        }

        public static void AddDataContext(this IServiceCollection services, IConfiguration configuration)
        {
            //Binding MongoDBSettings from appsettings.json and add as a singleton
            var mongoDBSettings = new MongoDBSettings();
            configuration.GetSection(nameof(MongoDBSettings)).Bind(mongoDBSettings);
            services.AddSingleton(mongoDBSettings);
            // injecting MongoDBContext
            services.AddSingleton<MongoDBContext>();
        }
    }
}
