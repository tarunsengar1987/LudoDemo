using LudoWebAPI.Models.DTO;

namespace LudoWebAPI.Interfaces
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        UserInfo GetCurrentUserInfo();
    }
}
