using DataLayer.Configuration;

namespace DataLayer.Models
{
    public class AppSettings
    {
        public string Championship { get; set; } = AppConstants.MenChampionship;
        public string DataSource { get; set; } = AppConstants.JsonDataSource;
    }
}
