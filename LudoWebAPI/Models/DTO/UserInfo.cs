namespace LudoWebAPI.Models.DTO
{
    public class UserInfo
    {
        public string UserId { get; set; }
        public string Username { get; set; }

        public UserInfo(string userId, string username) {
            UserId = userId;
            Username = username;
        }
    }
}
