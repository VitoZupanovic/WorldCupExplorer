using DataLayer.Configuration;
using DataLayer.Helpers;
using DataLayer.Models;
using WorldCup.DataLayer.Models;
using WorldCup.DataLayer.Services;

namespace DataLayer.Services
{
    public class WorldCupDataService
    {
        private readonly JsonDataService _jsonDataService;
        private readonly ApiDataService _apiDataService;
        private readonly SettingsService _settingsService;

        public WorldCupDataService()
        {
            _jsonDataService = new JsonDataService();
            _apiDataService = new ApiDataService();
            _settingsService = new SettingsService();
        }

        private bool UseApi()
        {
            var settings = _settingsService.LoadSettings();

            string dataSource = DataSourceHelper.NormalizeDataSource(settings.DataSource);

            return dataSource == AppConstants.ApiDataSource;
        }

        public async Task<List<Match>> GetMatchesAsync(string championship)
        {
            if (UseApi())
            {
                return await _apiDataService.GetMatchesAsync(championship);
            }

            return _jsonDataService.GetMatches(championship);
        }

        public async Task<List<TeamResult>> GetTeamResultsAsync(string championship)
        {
            if (UseApi())
            {
                return await _apiDataService.GetTeamResultsAsync(championship);
            }

            return _jsonDataService.GetTeamResults(championship);
        }

        public async Task<List<string>> GetTeamDisplayNamesAsync(string championship)
        {
            if (UseApi())
            {
                return await _apiDataService.GetTeamDisplayNamesAsync(championship);
            }

            return _jsonDataService.GetTeamDisplayNames(championship);
        }

        public async Task<List<Match>> GetMatchesByCountryAsync(string championship, string fifaCode)
        {
            if (UseApi())
            {
                return await _apiDataService.GetMatchesByCountryAsync(championship, fifaCode);
            }

            return _jsonDataService.GetMatchesByCountry(championship, fifaCode);
        }

        public async Task<Match?> GetFirstMatchByCountryAsync(string championship, string fifaCode)
        {
            if (UseApi())
            {
                return await _apiDataService.GetFirstMatchByCountryAsync(championship, fifaCode);
            }

            return _jsonDataService.GetFirstMatchByCountry(championship, fifaCode);
        }

        public async Task<List<PlayerDisplay>> GetPlayerDisplaysFromFirstMatchAsync(string championship, string fifaCode)
        {
            if (UseApi())
            {
                return await _apiDataService.GetPlayerDisplaysFromFirstMatchAsync(championship, fifaCode);
            }

            return _jsonDataService.GetPlayerDisplaysFromFirstMatch(championship, fifaCode);
        }
        public async Task<TeamResult?> GetTeamResultByFifaCodeAsync(string championship, string fifaCode)
        {
            var teams = await GetTeamResultsAsync(championship);

            return teams.FirstOrDefault(t =>
                t.FifaCode.Equals(fifaCode, StringComparison.OrdinalIgnoreCase));
        }
        public async Task<List<string>> GetOpponentTeamDisplayNamesAsync(string championship, string fifaCode)
        {
            var matches = await GetMatchesByCountryAsync(championship, fifaCode);

            var opponents = matches
                .Select(m =>
                {
                    bool selectedTeamIsHome = m.HomeTeam.Code.Equals(fifaCode, StringComparison.OrdinalIgnoreCase);

                    return selectedTeamIsHome
                        ? $"{m.AwayTeamCountry} ({m.AwayTeam.Code})"
                        : $"{m.HomeTeamCountry} ({m.HomeTeam.Code})";
                })
                .Distinct()
                .OrderBy(team => team)
                .ToList();

            return opponents;
        }
        public async Task<Match?> GetMatchBetweenTeamsAsync(string championship, string firstTeamCode, string secondTeamCode)
        {
            var matches = await GetMatchesByCountryAsync(championship, firstTeamCode);

            return matches.FirstOrDefault(m =>
                (m.HomeTeam.Code.Equals(firstTeamCode, StringComparison.OrdinalIgnoreCase) &&
                 m.AwayTeam.Code.Equals(secondTeamCode, StringComparison.OrdinalIgnoreCase)) ||

                (m.HomeTeam.Code.Equals(secondTeamCode, StringComparison.OrdinalIgnoreCase) &&
                 m.AwayTeam.Code.Equals(firstTeamCode, StringComparison.OrdinalIgnoreCase)));
        }
        public async Task<string> GetMatchResultTextAsync(string championship, string firstTeamCode, string secondTeamCode)
        {
            Match? match = await GetMatchBetweenTeamsAsync(championship, firstTeamCode, secondTeamCode);

            if (match == null)
            {
                return "Match not found";
            }

            return $"{match.HomeTeamCountry} {match.HomeTeam.Goals} : {match.AwayTeam.Goals} {match.AwayTeamCountry}";
        }

        public async Task<List<TeamInfo>> GetTeamsAsync(string championship)
        {
            if (UseApi())
            {
                return await _apiDataService.GetTeamsAsync(championship);
            }

            return _jsonDataService.GetTeams(championship);
        }

        public async Task<List<GroupResult>> GetGroupResultsAsync(string championship)
        {
            if (UseApi())
            {
                return await _apiDataService.GetGroupResultsAsync(championship);
            }

            return _jsonDataService.GetGroupResults(championship);
        }
    }
}
