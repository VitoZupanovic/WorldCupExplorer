using System.Windows;
using WorldCup.DataLayer.Models;

namespace WPF
{
    /// <summary>
    /// Interaction logic for TeamDetailsWindow.xaml
    /// </summary>
    public partial class TeamDetailsWindow : Window
    {

        public TeamDetailsWindow(TeamResult team)
        {
            InitializeComponent();

            if (team == null)
            {
                Close();
                return;
            }

            txtCountry.Text = team.Country;
            txtFifaCode.Text = team.FifaCode;
            txtGamesPlayed.Text = team.GamesPlayed.ToString();
            txtWins.Text = team.Wins.ToString();
            txtDraws.Text = team.Draws.ToString();
            txtLosses.Text = team.Losses.ToString();
            txtGoalsFor.Text = team.GoalsFor.ToString();
            txtGoalsAgainst.Text = team.GoalsAgainst.ToString();
            txtGoalDifference.Text = team.GoalDifferential.ToString();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
