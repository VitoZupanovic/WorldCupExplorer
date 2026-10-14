namespace DataLayer.Models
{
    public class PlayerRankingItem
    {
        public string PlayerName { get; set; } = string.Empty;
        public long ShirtNumber { get; set; }
        public string Position { get; set; } = string.Empty;
        public int Count { get; set; }
        public int Appearances { get; set; }
    }
}
