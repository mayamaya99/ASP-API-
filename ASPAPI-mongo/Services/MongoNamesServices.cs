using ASPAPI_mongo.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ASPAPI_mongo.Services
{
    public class MongoNamesServices
    {
        private readonly IMongoCollection<MongoNames> _namesCollection;
        public MongoNamesServices(
        IOptions<NamesDatabaseSettings> namesDatabaseSettings)
        {
            var mongoClient = new MongoClient(
            namesDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(
            namesDatabaseSettings.Value.DatabaseName);
            _namesCollection = mongoDatabase.GetCollection<MongoNames>(
            namesDatabaseSettings.Value.NamesCollectionName);
        }
        public async Task<List<MongoNames>> GetAsync() =>
        await _namesCollection.Find(_ => true).ToListAsync();
        public async Task<MongoNames?> GetAsync(string id) =>
        await _namesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task CreateAsync(MongoNames newName) =>
        await _namesCollection.InsertOneAsync(newName);
        public async Task UpdateAsync(string id, MongoNames updatedNames) =>
        await _namesCollection.ReplaceOneAsync(x => x.Id == id, updatedNames);
        public async Task RemoveAsync(string id) =>
        await _namesCollection.DeleteOneAsync(x => x.Id == id);

    }
}
