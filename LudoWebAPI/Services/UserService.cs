using Amazon.Runtime.Internal;
using LudoWebAPI.Interfaces;
using LudoWebAPI.Models;
using LudoWebAPI.Models.DTO;
using LudoWebAPI.Models.Entity;

namespace LudoWebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly ICurrentUserService _currentUser;


        public UserService(IUserRepository userRepository, IJwtService jwtService, ICurrentUserService currentUser)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _currentUser = currentUser;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = await _userRepository.GetAll();

            if (users is null)
                return new List<User>();
            else
                return users.ToList();
        }

        public async Task<User> GetUserById(string userId) => await _userRepository.GetById(userId);

        public async Task<bool> IsUsernameExists(string username)
        {
            return await _userRepository.IsUsernameExists(username);
        }

        public async Task<bool> IsEmailExists(string email)
        {
            return await _userRepository.IsEmailExists(email);
        }

        public async Task RegisterUser(User user, IFormFile avatar)
        {
            if (await IsUsernameExists(user.Username))
            {
                throw new InvalidOperationException("Username already exists");
            }

            if (await IsEmailExists(user.Email))
            {
                throw new InvalidOperationException("Email already exists");
            }

            if (avatar != null && avatar.Length > 0)
            {
                var avatarFileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(avatar.FileName)}";
                var avatarPath = Path.Combine("Avatars", avatarFileName);

                using (var stream = new FileStream(avatarPath, FileMode.Create))
                {
                    avatar.CopyTo(stream);
                }

                user.AvatarFileName = avatarFileName;
            }

            await _userRepository.Insert(user);
        }

        public async Task<UserLoginResponse> LoginUser(LoginRequest request)
        {
            var user = await _userRepository.GetUserByUsername(request.Username);

            if (user != null && user.Password == request.Password)
            {
                var token = _jwtService.GenerateToken(user.Id, user.Username);
                return new UserLoginResponse(token, true, "Login successful");
            }

            return new UserLoginResponse("", false, "Invalid credentials");
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await _userRepository.GetUserByUsername(username);
        }

        public async Task AddCoins(string userId, int amount)
        {
            if (userId == _currentUser.UserId)
                await _userRepository.AddCoins(_currentUser.UserId, amount);
        }

    }
}
