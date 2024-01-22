using LudoWebAPI.Models;
using LudoWebAPI.Models.DTO;
using LudoWebAPI.Models.Entity;

namespace LudoWebAPI.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(string userId);
        Task<User> GetUserByUsername(string username);
        Task<bool> IsUsernameExists(string username);
        Task<bool> IsEmailExists(string email);
        Task RegisterUser(User user, IFormFile avatar);
        Task<UserLoginResponse> LoginUser(LoginRequest user);
        Task AddCoins(string userId, int amount);
    }
}
