using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using WorldCup.DataLayer.Models;

namespace WPF
{
    /// <summary>
    /// Interaction logic for PlayerDetailsWindow.xaml
    /// </summary>
    public partial class PlayerDetailsWindow : Window
    {
        public PlayerDetailsWindow(
            StartingEleven player,
            string imagePath,
            int goals,
            int yellowCards)
        {
            InitializeComponent();

            txtName.Text = player.Name;
            txtShirtNumber.Text = player.ShirtNumber.ToString();
            txtPosition.Text = player.Position.ToString();
            txtCaptain.Text = player.Captain ? "Yes" : "No";
            txtGoals.Text = goals.ToString();
            txtYellowCards.Text = yellowCards.ToString();

            LoadPlayerImage(imagePath);
        }
        private void LoadPlayerImage(string imagePath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(imagePath) || !File.Exists(imagePath))
                {
                    imgPlayer.Source = null;
                    return;
                }

                imgPlayer.Source = new BitmapImage(new Uri(Path.GetFullPath(imagePath)));
            }
            catch (Exception)
            {
                imgPlayer.Source = null;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
