using LudoWebAPI.Interfaces;
using LudoWebAPI.Models.DTO;
using System.Security.Claims;

namespace LudoWebAPI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserId
        {
            get
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
                return userIdClaim?.Value;
            }
        }

        public UserInfo GetCurrentUserInfo()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
            var usernameClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name);
            

            if (userIdClaim != null && usernameClaim != null )
            {
                string userId = userIdClaim.Value;
                string username = usernameClaim.Value;

                return new UserInfo(userId, username);
            }

            return null;
        }
    }
}
