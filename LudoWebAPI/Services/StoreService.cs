using LudoWebAPI.Interfaces;
using LudoWebAPI.Models;
using LudoWebAPI.Repositories;

namespace LudoWebAPI.Services
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeItemRepository;
        private readonly IUserRepository _userRepository;


        public StoreService(IStoreRepository storeItemRepository, IUserRepository userRepository)
        {
            _storeItemRepository = storeItemRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<StoreItem>> GetAvailableItems()
        {
            return await _storeItemRepository.GetAll();
        }

        public async Task<IEnumerable<StoreItem>> GetAllStoreItems()
        {
            return await _storeItemRepository.GetAll();
        }

        public async Task<StoreItem> GetStoreItemById(string id)
        {
            return await _storeItemRepository.GetById(id);
        }

        public async Task InsertStoreItem(StoreItem storeItem)
        {
            await _storeItemRepository.Insert(storeItem);
        }

        public async Task BuyDice(User user, string diceId)
        {
            var dice = await _storeItemRepository.GetById(diceId);

            if (dice == null || dice.Name != "Dice")
            {
                throw new InvalidOperationException("Invalid dice item");
            }

            if (user.Coins < dice.Price)
            {
                throw new InvalidOperationException("Insufficient coins to buy the dice");
            }

            user.Coins -= dice.Price;

            user.Inventory.Add(dice);

            await _userRepository.UpdateUser(user);
        }

        public async Task BuyBoard(User user, string boardId)
        {
            var board = await _storeItemRepository.GetById(boardId);

            if (board == null || board.Name != "Board")
            {
                throw new InvalidOperationException("Invalid board item");
            }

            if (user.Coins < board.Price)
            {
                throw new InvalidOperationException("Insufficient coins to buy the board");
            }

            user.Coins -= board.Price;

            user.Inventory.Add(board);

            await _userRepository.UpdateUser(user);
        }

        public async Task BuyCostume(User user, string costumeId)
        {
            var costume = await _storeItemRepository.GetById(costumeId);

            if (costume == null || costume.Name != "Costume")
            {
                throw new InvalidOperationException("Invalid costume item");
            }

            if (user.Coins < costume.Price)
            {
                throw new InvalidOperationException("Insufficient coins to buy the costume");
            }

            user.Coins -= costume.Price;


            user.Inventory.Add(costume);

            
            await _userRepository.UpdateUser(user);
        }
    }
}
