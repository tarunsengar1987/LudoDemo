using LudoWebAPI.Interfaces;
using LudoWebAPI.Models.DTO;

namespace LudoWebAPI.Services
{
    public class LeaderboardService : ILeaderboardService
    {
        private readonly IUserRepository _userRepository;

        public LeaderboardService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public async Task<IEnumerable<LeaderboardItem>> GetLeaderboard()
        {
            var users = await _userRepository.GetAll();
            var leaderboard = users
                .OrderByDescending(u => u.Coins)
                .Select((user, index) => new LeaderboardItem
                {
                    UserId = user.Id,
                    Playername = user.Name,
                    Rank = index + 1,
                    CoinBalance = user.Coins
                })
                .ToList();

            return leaderboard;
        }
    }
}
