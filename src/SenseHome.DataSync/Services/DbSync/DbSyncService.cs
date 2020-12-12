using System;
using System.Threading.Tasks;
using SenseHome.DataSync.Models;
using SenseHome.DataSync.Services.RedisCache;
using SenseHome.Repositories.MotionDetection;
using SenseHome.Repositories.TemperatureHumidity;

namespace SenseHome.DataSync.Services.DbSync
{
    public class DbSyncService : IDbSyncService
    {
        private readonly IRedisCacheService redisCacheService;
        private readonly ITemperatureHumidityRepository temperatureHumidityRepository;
        private readonly IMotionDetectionRepository motionDetectionRepository;

        public DbSyncService(
            IRedisCacheService redisCacheService,
            ITemperatureHumidityRepository temperatureHumidityRepository,
            IMotionDetectionRepository motionDetectionRepository)
        {
            this.redisCacheService = redisCacheService;
            this.motionDetectionRepository = motionDetectionRepository;
            this.temperatureHumidityRepository = temperatureHumidityRepository;
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
                        var message = MqttBroadcastMessage.ParseFromJson(value);
                        switch(message.Topic)
                        {
                            case Topics.HomeTemperatureHumidity:
                                await temperatureHumidityRepository.CreateAsync(message.ToTemperatureHumidityModel());
                                break;
                            
                        }
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
