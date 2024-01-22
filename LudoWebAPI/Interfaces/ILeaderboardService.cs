using LudoWebAPI.Models;
using LudoWebAPI.Models.DTO;

namespace LudoWebAPI.Interfaces
{
    public interface ILeaderboardService
    {
        Task<IEnumerable<LeaderboardItem>> GetLeaderboard();
    }
}
