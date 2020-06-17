using Microsoft.EntityFrameworkCore;
using SuxrobGM.Sdk.ServerAnalytics.Models;

namespace SuxrobGM.Sdk.ServerAnalytics.Sqlite
{
    public class SqliteDbContext : DbContext
    {
        private readonly string _connectionString;
       
        public SqliteDbContext() : this("Data Source = app_analytics.sqlite")
        {
        }

        public SqliteDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<WebTraffic> Traffics { get; set; }        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);
        }
    }
}
