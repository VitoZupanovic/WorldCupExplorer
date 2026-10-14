using DataLayer.Configuration;
using DataLayer.Helpers;
using DataLayer.Models;
using DataLayer.Services;
using System.Drawing.Printing;
using WinForms.Forms;

namespace WinForms
{
    public partial class WorldCupStats : Form
    {
        private readonly SettingsService _settingsService = new SettingsService();
        private readonly FavoritesService _favoritesService = new FavoritesService();
        private readonly WorldCupDataService _dataService = new WorldCupDataService();
        private readonly ImageService _imageService = new ImageService();
        private readonly RankingService _rankingService = new RankingService();

        private AppSettings _settings = new AppSettings();

        private List<PlayerDisplay> _players = new List<PlayerDisplay>();
        private string _currentRankingTitle = string.Empty;
        private int _printRowIndex = 0;
        private string _currentPlayersTeamCode = string.Empty;
        public WorldCupStats()
        {
            InitializeComponent();
            Load += Form1_Load;
        }
        private async void Form1_Load(object? sender, EventArgs e)
        {
            try
            {
                if (!_settingsService.SettingsExist())
                {
                    using (var settingsForm = new SettingsForm())
                    {
                        DialogResult result = settingsForm.ShowDialog();

                        if (result != DialogResult.OK)
                        {
                            Close();
                            return;
                        }
                    }
                }
                _settings = _settingsService.LoadSettings();

                lblCurrentSettings.Text =
                    $"Current settings: [Championship: {_settings.Championship} | Source: {_settings.DataSource}]";

                await LoadFavoriteTeamsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not start application: {ex.Message}");
                Close();
            }
        }
        private async Task LoadFavoriteTeamsAsync()
        {
            try
            {
                ShowLoading();
                cmbFavoriteTeam.Items.Clear();

                var teams = await _dataService.GetTeamDisplayNamesAsync(_settings.Championship);

                foreach (var team in teams)
                {
                    cmbFavoriteTeam.Items.Add(team);
                }

                string? savedFavoriteTeamCode = _favoritesService.LoadFavoriteTeam();

                if (!string.IsNullOrWhiteSpace(savedFavoriteTeamCode))
                {
                    string? savedTeamDisplay = teams.FirstOrDefault(team =>
                        TeamDisplayHelper.ExtractFifaCode(team)
                            .Equals(savedFavoriteTeamCode, StringComparison.OrdinalIgnoreCase));

                    if (savedTeamDisplay != null)
                    {
                        cmbFavoriteTeam.SelectedItem = savedTeamDisplay;
                    }
                }


                await LoadPlayersForFavoriteTeamAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load teams: {ex.Message}");
            }
            finally
            {
                HideLoading();
            }
        }

        private async void btnSaveFavorites_Click(object sender, EventArgs e)
        {
            if (cmbFavoriteTeam.SelectedItem == null)
            {
                MessageBox.Show("Please select a favorite team.");
                return;
            }

            string selectedTeam = cmbFavoriteTeam.SelectedItem.ToString()!;
            string fifaCode = TeamDisplayHelper.ExtractFifaCode(selectedTeam);

            if (string.IsNullOrWhiteSpace(fifaCode))
            {
                MessageBox.Show("Could not read FIFA code from selected team.");
                return;
            }

            try
            {
                ShowLoading();
                _favoritesService.SaveFavoriteTeam(fifaCode);

                if (!_currentPlayersTeamCode.Equals(fifaCode, StringComparison.OrdinalIgnoreCase))
                {
                    await LoadPlayersForFavoriteTeamAsync();
                }

                var favoritePlayers = _players
                .Where(player => player.IsFavorite)
                .Select(player => _favoritesService.CreateFavoritePlayer(fifaCode, player))
                .ToList();

                if (favoritePlayers.Count != 3)
                {
                    MessageBox.Show("Please select exactly 3 favorite players.");
                    return;
                }

                _favoritesService.SaveFavoritePlayers(fifaCode, favoritePlayers);

                MessageBox.Show($"Favorites saved for {selectedTeam}.\nFavorite players: {favoritePlayers.Count}/3");
                await LoadPlayersForFavoriteTeamAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not save favorites: {ex.Message}");
            }
            finally
            {
                HideLoading();
            }
        }
        private void DisplayPlayers()
        {
            pnlFavoritePlayers.Controls.Clear();
            pnlOtherPlayers.Controls.Clear();

            int favoriteY = 10;
            int otherY = 10;

            foreach (var player in _players)
            {
                var playerControl = new PlayerControl();
                playerControl.SetPlayer(player);
                string? favoriteTeamCode = _favoritesService.LoadFavoriteTeam();


                if (!string.IsNullOrWhiteSpace(favoriteTeamCode))
                {
                    string imagePath = _imageService.GetPlayerImageOrDefault(favoriteTeamCode, player.Name);
                    string defaultImagePath = Path.Combine(
                        AppConstants.ImagesFolder,
                        AppConstants.PlayersImagesFolder,
                        AppConstants.DefaultPlayerImageFileName
                    );

                    playerControl.SetPlayerImage(imagePath, defaultImagePath);
                }
                playerControl.Width = 300;
                playerControl.Height = 90;
                playerControl.ContextMenuStrip = CreatePlayerContextMenu(playerControl);
                playerControl.MouseDown += PlayerControl_MouseDown;
                playerControl.Click += PlayerControl_Click;

                if (player.IsFavorite)
                {
                    playerControl.Location = new Point(10, favoriteY);
                    pnlFavoritePlayers.Controls.Add(playerControl);
                    favoriteY += playerControl.Height + 10;
                }
                else
                {
                    playerControl.Location = new Point(10, otherY);
                    pnlOtherPlayers.Controls.Add(playerControl);
                    otherY += playerControl.Height + 10;
                }
            }
        }

