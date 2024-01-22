using LudoWebAPI.Models;

namespace LudoWebAPI.Interfaces
{
    public interface IStoreService
    {
        Task<IEnumerable<StoreItem>> GetAvailableItems();
        Task<IEnumerable<StoreItem>> GetAllStoreItems();
        Task<StoreItem> GetStoreItemById(string id);
        Task InsertStoreItem(StoreItem storeItem);

        Task BuyDice(User user, string diceId);
        Task BuyBoard(User user, string boardId);
        Task BuyCostume(User user, string costumeId);
    }
}
