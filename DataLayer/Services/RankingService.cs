using DataLayer.Models;

namespace DataLayer.Services
{
    public class RankingService
    {
        private readonly WorldCupDataService _worldCupDataService;

        public RankingService()
        {
            _worldCupDataService = new WorldCupDataService();
        }
        private async Task<List<PlayerRankingItem>> GetPlayerEventRankingAsync(
        string championship,
        string fifaCode,
        params string[] eventTypesToCount)
        {
            var matches = await _worldCupDataService.GetMatchesByCountryAsync(championship, fifaCode);

            var eventCounts = new Dictionary<string, int>();
            var appearances = new Dictionary<string, int>();
            var playerInfo = new Dictionary<string, PlayerRankingItem>();

            foreach (var match in matches)
            {
                bool isHomeTeam = match.HomeTeam.Code.Equals(fifaCode, StringComparison.OrdinalIgnoreCase);

                var teamStatistics = isHomeTeam
                    ? match.HomeTeamStatistics
                    : match.AwayTeamStatistics;

                var teamEvents = isHomeTeam
                    ? match.HomeTeamEvents
                    : match.AwayTeamEvents;

                var players = teamStatistics.StartingEleven
                    .Concat(teamStatistics.Substitutes)
                    .ToList();

                foreach (var player in players)
                {
                    string key = player.Name;

                    if (!appearances.ContainsKey(key))
                    {
                        appearances[key] = 0;
                    }

                    appearances[key]++;

                    if (!playerInfo.ContainsKey(key))
                    {
                        playerInfo[key] = new PlayerRankingItem
                        {
                            PlayerName = player.Name,
                            ShirtNumber = (long)player.ShirtNumber,
                            Position = player.Position.ToString(),
                            Count = 0,
                            Appearances = 0
                        };
                    }
                }

                foreach (var matchEvent in teamEvents)
                {
                    string eventType = matchEvent.TypeOfEvent.ToString();

                    bool shouldCount = eventTypesToCount.Any(type =>
                        type.Equals(eventType, StringComparison.OrdinalIgnoreCase));

                    if (shouldCount)
                    {
                        string playerName = matchEvent.Player;

                        if (!eventCounts.ContainsKey(playerName))
                        {
                            eventCounts[playerName] = 0;
                        }

                        eventCounts[playerName]++;
                    }
                }
            }

            foreach (var item in playerInfo.Values)
            {
                item.Count = eventCounts.ContainsKey(item.PlayerName)
                    ? eventCounts[item.PlayerName]
                    : 0;

                item.Appearances = appearances.ContainsKey(item.PlayerName)
                    ? appearances[item.PlayerName]
                    : 0;
            }

            return playerInfo.Values
                .OrderByDescending(p => p.Count)
                .ThenBy(p => p.PlayerName)
                .ToList();
        }
        public async Task<List<AttendanceRankingItem>> GetAttendanceRankingAsync(string championship, string fifaCode)
        {
            var matches = await _worldCupDataService.GetMatchesByCountryAsync(championship, fifaCode);

            return matches
                .Select(m => new AttendanceRankingItem
                {
                    Location = m.Location,
                    HomeTeam = m.HomeTeamCountry,
                    AwayTeam = m.AwayTeamCountry,
                    Attendance = (long)m.Attendance
                })
                .OrderByDescending(r => r.Attendance)
                .ToList();
        }
        public async Task<List<PlayerRankingItem>> GetGoalRankingAsync(string championship, string fifaCode)
        {
            return await GetPlayerEventRankingAsync(championship, fifaCode, "Goal", "GoalPenalty");
        }
        public async Task<List<PlayerRankingItem>> GetYellowCardRankingAsync(string championship, string fifaCode)
        {
            return await GetPlayerEventRankingAsync(championship, fifaCode, "YellowCard", "YellowCardSecond");
        }
    }
}
