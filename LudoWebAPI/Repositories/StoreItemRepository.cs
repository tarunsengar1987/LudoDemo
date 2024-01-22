using LudoWebAPI.Interfaces;
using LudoWebAPI.Models;
using LudoWebAPI.Repositories.Context;
using MongoDB.Driver;

namespace LudoWebAPI.Repositories
{
    public class StoreItemRepository : IStoreRepository
    {
        private readonly IMongoCollection<StoreItem> _collection;

        public StoreItemRepository(IMongoDBContext dbContext)
        {
            _collection = dbContext.StoreItems;
        }

        public async Task<IEnumerable<StoreItem>> GetAll()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<StoreItem> GetById(string itemId)
        {
            return await _collection.Find(item => item.Id == itemId).FirstOrDefaultAsync();
        }

        public async Task Insert(StoreItem item)
        {
            await _collection.InsertOneAsync(item);
        }
    }
}
