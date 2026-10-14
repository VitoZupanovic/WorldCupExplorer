using DataLayer.Configuration;
using DataLayer.Models;

namespace DataLayer.Services
{
    public class FavoritesService
    {
        private string FavoriteTeamFilePath
        {
            get
            {
                return Path.Combine(
                    GetSharedConfigFolder(),
                    AppConstants.FavoriteTeamFileName
                );
            }
        }

        private string FavoritePlayersFilePath
        {
            get
            {
                return Path.Combine(
                    GetSharedConfigFolder(),
                    AppConstants.FavoritePlayersFileName
                );
            }
        }

        public bool FavoriteTeamExists()
        {
            return File.Exists(FavoriteTeamFilePath);
        }

        public string? LoadFavoriteTeam()
        {
            if (!FavoriteTeamExists())
            {
                return null;
            }

            string fifaCode = File.ReadAllText(FavoriteTeamFilePath);

            if (string.IsNullOrWhiteSpace(fifaCode))
            {
                return null;
            }

            return fifaCode.Trim();
        }

        public void SaveFavoriteTeam(string fifaCode)
        {
            Directory.CreateDirectory(GetSharedConfigFolder());

            File.WriteAllText(FavoriteTeamFilePath, fifaCode);
        }

        public void SaveFavoritePlayers(string fifaCode, List<FavoritePlayer> player)
        {
            Directory.CreateDirectory(GetSharedConfigFolder());

            var linesToSave = player
                .Take(3)
                .Select(player => $"{fifaCode}|{player.ShirtNumber}|{player.Name}")
                .ToList();

            File.WriteAllLines(FavoritePlayersFilePath, linesToSave);
        }

        public List<FavoritePlayer> LoadFavoritePlayers(string fifaCode)
        {
            if (!File.Exists(FavoritePlayersFilePath))
            {
                return new List<FavoritePlayer>();
            }

            return File.ReadAllLines(FavoritePlayersFilePath)
                       .Where(line => line.StartsWith($"{fifaCode}|"))
                       .Select(line =>
                       {
                           string[] parts = line.Split('|');

                           if (parts.Length < 3)
                           {
                               return null;
                           }

                           bool shirtNumberParsed = long.TryParse(parts[1], out long shirtNumber);

                           if (!shirtNumberParsed)
                           {
                               return null;
                           }

                           return new FavoritePlayer
                           {
                               FifaCode = parts[0],
                               ShirtNumber = shirtNumber,
                               Name = parts[2]
                           };
                       })
                       .Where(player => player != null)
                       .ToList()!;
        }

        public FavoritePlayer CreateFavoritePlayer(string fifaCode, PlayerDisplay player)
        {
            return new FavoritePlayer
            {
                FifaCode = fifaCode,
                ShirtNumber = player.ShirtNumber,
                Name = player.Name
            };
        }
        private string GetSolutionRootFolder()
        {
            DirectoryInfo? directory = new DirectoryInfo(AppContext.BaseDirectory);

            while (directory != null && !directory.GetFiles("*.sln").Any())
            {
                directory = directory.Parent;
            }

            return directory?.FullName ?? AppContext.BaseDirectory;
        }

        private string GetSharedConfigFolder()
        {
            return Path.Combine(GetSolutionRootFolder(), AppConstants.ConfigFolder);
        }
    }
}
