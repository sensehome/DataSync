using System.Collections.Generic;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace SenseHome.DataSync.Services.RedisCache
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDatabase redisDatabase;

        public RedisCacheService(IDatabase redisDatabase)
        {
            this.redisDatabase = redisDatabase;
        }

        public async Task<string> PopBackAsync(string key)
        {
            var value = await redisDatabase.ListRightPopAsync(key);
            return value;
        }

        public async Task<string> PopTopAsync(string key)
        {
            var value = await redisDatabase.ListLeftPopAsync(key);
            return value;
        }

        public async Task PushBackAsync(string key, string value)
        {
            await redisDatabase.ListRightPushAsync(key, value);
        }
    }
}