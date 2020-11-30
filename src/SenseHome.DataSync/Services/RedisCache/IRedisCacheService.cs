using System.Collections.Generic;
using System.Threading.Tasks;

namespace SenseHome.DataSync.Services.RedisCache
{
    public interface IRedisCacheService
    {
        Task PushBackAsync(string key, string value);
        Task<string> PopTopAsync(string key);
        Task<string> PopBackAsync(string key);
    }
}
