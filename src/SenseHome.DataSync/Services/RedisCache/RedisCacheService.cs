using System.Collections.Generic;
using System.Threading.Tasks;
using SenseHome.DB.Redis;
using StackExchange.Redis;

namespace SenseHome.DataSync.Services.RedisCache
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly RedisDBContext redisDBContext;
        private readonly RedisDBSettings redisDBSettings;
        private readonly IServer server;

        public RedisCacheService(RedisDBContext redisDBContext, RedisDBSettings redisDBSettings)
        {
            this.redisDBContext = redisDBContext;
            this.redisDBSettings = redisDBSettings;
            server = redisDBContext.Connection.GetServer(redisDBSettings.Host, redisDBSettings.Port);
        }

        public IEnumerable<RedisKey> GetKeys()
        {
            return server.Keys(redisDBSettings.DatabaseIndex);
        }

        public async Task<long> LengthAsync(string key)
        {
            var length = await redisDBContext.Database.ListLengthAsync(key);
            return length;
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