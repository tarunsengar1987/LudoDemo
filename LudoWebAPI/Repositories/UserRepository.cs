using LudoWebAPI.Interfaces;
using LudoWebAPI.Models;
using LudoWebAPI.Repositories.Context;
using MongoDB.Driver;

namespace LudoWebAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _collection;

        public UserRepository(IMongoDBContext dbContext)
        {
            _collection = dbContext.Users;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<User> GetById(string userId)
        {
            return await _collection.Find(u => u.Id == userId).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await _collection.Find(u => u.Username == username).FirstOrDefaultAsync();
        }

        public async Task Insert(User user)
        {
            await _collection.InsertOneAsync(user);
        }

        public async Task UpdateUser(User user)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, user.Id);
            var update = Builders<User>.Update
                .Set(u => u.Email, user.Email)
                .Set(u => u.Name, user.Name)
                .Set(u => u.AvatarFileName, user.AvatarFileName)
                .Set(u => u.Coins, user.Coins)
                .Set(u => u.Score, user.Score)
                .Set(u => u.Inventory, user.Inventory)
                .CurrentDate(u => u.LastUpdated);

            await _collection.UpdateOneAsync(filter, update);
        }

        public async Task AddCoins(string userId, int amount)
        {
            User user = await GetById(userId);
            if (user != null)
            {
                user.Coins += amount;

                var filter = Builders<User>.Filter.Eq(u => u.Id, user.Id);
                var update = Builders<User>.Update
                    .Set(u => u.Coins, user.Coins)
                    .CurrentDate(u => u.LastUpdated);

                await _collection.UpdateOneAsync(filter, update);
            }
        }

        public async Task UpdateScore(string userId, decimal newScore)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, userId);
            var update = Builders<User>.Update.Set(u => u.Score, newScore);

           await _collection.UpdateOneAsync(filter, update);
        }

        public async Task<bool> IsUsernameExists(string username)
        {
            return await _collection.Find(u => u.Username == username).AnyAsync();
        }

        public async Task<bool> IsEmailExists(string email)
        {
            return await _collection.Find(u => u.Email == email).AnyAsync();
        }
    }
}
