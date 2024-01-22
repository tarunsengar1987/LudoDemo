using System.ComponentModel.DataAnnotations;

namespace LudoWebAPI.Models.Entity
{
    public class UserRegistrationRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }


        public IFormFile Avatar { get; set; }
    }
}
