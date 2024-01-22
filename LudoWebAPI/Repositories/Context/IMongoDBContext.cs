using LudoWebAPI.Models;
using MongoDB.Driver;

namespace LudoWebAPI.Repositories.Context
{
    public interface IMongoDBContext
    {
        IMongoCollection<User> Users { get; }
        IMongoCollection<StoreItem> StoreItems { get; }
    }
}
