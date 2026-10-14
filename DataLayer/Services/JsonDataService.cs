using DataLayer.Configuration;
using DataLayer.Models;
using WorldCup.DataLayer.Models;

namespace WorldCup.DataLayer.Services
{
    public class JsonDataService
    {
        public List<Match> GetMatches(string championship)
        {
            string filePath = Path.Combine(
                AppConstants.DataFolder,
                championship,
                AppConstants.MatchesFileName
            );

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Could not find file: {filePath}");
            }

            string json = File.ReadAllText(filePath);

            return Match.FromJson(json);
        }
        public List<TeamResult> GetTeamResults(string championship)
        {
            string filePath = Path.Combine(
                AppConstants.DataFolder,
                championship,
                AppConstants.ResultsFileName
            );

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Could not find file: {filePath}");
            }

            string json = File.ReadAllText(filePath);

            return TeamResult.FromJson(json);
        }

        public List<string> GetTeamDisplayNames(string championship)
        {
            List<TeamResult> teams = GetTeamResults(championship);

            return teams
                .OrderBy(t => t.Country)
                .Select(t => $"{t.Country} ({t.FifaCode})")
                .ToList();
        }
        public List<Match> GetMatchesByCountry(string championship, string fifaCode)
        {
            List<Match> matches = GetMatches(championship);

            return matches
                .Where(m =>
                    m.HomeTeam.Code.Equals(fifaCode, StringComparison.OrdinalIgnoreCase) ||
                    m.AwayTeam.Code.Equals(fifaCode, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        public Match? GetFirstMatchByCountry(string championship, string fifaCode)
        {
            return GetMatchesByCountry(championship, fifaCode)
                .OrderBy(m => m.Datetime)
                .FirstOrDefault();
        }
        public List<StartingEleven> GetPlayersFromFirstMatch(string championship, string fifaCode)
        {
            Match? firstMatch = GetFirstMatchByCountry(championship, fifaCode);

            if (firstMatch == null)
            {
                return new List<StartingEleven>();
            }

            if (firstMatch.HomeTeam.Code.Equals(fifaCode, StringComparison.OrdinalIgnoreCase))
            {
                return firstMatch.HomeTeamStatistics.StartingEleven
                    .Concat(firstMatch.HomeTeamStatistics.Substitutes)
                    .ToList();
            }

            return firstMatch.AwayTeamStatistics.StartingEleven
                .Concat(firstMatch.AwayTeamStatistics.Substitutes)
                .ToList();
        }
        public List<PlayerDisplay> GetPlayerDisplaysFromFirstMatch(string championship, string fifaCode)
        {
            List<StartingEleven> players = GetPlayersFromFirstMatch(championship, fifaCode);

            return players
                .Select(p => new PlayerDisplay
                {
                    Name = p.Name,
                    ShirtNumber = (long)p.ShirtNumber,
                    Position = p.Position.ToString(),
                    Captain = p.Captain,
                    IsFavorite = false
                })
                .ToList();
        }

        public List<TeamInfo> GetTeams(string championship)
        {
            string filePath = Path.Combine(
                AppConstants.DataFolder,
                championship,
                AppConstants.TeamsFileName
            );

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Could not find file: {filePath}");
            }

            string json = File.ReadAllText(filePath);

            return TeamInfo.FromJson(json);
        }

        public List<GroupResult> GetGroupResults(string championship)
        {
            string filePath = Path.Combine(
                AppConstants.DataFolder,
                championship,
                AppConstants.GroupResultsFileName
            );

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Could not find file: {filePath}");
            }

            string json = File.ReadAllText(filePath);

            return GroupResult.FromJson(json);
        }
    }
}