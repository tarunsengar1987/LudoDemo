namespace LudoWebAPI.Models
{
    public class UserRegistrationRequest
    {
        public string Username { get; set; }
        public string Avatar { get; set; }
        public string Password { get; internal set; }
    }
}
