using System.ComponentModel.DataAnnotations;

namespace LudoWebAPI.Models.DTO
{
    public class StoreAddItemRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