        private async Task LoadPlayersForFavoriteTeamAsync()
        {
            try
            {
                if (cmbFavoriteTeam.SelectedItem == null)
                {
                    return;
                }

                string selectedTeam = cmbFavoriteTeam.SelectedItem.ToString()!;
                string fifaCode = TeamDisplayHelper.ExtractFifaCode(selectedTeam);

                if (string.IsNullOrWhiteSpace(fifaCode))
                {
                    return;
                }

                _players.Clear();
                pnlFavoritePlayers.Controls.Clear();
                pnlOtherPlayers.Controls.Clear();

                _players = await _dataService.GetPlayerDisplaysFromFirstMatchAsync(_settings.Championship, fifaCode);

                _currentPlayersTeamCode = fifaCode;

                var favoritePlayers = _favoritesService.LoadFavoritePlayers(fifaCode);

                foreach (var player in _players)
                {
                    player.IsFavorite = favoritePlayers.Any(favorite =>
                        favorite.ShirtNumber == player.ShirtNumber &&
                        favorite.Name.Equals(player.Name, StringComparison.OrdinalIgnoreCase));
                }

                DisplayPlayers();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load players: {ex.Message}");
            }
        }
        private ContextMenuStrip CreatePlayerContextMenu(PlayerControl playerControl)
        {
            var contextMenu = new ContextMenuStrip();

            if (playerControl.Player == null)
            {
                return contextMenu;
            }

            string menuText = playerControl.Player.IsFavorite
                ? "Remove from favorites"
                : "Add to favorites";

            var menuItem = new ToolStripMenuItem(menuText);

            menuItem.Click += (sender, e) =>
            {
                ToggleFavoritePlayer(playerControl.Player);
            };

            contextMenu.Items.Add(menuItem);

            var setImageItem = new ToolStripMenuItem("Set player image");

            setImageItem.Click += (sender, e) =>
            {
                SetPlayerImage(playerControl.Player);
            };

            contextMenu.Items.Add(setImageItem);

            return contextMenu;
        }
        private void ToggleFavoritePlayer(PlayerDisplay player)
        {
            int favoriteCount = _players.Count(p => p.IsFavorite);

            if (!player.IsFavorite && favoriteCount >= 3)
            {
                MessageBox.Show("You can select maximum 3 favorite players.");
                return;
            }

            player.IsFavorite = !player.IsFavorite;

            DisplayPlayers();
        }
        private void PlayerControl_MouseDown(object? sender, MouseEventArgs e)
        {
            if (sender is not PlayerControl playerControl || playerControl.Player == null)
            {
                return;
            }

            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            bool ctrlPressed = ModifierKeys.HasFlag(Keys.Control);

            if (ctrlPressed)
            {
                playerControl.Player.IsSelected = !playerControl.Player.IsSelected;
                DisplayPlayers();
                return;
            }

            DoDragDrop(playerControl.Player, DragDropEffects.Move);
        }

        private void pnlPlayers_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data != null && e.Data.GetDataPresent(typeof(PlayerDisplay)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void pnlFavoritePlayers_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data == null || !e.Data.GetDataPresent(typeof(PlayerDisplay)))
            {
                return;
            }

