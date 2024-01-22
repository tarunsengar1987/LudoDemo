using Amazon.Runtime.Internal;
using LudoWebAPI.Interfaces;
using LudoWebAPI.Models;
using LudoWebAPI.Models.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace LudoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IHubContext<LudoChatHub> _ludoHubContext;
        public UserController(IUserService userService, IHubContext<LudoChatHub> ludoHubContext)
        {
            _userService = userService;
            _ludoHubContext = ludoHubContext;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult> GetUserById(string id)
        {
            var user = await _userService.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromForm] UserRegistrationRequest request)
        {

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Name = request.Name,
                Email = request.Email,
                Username = request.Username,
                Password = request.Password
            };
            await _userService.RegisterUser(user, request.Avatar);
            return Ok("user created");
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            return Ok(await _userService.LoginUser(request));
        }

        [HttpPost("{userId}/addCoins")]
        [Authorize]
        public async Task<IActionResult> AddCoins(string userId, [FromForm] int amount)
        {
            await _userService.AddCoins(userId, amount);
            return Ok($"Added {amount} coins to user {userId}");
        }

        [HttpGet("{userId}/balance")]
        [Authorize]
        public async Task<IActionResult> GetCoinBalance(string userId)
        {
            var user = await _userService.GetUserById(userId);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(new { UserId = userId, CoinBalance = user.Coins });
        }

        [HttpPost("sendmessage")]
        public async Task<IActionResult> SendMessage(string userId, string message)
        {
            await _ludoHubContext.Clients.User(userId).SendAsync("ReceiveMessage", message);
            return Ok();
        }
    }
}