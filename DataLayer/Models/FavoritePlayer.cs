namespace DataLayer.Models
{
    public class FavoritePlayer
    {
        public string FifaCode { get; set; } = string.Empty;
        public long ShirtNumber { get; set; }
        public string Name { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{ShirtNumber} - {Name}";
        }
    }
}
