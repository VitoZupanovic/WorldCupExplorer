using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WorldCup.DataLayer.Models;

namespace WPF
{
    /// <summary>
    /// Interaction logic for PlayerFieldControl.xaml
    /// </summary>
    public partial class PlayerFieldControl : UserControl
    {
        public StartingEleven? Player { get; private set; }
        public PlayerFieldControl()
        {
            InitializeComponent();
        }

        public void SetPlayer(StartingEleven player, string imagePath)
        {
            Player = player;

            txtPlayer.Text = $"#{player.ShirtNumber} {GetShortPlayerName(player.Name)}";
            txtPlayer.ToolTip = player.Name;

            try
            {
                if (!string.IsNullOrWhiteSpace(imagePath) && File.Exists(imagePath))
                {
                    imgPlayer.Source = new BitmapImage(new Uri(System.IO.Path.GetFullPath(imagePath)));
                }
                else
                {
                    imgPlayer.Source = null;
                }
            }
            catch (Exception)
            {
                imgPlayer.Source = null;
            }
        }
        private string GetShortPlayerName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                return string.Empty;
            }

            string[] parts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 0)
            {
                return fullName;
            }

            return parts[parts.Length - 1];
        }
    }
}
