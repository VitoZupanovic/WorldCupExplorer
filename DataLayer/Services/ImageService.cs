using DataLayer.Configuration;

namespace DataLayer.Services
{
    public class ImageService
    {

        private string PlayerImagesFilePath
        {
            get
            {
                return Path.Combine(
                    GetSolutionRootFolder(),
                    AppConstants.ConfigFolder,
                    AppConstants.PlayerImagesFileName
                );
            }
        }
        public void SavePlayerImage(string fifaCode, string playerName, string imagePath)
        {
            Directory.CreateDirectory(Path.Combine(
                GetSolutionRootFolder(),
                AppConstants.ConfigFolder
            ));

            var lines = File.Exists(PlayerImagesFilePath)
                ? File.ReadAllLines(PlayerImagesFilePath).ToList()
                : new List<string>();

            string key = $"{fifaCode}|{playerName}|";

            lines.RemoveAll(line => line.StartsWith(key));

            lines.Add($"{fifaCode}|{playerName}|{imagePath}");

            File.WriteAllLines(PlayerImagesFilePath, lines);
        }

        public string? LoadPlayerImage(string fifaCode, string playerName)
        {
            if (!File.Exists(PlayerImagesFilePath))
            {
                return null;
            }

            string key = $"{fifaCode}|{playerName}|";

            string? line = File.ReadAllLines(PlayerImagesFilePath)
                .FirstOrDefault(line => line.StartsWith(key));

            if (line == null)
            {
                return null;
            }

            return line.Substring(key.Length);
        }
        public string GetPlayerImageOrDefault(string fifaCode, string playerName)
        {
            string? savedImagePath = LoadPlayerImage(fifaCode, playerName);

            if (!string.IsNullOrWhiteSpace(savedImagePath))
            {
                string fullSavedPath = GetFullImagePath(savedImagePath);

                if (File.Exists(fullSavedPath))
                {
                    return fullSavedPath;
                }
            }

            string defaultImagePath = Path.Combine(
                AppConstants.ImagesFolder,
                AppConstants.PlayersImagesFolder,
                AppConstants.DefaultPlayerImageFileName
            );

            return GetFullImagePath(defaultImagePath);
        }
        public string CopyPlayerImageToProjectFolder(string fifaCode, string playerName, string sourceImagePath)
        {
            string playersImagesFolder = GetSharedPlayersImagesFolder();

            Directory.CreateDirectory(playersImagesFolder);

            string extension = Path.GetExtension(sourceImagePath);

            string safePlayerName = string.Join("_", playerName.Split(Path.GetInvalidFileNameChars()));

            string fileName = $"{fifaCode}_{safePlayerName}{extension}";

            string destinationPath = Path.Combine(playersImagesFolder, fileName);

            File.Copy(sourceImagePath, destinationPath, true);

            return Path.Combine(
                AppConstants.ImagesFolder,
                AppConstants.PlayersImagesFolder,
                fileName
            );
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

        private string GetFullImagePath(string imagePath)
        {
            if (Path.IsPathRooted(imagePath))
            {
                return imagePath;
            }

            return Path.Combine(GetSolutionRootFolder(), imagePath);
        }

        private string GetSharedPlayersImagesFolder()
        {
            return Path.Combine(
                GetSolutionRootFolder(),
                AppConstants.ImagesFolder,
                AppConstants.PlayersImagesFolder
            );
        }
    }
}
