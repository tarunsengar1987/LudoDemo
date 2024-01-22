using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace LudoWebAPI.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        //public string AvatarId { get; set; }
        public string AvatarFileName { get; set; }
        public decimal Coins { get; set; }
        public decimal Score { get; set; }

        public List<StoreItem> Inventory { get; set; } = new List<StoreItem>();

        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    }
}
