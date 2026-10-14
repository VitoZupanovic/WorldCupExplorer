using DataLayer.Models;
using WorldCup.DataLayer.Models;

namespace DataLayer.Services
{
    public class ApiDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://worldcup-vua.nullbit.hr";

        public ApiDataService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Match>> GetMatchesAsync(string championship)
        {
            string url = $"{_baseUrl}/{championship}/matches";

            string json = await _httpClient.GetStringAsync(url);

            return Match.FromJson(json);
        }

        public async Task<List<TeamResult>> GetTeamResultsAsync(string championship)
        {
            string url = $"{_baseUrl}/{championship}/teams/results";

            string json = await _httpClient.GetStringAsync(url);

            return TeamResult.FromJson(json);
        }
        public async Task<List<string>> GetTeamDisplayNamesAsync(string championship)
        {
            List<TeamResult> teams = await GetTeamResultsAsync(championship);

            return teams
                .OrderBy(t => t.Country)
                .Select(t => $"{t.Country} ({t.FifaCode})")
                .ToList();
        }

        public async Task<List<Match>> GetMatchesByCountryAsync(string championship, string fifaCode)
        {
            List<Match> matches = await GetMatchesAsync(championship);

            return matches
                .Where(m =>
                    m.HomeTeam.Code.Equals(fifaCode, StringComparison.OrdinalIgnoreCase) ||
                    m.AwayTeam.Code.Equals(fifaCode, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public async Task<Match?> GetFirstMatchByCountryAsync(string championship, string fifaCode)
        {
            return (await GetMatchesByCountryAsync(championship, fifaCode))
                .OrderBy(m => m.Datetime)
                .FirstOrDefault();
        }

        public async Task<List<StartingEleven>> GetPlayersFromFirstMatchAsync(string championship, string fifaCode)
        {
            Match? firstMatch = await GetFirstMatchByCountryAsync(championship, fifaCode);

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

        public async Task<List<PlayerDisplay>> GetPlayerDisplaysFromFirstMatchAsync(string championship, string fifaCode)
        {
            List<StartingEleven> players = await GetPlayersFromFirstMatchAsync(championship, fifaCode);

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

        public async Task<List<TeamInfo>> GetTeamsAsync(string championship)
        {
            string url = $"{_baseUrl}/{championship}/teams";

            string json = await _httpClient.GetStringAsync(url);

            return TeamInfo.FromJson(json);
        }

        public async Task<List<GroupResult>> GetGroupResultsAsync(string championship)
        {
            string url = $"{_baseUrl}/{championship}/teams/group_results";

            string json = await _httpClient.GetStringAsync(url);

            return GroupResult.FromJson(json);
        }
    }
}