            var player = e.Data.GetData(typeof(PlayerDisplay)) as PlayerDisplay;

            if (player == null)
            {
                return;
            }

            int favoriteCount = _players.Count(p => p.IsFavorite);

            if (!player.IsFavorite && favoriteCount >= 3)
            {
                MessageBox.Show("You can select maximum 3 favorite players.");
                return;
            }

            player.IsFavorite = true;
            DisplayPlayers();
        }

        private void pnlOtherPlayers_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data == null || !e.Data.GetDataPresent(typeof(PlayerDisplay)))
            {
                return;
            }

            var player = e.Data.GetData(typeof(PlayerDisplay)) as PlayerDisplay;

            if (player == null)
            {
                return;
            }

            player.IsFavorite = false;
            DisplayPlayers();
        }
        private void PlayerControl_Click(object? sender, EventArgs e)
        {
            if (sender is not PlayerControl playerControl || playerControl.Player == null)
            {
                return;
            }

            playerControl.Player.IsSelected = !playerControl.Player.IsSelected;

            DisplayPlayers();
        }
        private void ClearPlayerSelections()
        {
            foreach (var player in _players)
            {
                player.IsSelected = false;
            }
        }

        private void btnAddSelectedToFavorites_Click(object sender, EventArgs e)
        {
            var selectedPlayers = _players
            .Where(player => player.IsSelected && !player.IsFavorite)
            .ToList();

            if (!selectedPlayers.Any())
            {
                MessageBox.Show("Please select players from Other players.");
                return;
            }

            int currentFavoriteCount = _players.Count(player => player.IsFavorite);

            if (currentFavoriteCount + selectedPlayers.Count > 3)
            {
                MessageBox.Show("You can select maximum 3 favorite players.");
                return;
            }

            foreach (var player in selectedPlayers)
            {
                player.IsFavorite = true;
            }

            ClearPlayerSelections();
            DisplayPlayers();
        }

        private void btnRemoveSelectedFromFavorites_Click(object sender, EventArgs e)
        {
            var selectedPlayers = _players
            .Where(player => player.IsSelected && player.IsFavorite)
            .ToList();

            if (!selectedPlayers.Any())
            {
                MessageBox.Show("Please select players from Favorite players.");
                return;
            }

            foreach (var player in selectedPlayers)
            {
                player.IsFavorite = false;
            }

            ClearPlayerSelections();
            DisplayPlayers();
        }
        private void SetPlayerImage(PlayerDisplay? player)
        {
            if (player == null)
            {
                return;
            }

            try
            {
                string? favoriteTeamCode = _favoritesService.LoadFavoriteTeam();

                if (string.IsNullOrWhiteSpace(favoriteTeamCode))
                {
                    MessageBox.Show("Please save a favorite team first.");
                    return;
                }

                using (var openFileDialog = new OpenFileDialog()
                {
                    Title = "Select player image",
                    Filter = "Image files|*.jpg;*.jpeg;*.png;*.bmp;"
                })
                {
                    if (openFileDialog.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                    if (!IsValidImageFile(openFileDialog.FileName))
                    {
                        MessageBox.Show("Selected file is not a valid image.");
                        return;
                    }
                    string copiedImagePath = _imageService.CopyPlayerImageToProjectFolder(
                        favoriteTeamCode,
                        player.Name,
                        openFileDialog.FileName
                    );

                    _imageService.SavePlayerImage(favoriteTeamCode, player.Name, copiedImagePath);

                }

                DisplayPlayers();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not save player image: {ex.Message}");
            }
        }
        private bool IsValidImageFile(string filePath)
        {
            try
            {
                using (Image image = Image.FromFile(filePath))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        private string GetSelectedFavoriteTeamCode()
        {
            if (cmbFavoriteTeam.SelectedItem == null)
            {
                return string.Empty;
            }

            string selectedTeam = cmbFavoriteTeam.SelectedItem.ToString()!;

            return TeamDisplayHelper.ExtractFifaCode(selectedTeam);
        }

        private async void btnGoalRanking_Click(object sender, EventArgs e)
        {
            string fifaCode = GetSelectedFavoriteTeamCode();

            if (string.IsNullOrWhiteSpace(fifaCode))
            {
                MessageBox.Show("Please select a favorite team.");
                return;
            }

            try
            {
                ShowLoading();
                var ranking = await _rankingService.GetGoalRankingAsync(_settings.Championship, fifaCode);

                dgvRankings.RowTemplate.Height = 55;

                _currentRankingTitle = "Goal ranking";

                dgvRankings.DataSource = ranking
                    .Select(player => new
                    {
                        Picture = GetRankingPlayerImage(fifaCode, player.PlayerName),
                        Player = player.PlayerName,
                        ShirtNumber = player.ShirtNumber,
                        Position = player.Position,
                        Goals = player.Count,
                        Appearances = player.Appearances
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load goal ranking: {ex.Message}");
            }
            finally
            {
                HideLoading();
            }
        }

        private async void btnYellowCardRanking_Click(object sender, EventArgs e)
        {
            string fifaCode = GetSelectedFavoriteTeamCode();

            if (string.IsNullOrWhiteSpace(fifaCode))
            {
                MessageBox.Show("Please select a favorite team.");
                return;
            }

            try
            {
                ShowLoading();
                var ranking = await _rankingService.GetYellowCardRankingAsync(_settings.Championship, fifaCode);

                dgvRankings.RowTemplate.Height = 55;

                _currentRankingTitle = "Yellow card ranking";

                dgvRankings.DataSource = ranking
                    .Select(player => new
                    {
                        Picture = GetRankingPlayerImage(fifaCode, player.PlayerName),
                        Player = player.PlayerName,
                        ShirtNumber = player.ShirtNumber,
                        Position = player.Position,
                        YellowCards = player.Count,
                        Appearances = player.Appearances
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load yellow card ranking: {ex.Message}");
            }
            finally
            {
                HideLoading();
            }
        }

        private async void btnAttendanceRanking_Click(object sender, EventArgs e)
        {
            string fifaCode = GetSelectedFavoriteTeamCode();

            if (string.IsNullOrWhiteSpace(fifaCode))
            {
                MessageBox.Show("Please select a favorite team.");
                return;
            }

            try
            {
                ShowLoading();
                var ranking = await _rankingService.GetAttendanceRankingAsync(_settings.Championship, fifaCode);

                dgvRankings.RowTemplate.Height = 25;

                _currentRankingTitle = "Attendance ranking";

                dgvRankings.DataSource = ranking
                    .Select(match => new
                    {
                        Location = match.Location,
                        Attendance = match.Attendance,
                        HomeTeam = match.HomeTeam,
                        AwayTeam = match.AwayTeam
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load attendance ranking: {ex.Message}");
            }
            finally
            {
                HideLoading();
            }
        }
        private Image? GetRankingPlayerImage(string fifaCode, string playerName)
        {
            try
            {
                string imagePath = _imageService.GetPlayerImageOrDefault(fifaCode, playerName);

                if (string.IsNullOrWhiteSpace(imagePath) || !File.Exists(imagePath))
                {
                    return null;
                }

                using (Image originalImage = Image.FromFile(imagePath))
                {
                    return new Bitmap(originalImage, new Size(50, 50));
                }
            }
            catch
            {
                return null;
            }
        }

        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            if (e.Graphics == null)
            {
                return;
            }

            int left = e.MarginBounds.Left;
            int top = e.MarginBounds.Top;
            int pageWidth = e.MarginBounds.Width;
            int y = top;

            using (Font titleFont = new Font("Arial", 16, FontStyle.Bold))
            using (Font infoFont = new Font("Arial", 10))
            using (Font headerFont = new Font("Arial", 9, FontStyle.Bold))
            using (Font rowFont = new Font("Arial", 8))
            using (Pen pen = new Pen(Color.Black))
            {
                e.Graphics.DrawString(_currentRankingTitle, titleFont, Brushes.Black, left, y);
                y += 35;

                e.Graphics.DrawString(
                    $"Championship: {_settings.Championship} | Source: {_settings.DataSource}",
                    infoFont,
                    Brushes.Black,
                    left,
                    y
                );

                y += 30;

                int columnCount = dgvRankings.Columns.Count;
                int imageColumnWidth = dgvRankings.Columns.Contains("Picture") ? 70 : 0;
                int normalColumnWidth = (pageWidth - imageColumnWidth) / Math.Max(1, columnCount - (imageColumnWidth > 0 ? 1 : 0));

                int headerHeight = 30;
                int rowHeight = dgvRankings.Columns.Contains("Picture") ? 60 : 30;

                int x = left;

                foreach (DataGridViewColumn column in dgvRankings.Columns)
                {
                    int columnWidth = column.Name == "Picture" ? imageColumnWidth : normalColumnWidth;

                    e.Graphics.DrawRectangle(pen, x, y, columnWidth, headerHeight);
                    e.Graphics.DrawString(column.HeaderText, headerFont, Brushes.Black, x + 4, y + 7);

                    x += columnWidth;
                }

                y += headerHeight;

                while (_printRowIndex < dgvRankings.Rows.Count)
                {
                    DataGridViewRow row = dgvRankings.Rows[_printRowIndex];

                    if (row.IsNewRow)
                    {
                        _printRowIndex++;
                        continue;
                    }

                    if (y + rowHeight > e.MarginBounds.Bottom)
                    {
                        e.HasMorePages = true;
                        return;
                    }

                    x = left;

                    foreach (DataGridViewColumn column in dgvRankings.Columns)
                    {
                        int columnWidth = column.Name == "Picture" ? imageColumnWidth : normalColumnWidth;

                        e.Graphics.DrawRectangle(pen, x, y, columnWidth, rowHeight);

                        object? value = row.Cells[column.Index].Value;

                        if (column.Name == "Picture" && value is Image image)
                        {
                            Rectangle imageRectangle = new Rectangle(x + 10, y + 5, 50, 50);
                            e.Graphics.DrawImage(image, imageRectangle);
                        }
                        else
                        {
                            string text = value?.ToString() ?? string.Empty;

                            RectangleF textRectangle = new RectangleF(
                                x + 4,
                                y + 7,
                                columnWidth - 8,
                                rowHeight - 10
                            );

                            e.Graphics.DrawString(text, rowFont, Brushes.Black, textRectangle);
                        }

                        x += columnWidth;
                    }

                    y += rowHeight;
                    _printRowIndex++;
                }

                e.HasMorePages = false;
                _printRowIndex = 0;
            }
        }

        private void printDocument_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (e.PrintAction == PrintAction.PrintToPrinter)
            {
                MessageBox.Show("Printing finished.");
            }
        }

        private void btnPageSetup_Click(object sender, EventArgs e)
        {
            try
            {
                pageSetupDialog.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not open page setup: {ex.Message}");
            }
        }

        private void btnPreviewRanking_Click(object sender, EventArgs e)
        {
            if (dgvRankings.Rows.Count == 0)
            {
                MessageBox.Show("Please load a ranking first.");
                return;
            }

            if (string.IsNullOrWhiteSpace(_currentRankingTitle))
            {
                MessageBox.Show("Please select a ranking first.");
                return;
            }

            try
            {
                _printRowIndex = 0;
                printPreviewDialog.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not preview ranking: {ex.Message}");
            }
        }

        private void btnPrintRanking_Click(object sender, EventArgs e)
        {
            if (dgvRankings.Rows.Count == 0)
            {
                MessageBox.Show("Please load a ranking first.");
                return;
            }

            if (string.IsNullOrWhiteSpace(_currentRankingTitle))
            {
                MessageBox.Show("Please select a ranking first.");
                return;
            }

            try
            {
                _printRowIndex = 0;

                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    printDocument.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not print ranking: {ex.Message}");
            }
        }

        private void WorldCupStats_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (ConfirmForm confirmForm = new ConfirmForm(
                "Are you sure you want to close the application?",
                "Confirm exit"))
            {
                DialogResult result = confirmForm.ShowDialog(this);

                if (result != DialogResult.OK)
                {
                    e.Cancel = true;
                }
            }
        }

        private async void btnSettings_Click(object sender, EventArgs e)
        {
            try
            {
                using (var settingsForm = new SettingsForm())
                {
                    DialogResult result = settingsForm.ShowDialog();

                    if (result != DialogResult.OK)
                    {
                        return;
                    }
                }

                _settings = _settingsService.LoadSettings();

                lblCurrentSettings.Text =
                    $"Current settings: [Championship: {_settings.Championship} | Source: {_settings.DataSource}]";

                _players.Clear();
                pnlFavoritePlayers.Controls.Clear();
                pnlOtherPlayers.Controls.Clear();

                dgvRankings.DataSource = null;
                _currentRankingTitle = string.Empty;

                await LoadFavoriteTeamsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not apply settings: {ex.Message}");
            }
        }
        private void ShowLoading()
        {
            lblLoading.Visible = true;
            Cursor = Cursors.WaitCursor;
        }

        private void HideLoading()
        {
            lblLoading.Visible = false;
            Cursor = Cursors.Default;
        }

        private void btnClearRanking_Click(object sender, EventArgs e)
        {
            dgvRankings.DataSource = null;
            _currentRankingTitle = string.Empty;
        }
    }
}
