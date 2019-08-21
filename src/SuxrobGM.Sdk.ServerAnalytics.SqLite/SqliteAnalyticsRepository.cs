using System.Threading.Tasks;
using SuxrobGM.Sdk.ServerAnalytics.Models;

namespace SuxrobGM.Sdk.ServerAnalytics.Sqlite
{
    public class SqliteAnalyticsRepository : IAnalyticsRepository
    {
        private readonly string _connectionString;
        private bool _firstCall;

        public SqliteAnalyticsRepository()
        {
            _connectionString = "Data Source = app_analytics.sqlite";
        }

        public SqliteAnalyticsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddToDatabaseAsync(WebTraffic traffic)
        {
            using (var db = GetContext())
            {
                db.Traffics.Add(traffic);
                await db.SaveChangesAsync();
            }
        }

        private SqliteDbContext GetContext()
        {
            var context = new SqliteDbContext(_connectionString);
            if (!_firstCall)
            {
                context.Database.EnsureCreated();
                _firstCall = true;
            }
            return context;
        }
    }
}
