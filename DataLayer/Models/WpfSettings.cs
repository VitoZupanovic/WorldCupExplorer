using DataLayer.Configuration;

namespace DataLayer.Models
{
    public class WpfSettings
    {
        public string DisplayMode { get; set; } = AppConstants.WindowedDisplayMode;
        public string Resolution { get; set; } = AppConstants.DefaultResolution;
    }
}
