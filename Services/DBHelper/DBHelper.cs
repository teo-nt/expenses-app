using System.Data.SqlClient;

namespace ExpensesManagementApp.Services.DBHelper
{
    public static class DBHelper
    {
        private static SqlConnection? _connection;

        public static SqlConnection? GetConnection()
        {
            var configurationBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            var configuration = configurationBuilder.Build();
            string url = configuration.GetConnectionString("DefaultConnection")!;

            try
            {
                _connection = new SqlConnection(url);
                return _connection;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }
    }
}
