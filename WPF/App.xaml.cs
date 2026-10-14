using DataLayer.Services;
using System.Windows;

namespace WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly SettingsService _settingsService = new SettingsService();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                ShutdownMode = ShutdownMode.OnExplicitShutdown;

                if (!_settingsService.WpfSettingsExist())
                {
                    SettingsWindow settingsWindow = new SettingsWindow();

                    bool? result = settingsWindow.ShowDialog();

                    if (result != true)
                    {
                        Shutdown();
                        return;
                    }
                }

                MainWindow mainWindow = new MainWindow();

                MainWindow = mainWindow;
                ShutdownMode = ShutdownMode.OnMainWindowClose;

                mainWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not start application: {ex.Message}");
                Shutdown();
            }
        }
    }

}
