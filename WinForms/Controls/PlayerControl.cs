using DataLayer.Models;

namespace WinForms
{
    public partial class PlayerControl : UserControl
    {
        public PlayerDisplay? Player { get; private set; }
        public PlayerControl()
        {
            InitializeComponent();
            foreach (Control control in Controls)
            {
                control.MouseDown += ChildControl_MouseDown;
            }
        }

        private void ChildControl_MouseDown(object? sender, MouseEventArgs e)
        {
            OnMouseDown(e);
        }

        public void SetPlayer(PlayerDisplay player)
        {
            Player = player;

            lblNumber.Text = $"#{player.ShirtNumber}";
            lblName.Text = player.Name;
            lblPosition.Text = player.Position;
            lblCaptain.Text = player.Captain ? "Captain: Yes" : "Captain: No";
            lblFavorite.Visible = player.IsFavorite;
            BackColor = player.IsSelected ? Color.LightBlue : SystemColors.Control;
        }
        public void SetPlayerImage(string imagePath, string defaultImagePath)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(imagePath) && File.Exists(imagePath))
                {
                    picPlayer.ImageLocation = imagePath;
                }
                else if (!string.IsNullOrWhiteSpace(defaultImagePath) && File.Exists(defaultImagePath))
                {
                    picPlayer.ImageLocation = defaultImagePath;
                }
                else
                {
                    picPlayer.ImageLocation = null;
                }
            }
            catch (Exception)
            {
                picPlayer.ImageLocation = null;
            }
        }
    }
}
