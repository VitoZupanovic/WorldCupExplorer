using DataLayer.Configuration;
using DataLayer.Helpers;
using DataLayer.Models;
using DataLayer.Services;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WorldCup.DataLayer.Models;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly SettingsService _settingsService = new SettingsService();
        private readonly FavoritesService _favoritesService = new FavoritesService();
        private readonly WorldCupDataService _dataService = new WorldCupDataService();
        private readonly ImageService _imageService = new ImageService();

        private AppSettings _appSettings = new AppSettings();
        private bool _isLoading = false;

        private Match? _selectedMatch;
        public MainWindow()
        {
            InitializeComponent();
            ApplyWpfSettings();
            Loaded += MainWindow_Loaded;
        }
        private void ApplyWpfSettings()
        {
            WpfSettings settings = _settingsService.LoadWpfSettings();

            if (settings.DisplayMode == AppConstants.FullscreenDisplayMode)
            {
                WindowStyle = WindowStyle.SingleBorderWindow;
                WindowState = WindowState.Maximized;
            }
            else
            {
                WindowStyle = WindowStyle.SingleBorderWindow;
                WindowState = WindowState.Normal;

                string[] resolutionParts = settings.Resolution.Split('x');

                if (resolutionParts.Length == 2 &&
                    double.TryParse(resolutionParts[0], out double width) &&
                    double.TryParse(resolutionParts[1], out double height))
                {
                    Width = width;
                    Height = height;
                }

                CenterWindowOnScreen();
            }
        }
        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadMainDataAsync();
        }
        private async Task LoadMainDataAsync()
        {
            try
            {
                ShowLoading();

                _isLoading = true;

                _appSettings = _settingsService.LoadSettings();

                txtCurrentSettings.Text =
                    $"Current settings: Championship: {_appSettings.Championship} | Source: {_appSettings.DataSource}";

                cmbFavoriteTeam.Items.Clear();
                cmbOpponentTeam.Items.Clear();

                txtMatchResult.Text = "Select teams to show result";
                canvasField.Children.Clear();
                _selectedMatch = null;

                var teams = await _dataService.GetTeamDisplayNamesAsync(_appSettings.Championship);

                foreach (string team in teams)
                {
                    cmbFavoriteTeam.Items.Add(team);
                }

                string? favoriteTeamCode = _favoritesService.LoadFavoriteTeam();

                if (!string.IsNullOrWhiteSpace(favoriteTeamCode))
                {
                    string? favoriteTeamDisplay = teams.FirstOrDefault(team =>
                        TeamDisplayHelper.ExtractFifaCode(team)
                            .Equals(favoriteTeamCode, StringComparison.OrdinalIgnoreCase));

                    if (favoriteTeamDisplay != null)
                    {
                        cmbFavoriteTeam.SelectedItem = favoriteTeamDisplay;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load WPF data: {ex.Message}");
            }
            finally
            {
                _isLoading = false;
                HideLoading();
            }

            await LoadOpponentTeamsAsync();
        }

        private async Task LoadOpponentTeamsAsync()
        {
            if (_isLoading || cmbFavoriteTeam.SelectedItem == null)
            {
                return;
            }

            try
            {
                ShowLoading();
                _isLoading = true;

                cmbOpponentTeam.Items.Clear();
                txtMatchResult.Text = "Select teams to show result";

                string selectedTeam = cmbFavoriteTeam.SelectedItem.ToString()!;
                string fifaCode = TeamDisplayHelper.ExtractFifaCode(selectedTeam);

                if (string.IsNullOrWhiteSpace(fifaCode))
                {
                    return;
                }

                var opponents = await _dataService.GetOpponentTeamDisplayNamesAsync(_appSettings.Championship, fifaCode);

                foreach (string opponent in opponents)
                {
                    cmbOpponentTeam.Items.Add(opponent);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load opponent teams: {ex.Message}");
            }
            finally
            {
                _isLoading = false;
                HideLoading();
            }

            await LoadMatchResultAsync();
        }
        private async Task LoadMatchResultAsync()
        {
            if (_isLoading || cmbFavoriteTeam.SelectedItem == null || cmbOpponentTeam.SelectedItem == null)
            {
                return;
            }

            try
            {
                ShowLoading();
                string selectedTeam = cmbFavoriteTeam.SelectedItem.ToString()!;
                string opponentTeam = cmbOpponentTeam.SelectedItem.ToString()!;

                string selectedCode = TeamDisplayHelper.ExtractFifaCode(selectedTeam);
                string opponentCode = TeamDisplayHelper.ExtractFifaCode(opponentTeam);

                _selectedMatch = await _dataService.GetMatchBetweenTeamsAsync(
                    _appSettings.Championship,
                    selectedCode,
                    opponentCode
                );

                if (string.IsNullOrWhiteSpace(selectedCode) || string.IsNullOrWhiteSpace(opponentCode))
                {
                    return;
                }

                string result = await _dataService.GetMatchResultTextAsync(
                    _appSettings.Championship,
                    selectedCode,
                    opponentCode
                );

                txtMatchResult.Text = result;
                DisplayStartingLineups(selectedCode, opponentCode);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not load match result: {ex.Message}");
            }
            finally
            {
                HideLoading();
            }
        }
        private async void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string oldChampionship = _appSettings.Championship;
                string oldDataSource = _appSettings.DataSource;

                SettingsWindow settingsWindow = new SettingsWindow();

                bool? result = settingsWindow.ShowDialog();

                if (result != true)
                {
                    return;
                }

                ApplyWpfSettings();

                AppSettings newAppSettings = _settingsService.LoadSettings();

                bool appSettingsChanged =
                    !oldChampionship.Equals(newAppSettings.Championship, StringComparison.OrdinalIgnoreCase) ||
                    !oldDataSource.Equals(newAppSettings.DataSource, StringComparison.OrdinalIgnoreCase);

                if (appSettingsChanged)
                {
                    await LoadMainDataAsync();
                }
                else
                {
                    RefreshFieldPositions();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not apply settings: {ex.Message}");
            }
        }

        private async void cmbFavoriteTeam_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            await LoadOpponentTeamsAsync();
        }

        private async void cmbOpponentTeam_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            await LoadMatchResultAsync();
        }
        private async Task ShowTeamDetailsAsync(string? selectedTeamText)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(selectedTeamText))
                {
                    MessageBox.Show("Please select a team.");
                    return;
                }

                string fifaCode = TeamDisplayHelper.ExtractFifaCode(selectedTeamText);

                if (string.IsNullOrWhiteSpace(fifaCode))
                {
                    MessageBox.Show("Could not read FIFA code.");
                    return;
                }

                TeamResult? team = await _dataService.GetTeamResultByFifaCodeAsync(_appSettings.Championship, fifaCode);

                if (team == null)
                {
                    MessageBox.Show("Team details not found.");
                    return;
                }

                TeamDetailsWindow detailsWindow = new TeamDetailsWindow(team);
                detailsWindow.Owner = this;
                detailsWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not show team details: {ex.Message}");
            }
        }
        private void DisplayStartingLineups(string selectedCode, string opponentCode)
        {
            canvasField.Children.Clear();

            if (_selectedMatch == null)
            {
                return;
            }

            bool selectedTeamIsHome = _selectedMatch.HomeTeam.Code.Equals(
                selectedCode,
                StringComparison.OrdinalIgnoreCase
            );

            var selectedTeamStats = selectedTeamIsHome
                ? _selectedMatch.HomeTeamStatistics
                : _selectedMatch.AwayTeamStatistics;

            var opponentTeamStats = selectedTeamIsHome
                ? _selectedMatch.AwayTeamStatistics
                : _selectedMatch.HomeTeamStatistics;

            AddPlayersToField(selectedTeamStats.StartingEleven, selectedCode, true);
            AddPlayersToField(opponentTeamStats.StartingEleven, opponentCode, false);
        }
        private void AddPlayersToField(List<StartingEleven> players, string fifaCode, bool isLeftTeam)
        {
            double fieldWidth = canvasField.ActualWidth;
            double fieldHeight = canvasField.ActualHeight;

            if (fieldWidth <= 0)
            {
                fieldWidth = 900;
            }

            if (fieldHeight <= 0)
            {
                fieldHeight = 400;
            }

            Dictionary<string, int> positionCounters = new Dictionary<string, int>();
            Dictionary<string, int> positionTotals = new Dictionary<string, int>();

            foreach (StartingEleven player in players)
            {
                string position = player.Position.ToString();

                if (!positionTotals.ContainsKey(position))
                {
                    positionTotals[position] = 0;
                }

                positionTotals[position]++;
            }

            foreach (StartingEleven player in players)
            {
                string position = player.Position.ToString();

                if (!positionCounters.ContainsKey(position))
                {
                    positionCounters[position] = 0;
                }

                int indexInPosition = positionCounters[position];
                positionCounters[position]++;

                int totalInPosition = positionTotals[position];

                double x = GetPlayerXPosition(position, isLeftTeam, fieldWidth);
                double y = GetPlayerYPosition(indexInPosition, totalInPosition, fieldHeight);

                string imagePath = _imageService.GetPlayerImageOrDefault(fifaCode, player.Name);

                PlayerFieldControl control = new PlayerFieldControl();
                control.SetPlayer(player, imagePath);

                control.MouseLeftButtonUp += PlayerFieldControl_MouseLeftButtonUp;

                Canvas.SetLeft(control, x);
                Canvas.SetTop(control, y);

                canvasField.Children.Add(control);
            }
        }
        private double GetPlayerXPosition(string position, bool isLeftTeam, double fieldWidth)
        {
            if (isLeftTeam)
            {
                switch (position)
                {
                    case "Goalie":
                        return fieldWidth * 0.03;

                    case "Defender":
                        return fieldWidth * 0.16;

                    case "Midfield":
                        return fieldWidth * 0.31;

                    case "Forward":
                        return fieldWidth * 0.41;

                    default:
                        return fieldWidth * 0.25;
                }
            }
            else
            {
                switch (position)
                {
                    case "Goalie":
                        return fieldWidth - 110;

                    case "Defender":
                        return fieldWidth * 0.75;

                    case "Midfield":
                        return fieldWidth * 0.60;

                    case "Forward":
                        return fieldWidth * 0.50;

                    default:
                        return fieldWidth * 0.70;
                }
            }
        }

        private double GetPlayerYPosition(int indexInPosition, int totalInPosition, double fieldHeight)
        {
            double controlHeight = 55;
            double topPadding = 15;
            double bottomPadding = 15;

            double availableHeight = fieldHeight - controlHeight - topPadding - bottomPadding;

            if (totalInPosition <= 1)
            {
                return topPadding + availableHeight / 2;
            }

            double spacing = availableHeight / (totalInPosition - 1);

            return topPadding + indexInPosition * spacing;
        }
        private void PlayerFieldControl_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is not PlayerFieldControl control || control.Player == null)
            {
                return;
            }

            ShowPlayerDetails(control.Player);
        }

        private void ShowPlayerDetails(StartingEleven player)
        {
            try
            {
                if (_selectedMatch == null)
                {
                    return;
                }

                string fifaCode = GetPlayerTeamCode(player);

                string imagePath = _imageService.GetPlayerImageOrDefault(fifaCode, player.Name);

                int goals = GetPlayerGoalsInSelectedMatch(player.Name);
                int yellowCards = GetPlayerYellowCardsInSelectedMatch(player.Name);

                PlayerDetailsWindow detailsWindow = new PlayerDetailsWindow(
                    player,
                    imagePath,
                    goals,
                    yellowCards
                );

                detailsWindow.Owner = this;
                detailsWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not show player details: {ex.Message}");
            }
        }

        private string GetPlayerTeamCode(StartingEleven player)
        {
            if (_selectedMatch == null)
            {
                return string.Empty;
            }

            bool isHomePlayer = _selectedMatch.HomeTeamStatistics.StartingEleven
                .Any(p => p.Name.Equals(player.Name, StringComparison.OrdinalIgnoreCase));

            if (isHomePlayer)
            {
                return _selectedMatch.HomeTeam.Code;
            }

            return _selectedMatch.AwayTeam.Code;
        }

        private int GetPlayerGoalsInSelectedMatch(string playerName)
        {
            if (_selectedMatch == null)
            {
                return 0;
            }

            var allEvents = _selectedMatch.HomeTeamEvents
                .Concat(_selectedMatch.AwayTeamEvents);

            return allEvents.Count(matchEvent =>
                matchEvent.Player.Equals(playerName, StringComparison.OrdinalIgnoreCase) &&
                (
                    matchEvent.TypeOfEvent.ToString().Equals("Goal", StringComparison.OrdinalIgnoreCase) ||
                    matchEvent.TypeOfEvent.ToString().Equals("GoalPenalty", StringComparison.OrdinalIgnoreCase)
                ));
        }

        private int GetPlayerYellowCardsInSelectedMatch(string playerName)
        {
            if (_selectedMatch == null)
            {
                return 0;
            }

            var allEvents = _selectedMatch.HomeTeamEvents
                .Concat(_selectedMatch.AwayTeamEvents);

            return allEvents.Count(matchEvent =>
                matchEvent.Player.Equals(playerName, StringComparison.OrdinalIgnoreCase) &&
                (
                    matchEvent.TypeOfEvent.ToString().Equals("YellowCard", StringComparison.OrdinalIgnoreCase) ||
                    matchEvent.TypeOfEvent.ToString().Equals("YellowCardSecond", StringComparison.OrdinalIgnoreCase)
                ));
        }
        private async void btnFavoriteTeamDetails_Click(object sender, RoutedEventArgs e)
        {
            await ShowTeamDetailsAsync(cmbFavoriteTeam.SelectedItem?.ToString());
        }

        private async void btnOpponentTeamDetails_Click(object sender, RoutedEventArgs e)
        {
            await ShowTeamDetailsAsync(cmbOpponentTeam.SelectedItem?.ToString());
        }
        private void Window_Closing(object? sender, CancelEventArgs e)
        {
            try
            {
                ConfirmExitWindow confirmWindow = new ConfirmExitWindow();
                confirmWindow.Owner = this;

                bool? result = confirmWindow.ShowDialog();

                if (result != true)
                {
                    e.Cancel = true;
                }
            }
            catch (Exception)
            {
                e.Cancel = false;
            }
        }

        private void ShowLoading()
        {
            txtLoading.Visibility = Visibility.Visible;
            Cursor = Cursors.Wait;
        }

        private void HideLoading()
        {
            txtLoading.Visibility = Visibility.Collapsed;
            Cursor = Cursors.Arrow;
        }

        private void RefreshFieldPositions()
        {
            if (_selectedMatch == null ||
                cmbFavoriteTeam.SelectedItem == null ||
                cmbOpponentTeam.SelectedItem == null)
            {
                return;
            }

            string selectedTeam = cmbFavoriteTeam.SelectedItem.ToString()!;
            string opponentTeam = cmbOpponentTeam.SelectedItem.ToString()!;

            string selectedCode = TeamDisplayHelper.ExtractFifaCode(selectedTeam);
            string opponentCode = TeamDisplayHelper.ExtractFifaCode(opponentTeam);

            if (string.IsNullOrWhiteSpace(selectedCode) ||
                string.IsNullOrWhiteSpace(opponentCode))
            {
                return;
            }

            DisplayStartingLineups(selectedCode, opponentCode);
        }

        private void canvasField_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RefreshFieldPositions();
        }
        private void CenterWindowOnScreen()
        {
            Left = (SystemParameters.WorkArea.Width - Width) / 2;
            Top = (SystemParameters.WorkArea.Height - Height) / 2;
        }
    }
}