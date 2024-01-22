using System.ComponentModel.DataAnnotations;

namespace LudoWebAPI.Models.Entity
{
    public class UpdateScoreRequest
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public int Score { get; set; }
    }
}
