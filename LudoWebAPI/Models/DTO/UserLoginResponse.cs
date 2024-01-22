namespace LudoWebAPI.Models.DTO
{
    public class UserLoginResponse
    {
        public string Token { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }

        public UserLoginResponse(string token, bool success, string message)
        {
            Token = token;
            Success = success;
            Message = message;
        }
    }
}
