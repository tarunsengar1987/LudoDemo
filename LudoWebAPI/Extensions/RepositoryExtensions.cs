using LudoWebAPI.Interfaces;
using LudoWebAPI.Repositories;

namespace LudoWebAPI.Extensions
{
    public static class RepositoryExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IStoreRepository, StoreItemRepository>();
        }
    }
}