using DataLayer.Configuration;

namespace DataLayer.Helpers
{
    public static class ChampionshipHelper
    {
        public static bool IsValidChampionship(string championship)
        {
            return championship.Equals(AppConstants.MenChampionship, StringComparison.OrdinalIgnoreCase) ||
                   championship.Equals(AppConstants.WomenChampionship, StringComparison.OrdinalIgnoreCase);
        }

        public static string NormalizeChampionship(string championship)
        {
            if (championship.Equals(AppConstants.WomenChampionship, StringComparison.OrdinalIgnoreCase))
            {
                return AppConstants.WomenChampionship;
            }

            return AppConstants.MenChampionship;
        }
    }
}
