using DataLayer.Configuration;

namespace DataLayer.Helpers
{
    public static class DataSourceHelper
    {
        public static bool IsValidDataSource(string dataSource)
        {
            return dataSource.Equals(AppConstants.JsonDataSource, StringComparison.OrdinalIgnoreCase) ||
                   dataSource.Equals(AppConstants.ApiDataSource, StringComparison.OrdinalIgnoreCase);
        }

        public static string NormalizeDataSource(string dataSource)
        {
            if (dataSource.Equals(AppConstants.ApiDataSource, StringComparison.OrdinalIgnoreCase))
            {
                return AppConstants.ApiDataSource;
            }

            return AppConstants.JsonDataSource;
        }
    }
}
