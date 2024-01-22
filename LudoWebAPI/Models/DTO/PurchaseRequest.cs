using System.ComponentModel.DataAnnotations;

namespace LudoWebAPI.Models.Entity
{
    public class PurchaseRequest
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string ItemId { get; set; }
    }
}
