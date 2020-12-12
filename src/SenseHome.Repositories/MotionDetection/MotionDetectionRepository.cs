using MongoDB.Driver;
using SenseHome.DB.Mongo;
using System.Threading.Tasks;

namespace SenseHome.Repositories.MotionDetection
{
    public class MotionDetectionRepository : IMotionDetectionRepository
    {
        private readonly IMongoCollection<DomainModels.MotionDetection> collection;

        public MotionDetectionRepository(MongoDBContext mongoDbContext)
        {
            collection = mongoDbContext.Database.GetCollection<DomainModels.MotionDetection>("motionDetections");
        }

        public async Task<DomainModels.MotionDetection> CreateAsync(DomainModels.MotionDetection entity)
        {
            await collection.InsertOneAsync(entity);
            return entity;
        }
    }
}
