using System.Collections.Generic;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace SenseHome.DataSync.Services.RedisCache
{
    public interface IRedisCacheService
    {
        Task PushBackAsync(string key, string value);
        Task<string> PopTopAsync(string key);
        Task<string> PopBackAsync(string key);
        Task<long> LengthAsync(string key);
        IEnumerable<RedisKey> GetKeys();
    }
}
