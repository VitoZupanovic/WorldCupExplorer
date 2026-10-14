namespace DataLayer.Models
{
    public class AttendanceRankingItem
    {
        public string Location { get; set; } = string.Empty;
        public string HomeTeam { get; set; } = string.Empty;
        public string AwayTeam { get; set; } = string.Empty;
        public long Attendance { get; set; }
    }
}
