using DataLayer.Configuration;
using DataLayer.Models;
using DataLayer.Services;
using System.Windows;

namespace WPF
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private readonly SettingsService _settingsService = new SettingsService();
        public SettingsWindow()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            try
            {
                cmbChampionship.Items.Clear();
                cmbChampionship.Items.Add(AppConstants.MenChampionship);
                cmbChampionship.Items.Add(AppConstants.WomenChampionship);

                cmbDataSource.Items.Clear();
                cmbDataSource.Items.Add(AppConstants.JsonDataSource);
                cmbDataSource.Items.Add(AppConstants.ApiDataSource);

                cmbDisplayMode.Items.Clear();
                cmbDisplayMode.Items.Add(AppConstants.WindowedDisplayMode);
                cmbDisplayMode.Items.Add(AppConstants.FullscreenDisplayMode);

                cmbResolution.Items.Clear();

                foreach (string resolution in AppConstants.AllowedResolutions)
                {
                    cmbResolution.Items.Add(resolution);
                }

                AppSettings appSettings = _settingsService.LoadSettings();
                WpfSettings wpfSettings = _settingsService.LoadWpfSettings();

                cmbChampionship.SelectedItem = appSettings.Championship;
                cmbDataSource.SelectedItem = appSettings.DataSource;
                cmbDisplayMode.SelectedItem = wpfSettings.DisplayMode;
                cmbResolution.SelectedItem = wpfSettings.Resolution;

                if (cmbChampionship.SelectedIndex == -1)
                {
                    cmbChampionship.SelectedItem = AppConstants.MenChampionship;
                }

                if (cmbDataSource.SelectedIndex == -1)
                {
                    cmbDataSource.SelectedItem = AppConstants.JsonDataSource;
                }

                if (cmbDisplayMode.SelectedIndex == -1)
                {
                    cmbDisplayMode.SelectedItem = AppConstants.WindowedDisplayMode;
                }

                if (cmbResolution.SelectedIndex == -1)
                {
                    cmbResolution.SelectedItem = AppConstants.DefaultResolution;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load settings: {ex.Message}");

                cmbChampionship.SelectedItem = AppConstants.MenChampionship;
                cmbDataSource.SelectedItem = AppConstants.JsonDataSource;
                cmbDisplayMode.SelectedItem = AppConstants.WindowedDisplayMode;
                cmbResolution.SelectedItem = AppConstants.DefaultResolution;
            }
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmbChampionship.SelectedItem == null ||
                        cmbDataSource.SelectedItem == null ||
                        cmbDisplayMode.SelectedItem == null ||
                        cmbResolution.SelectedItem == null)
                {
                    MessageBox.Show("Please select all settings.");
                    return;
                }

                MessageBoxResult confirmResult = MessageBox.Show(
                    "Are you sure you want to save these settings?",
                    "Confirm settings",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question
                );

                if (confirmResult != MessageBoxResult.Yes)
                {
                    return;
                }

                AppSettings appSettings = new AppSettings
                {
                    Championship = cmbChampionship.SelectedItem.ToString()!,
                    DataSource = cmbDataSource.SelectedItem.ToString()!
                };

                WpfSettings wpfSettings = new WpfSettings
                {
                    DisplayMode = cmbDisplayMode.SelectedItem.ToString()!,
                    Resolution = cmbResolution.SelectedItem.ToString()!
                };

                _settingsService.SaveSettings(appSettings);
                _settingsService.SaveWpfSettings(wpfSettings);

                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not save settings: {ex.Message}");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
