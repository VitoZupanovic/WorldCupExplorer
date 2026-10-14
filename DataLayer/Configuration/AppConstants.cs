namespace DataLayer.Configuration
{
    public static class AppConstants
    {
        public const string MenChampionship = "men";
        public const string WomenChampionship = "women";

        public const string JsonDataSource = "json";
        public const string ApiDataSource = "api";

        public const string FullscreenDisplayMode = "fullscreen";
        public const string WindowedDisplayMode = "windowed";

        public const string DefaultResolution = "1280x720";

        public static readonly List<string> AllowedResolutions = new()
        {
            "1280x720",
            "1600x900",
            "1920x1080"
        };
        public const string ConfigFolder = "Config";

        public const string AppSettingsFileName = "app_settings.txt";
        public const string WpfSettingsFileName = "wpf_settings.txt";
        public const string FavoriteTeamFileName = "favorite_team.txt";
        public const string FavoritePlayersFileName = "favorite_players.txt";
        public const string PlayerImagesFileName = "player_images.txt";

        public const string ImagesFolder = "Images";
        public const string PlayersImagesFolder = "Players";
        public const string DefaultPlayerImageFileName = "default-player.png";

        public const string DataFolder = "Data";
        public const string MatchesFileName = "matches.json";
        public const string ResultsFileName = "results.json";

        public const string TeamsFileName = "teams.json";
        public const string GroupResultsFileName = "group_results.json";
    }
}
