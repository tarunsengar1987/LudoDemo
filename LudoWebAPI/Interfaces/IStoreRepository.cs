using LudoWebAPI.Models;

namespace LudoWebAPI.Interfaces
{
    public interface IStoreRepository
    {
        Task<IEnumerable<StoreItem>> GetAll();
        Task<StoreItem> GetById(string itemId);
        Task Insert(StoreItem item);
    }
}
