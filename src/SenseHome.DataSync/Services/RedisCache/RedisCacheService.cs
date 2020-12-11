using System.Threading.Tasks;
using SenseHome.DB.Redis;

namespace SenseHome.DataSync.Services.RedisCache
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly RedisDBContext redisDBContext;

        public RedisCacheService(RedisDBContext redisDatabase)
        {
            this.redisDBContext = redisDatabase;
        }

        public async Task<string> PopBackAsync(string key)
        {
            var value = await redisDBContext.Database.ListRightPopAsync(key);
            return value;
        }

        public async Task<string> PopTopAsync(string key)
        {
            var value = await redisDBContext.Database.ListLeftPopAsync(key);
            return value;
        }

        public async Task PushBackAsync(string key, string value)
        {
            await redisDBContext.Database.ListRightPushAsync(key, value);
        }
    }
}