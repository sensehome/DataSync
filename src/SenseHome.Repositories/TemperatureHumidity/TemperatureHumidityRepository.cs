using MongoDB.Driver;
using SenseHome.DB.Mongo;
using System.Threading.Tasks;

namespace SenseHome.Repositories.TemperatureHumidity
{
    public class TemperatureHumidityRepository : ITemperatureHumidityRepository
    {
        private readonly IMongoCollection<DomainModels.TemperatureHumidity> collection;

        public TemperatureHumidityRepository(MongoDBContext mongoDbContext)
        {
            collection = mongoDbContext.Database.GetCollection<DomainModels.TemperatureHumidity>("temperatureHumidities");
        }

        public async Task<DomainModels.TemperatureHumidity> CreateAsync(DomainModels.TemperatureHumidity entity)
        {
            await collection.InsertOneAsync(entity);
            return entity;
        }
    }
}
