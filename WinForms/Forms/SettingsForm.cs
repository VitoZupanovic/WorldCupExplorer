using DataLayer.Configuration;
using DataLayer.Models;
using DataLayer.Services;
using WinForms.Forms;

namespace WinForms
{
    public partial class SettingsForm : Form
    {
        private readonly SettingsService _settingsService = new SettingsService();
        public SettingsForm()
        {
            InitializeComponent();
            Load += SettingsForm_Load;
        }
        private void SettingsForm_Load(object? sender, EventArgs e)
        {
            try
            {
                cmbChampionship.Items.Clear();
                cmbChampionship.Items.Add(AppConstants.MenChampionship);
                cmbChampionship.Items.Add(AppConstants.WomenChampionship);

                cmbDataSource.Items.Clear();
                cmbDataSource.Items.Add(AppConstants.JsonDataSource);
                cmbDataSource.Items.Add(AppConstants.ApiDataSource);

                AppSettings settings = _settingsService.LoadSettings();

                cmbChampionship.SelectedItem = settings.Championship;
                cmbDataSource.SelectedItem = settings.DataSource;

                if (cmbChampionship.SelectedIndex == -1)
                {
                    cmbChampionship.SelectedItem = AppConstants.MenChampionship;
                }

                if (cmbDataSource.SelectedIndex == -1)
                {
                    cmbDataSource.SelectedItem = AppConstants.JsonDataSource;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load settings: {ex.Message}");

                cmbChampionship.SelectedItem = AppConstants.MenChampionship;
                cmbDataSource.SelectedItem = AppConstants.JsonDataSource;
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbChampionship.SelectedItem == null || cmbDataSource.SelectedItem == null)
                {
                    MessageBox.Show("Please select championship and data source.");
                    return;
                }

                var settings = new AppSettings
                {
                    Championship = cmbChampionship.SelectedItem.ToString()!,
                    DataSource = cmbDataSource.SelectedItem.ToString()!
                };

                using (ConfirmForm confirmForm = new ConfirmForm(
                    "Are you sure you want to save these settings?",
                    "Confirm settings"))
                {
                    DialogResult result = confirmForm.ShowDialog(this);

                    if (result != DialogResult.OK)
                    {
                        return;
                    }
                }

                _settingsService.SaveSettings(settings);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not save settings: {ex.Message}");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
