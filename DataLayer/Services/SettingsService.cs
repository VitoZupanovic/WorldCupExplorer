using DataLayer.Configuration;
using DataLayer.Helpers;
using DataLayer.Models;
using System.Text.Json;

namespace DataLayer.Services
{
    public class SettingsService
    {
        private string AppSettingsFilePath
        {
            get
            {
                return Path.Combine(
                    GetSharedConfigFolder(),
                    AppConstants.AppSettingsFileName
                );
            }
        }

        private string WpfSettingsFilePath
        {
            get
            {
                return Path.Combine(
                    GetSharedConfigFolder(),
                    AppConstants.WpfSettingsFileName
                );
            }
        }

        public bool SettingsExist()
        {
            return File.Exists(AppSettingsFilePath);
        }
        private AppSettings ValidateSettings(AppSettings settings)
        {
            bool wasChanged = false;

            string normalizedChampionship = ChampionshipHelper.NormalizeChampionship(settings.Championship);

            if (settings.Championship != normalizedChampionship)
            {
                settings.Championship = normalizedChampionship;
                wasChanged = true;
            }

            string normalizedDataSource = DataSourceHelper.NormalizeDataSource(settings.DataSource);

            if (settings.DataSource != normalizedDataSource)
            {
                settings.DataSource = normalizedDataSource;
                wasChanged = true;
            }

            settings.Championship = settings.Championship.ToLower();
            settings.DataSource = settings.DataSource.ToLower();

            if (wasChanged)
            {
                SaveSettings(settings);
            }

            return settings;
        }
        public AppSettings LoadSettings()
        {
            if (!SettingsExist())
            {
                return new AppSettings();
            }

            try
            {
                string json = File.ReadAllText(AppSettingsFilePath);

                AppSettings? settings = JsonSerializer.Deserialize<AppSettings>(json);

                if (settings == null)
                {
                    return new AppSettings();
                }

                return ValidateSettings(settings);
            }
            catch
            {
                return new AppSettings();
            }
        }

        public void SaveSettings(AppSettings settings)
        {
            Directory.CreateDirectory(GetSharedConfigFolder());

            string json = JsonSerializer.Serialize(settings, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(AppSettingsFilePath, json);
        }
        private WpfSettings ValidateWpfSettings(WpfSettings settings)
        {
            bool wasChanged = false;

            if (!settings.DisplayMode.Equals(AppConstants.FullscreenDisplayMode, StringComparison.OrdinalIgnoreCase) &&
                !settings.DisplayMode.Equals(AppConstants.WindowedDisplayMode, StringComparison.OrdinalIgnoreCase))
            {
                settings.DisplayMode = AppConstants.WindowedDisplayMode;
                wasChanged = true;
            }

            if (!AppConstants.AllowedResolutions.Contains(settings.Resolution))
            {
                settings.Resolution = AppConstants.DefaultResolution;
                wasChanged = true;
            }

            settings.DisplayMode = settings.DisplayMode.ToLower();

            if (wasChanged)
            {
                SaveWpfSettings(settings);
            }

            return settings;
        }
        public bool WpfSettingsExist()
        {
            return File.Exists(WpfSettingsFilePath);
        }

        public WpfSettings LoadWpfSettings()
        {
            if (!WpfSettingsExist())
            {
                return new WpfSettings();
            }

            try
            {
                string json = File.ReadAllText(WpfSettingsFilePath);

                WpfSettings? settings = JsonSerializer.Deserialize<WpfSettings>(json);

                if (settings == null)
                {
                    return new WpfSettings();
                }

                return ValidateWpfSettings(settings);
            }
            catch
            {
                return new WpfSettings();
            }
        }

        public void SaveWpfSettings(WpfSettings settings)
        {
            Directory.CreateDirectory(GetSharedConfigFolder());

            string json = JsonSerializer.Serialize(settings, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(WpfSettingsFilePath, json);
        }

        private string GetSolutionRootFolder()
        {
            DirectoryInfo? directory = new DirectoryInfo(AppContext.BaseDirectory);

            while (directory != null && !directory.GetFiles("*.sln").Any())
            {
                directory = directory.Parent;
            }

            return directory?.FullName ?? AppContext.BaseDirectory;
        }
        private string GetSharedConfigFolder()
        {
            return Path.Combine(GetSolutionRootFolder(), AppConstants.ConfigFolder);
        }
    }
}
