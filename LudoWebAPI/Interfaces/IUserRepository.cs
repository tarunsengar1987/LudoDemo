using LudoWebAPI.Models;

namespace LudoWebAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(string userId);
        Task<User> GetUserByUsername(string username);
        Task<bool> IsUsernameExists(string username);
        Task<bool> IsEmailExists(string email);
        Task Insert(User user);
        Task UpdateUser(User user);
        Task AddCoins(string userId, int amount);
    }
}
