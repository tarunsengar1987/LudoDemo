using System.Security.Claims;

namespace LudoWebAPI.Middleware
{
    public class CustomAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    var customClaim = new Claim("UserId", userIdClaim.Value);
                    ((ClaimsIdentity)context.User.Identity).AddClaim(customClaim);
                }

                var usernameClaim = context.User.FindFirst(ClaimTypes.Name);

                if (usernameClaim != null)
                {
                    var customClaim = new Claim("Username", usernameClaim.Value);
                    ((ClaimsIdentity)context.User.Identity).AddClaim(customClaim);
                }
            }

            await _next(context);
        }
    }

    public static class AuthorizationMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomAuthorizationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomAuthorizationMiddleware>();
        }
    }
}
