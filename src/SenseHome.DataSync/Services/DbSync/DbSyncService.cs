using System;
using System.Threading.Tasks;
using SenseHome.DataSync.Services.RedisCache;

namespace SenseHome.DataSync.Services.DbSync
{
    public class DbSyncService : IDbSyncService
    {
        private readonly IRedisCacheService redisCacheService;

        public DbSyncService(IRedisCacheService redisCacheService)
        {
            this.redisCacheService = redisCacheService;
        }

        public async Task Execute(object state)
        {
            var keys = redisCacheService.GetKeys();
            foreach(var key in keys)
            {
                try
                {
                    var len = await redisCacheService.LengthAsync(key);
                    for(var i=0; i<len; i++)
                    {
                        var value = await redisCacheService.PopBackAsync(key);
                        Console.WriteLine(value);
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            
        }
    }
}
