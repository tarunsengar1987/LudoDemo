namespace LudoWebAPI.Models.DTO
{
    public class LeaderboardItem
    {
        public string UserId { get; set; }
        public string Playername { get; set; }
        public int Rank { get; set; }
        public decimal CoinBalance { get; set; }
    }
}
