using LudoWebAPI.Interfaces;
using LudoWebAPI.Models;
using LudoWebAPI.Models.DTO;
using LudoWebAPI.Models.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LudoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;
        private readonly IUserService _userService;

        public StoreController(IStoreService storeService, IUserService userService)
        {
            _storeService = storeService;
            _userService = userService;
        }

        [HttpGet("items")]
        public async Task<IActionResult> GetStoreItems()
        {
            var storeItems = await _storeService.GetAvailableItems();
            return Ok(storeItems);
        }

        [HttpGet("item/{id}")]
        public async Task<IActionResult> GetItemById(string id)
        {
            var item = await _storeService.GetStoreItemById(id);

            if (item == null)
            {
                return NotFound("Item not found");
            }

            return Ok(item);
        }

        [HttpPost("add-item")]
        public async Task<IActionResult> AddItem([FromBody] StoreAddItemRequest request)
        {
            var newItem = new StoreItem
            {
                Id = Guid.NewGuid().ToString(),
                Name = request.Name,
                Price = request.Price
            };

            await _storeService.InsertStoreItem(newItem);
            return Ok("Item added successfully");
        }

        [HttpPost("buy-dice")]
        public async Task<IActionResult> BuyDice([FromBody] PurchaseRequest request)
        {
            var user = await _userService.GetUserById(request.UserId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            await _storeService.BuyDice(user, request.ItemId);
            return Ok($"Dice purchased successfully by {user.Username}");
        }

        [HttpPost("buy-board")]
        public async Task<IActionResult> BuyBoard([FromBody] PurchaseRequest request)
        {
            var user = await _userService.GetUserById(request.UserId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            await _storeService.BuyBoard(user, request.ItemId);
            return Ok($"Board purchased successfully by {user.Username}");
        }

        [HttpPost("buy-costume")]
        public async Task<IActionResult> BuyCostume([FromBody] PurchaseRequest request)
        {
            var user = await _userService.GetUserById(request.UserId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            await _storeService.BuyCostume(user, request.ItemId);
            return Ok($"Costume purchased successfully by {user.Username}");
        }
    }
}