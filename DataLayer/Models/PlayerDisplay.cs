namespace DataLayer.Models
{
    public class PlayerDisplay
    {
        public string Name { get; set; } = string.Empty;
        public long ShirtNumber { get; set; }
        public string Position { get; set; } = string.Empty;
        public bool Captain { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsSelected { get; set; }

        public override string ToString()
        {
            return $"{ShirtNumber} - {Name} ({Position})";
        }
    }
}
