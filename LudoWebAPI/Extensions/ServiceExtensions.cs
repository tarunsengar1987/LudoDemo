using LudoWebAPI.Interfaces;
using LudoWebAPI.Services;

namespace LudoWebAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IStoreService, StoreService>();
            services.AddTransient<ILeaderboardService, LeaderboardService>();
            services.AddTransient<IJwtService, JwtService>();
            services.AddTransient<ICurrentUserService, CurrentUserService>();
        }
    }
}