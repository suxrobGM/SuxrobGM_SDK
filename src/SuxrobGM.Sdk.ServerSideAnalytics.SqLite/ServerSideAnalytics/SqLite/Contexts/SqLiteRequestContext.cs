using Microsoft.EntityFrameworkCore;

namespace SuxrobGM.Sdk.ServerSideAnalytics.SqLite
{
    internal class SqLiteRequestContext : DbContext
    {
        private readonly string _requestTable;
        private readonly string _connectionString;

        public SqLiteRequestContext(string connectionString, string requestTable)
        {
            _connectionString = connectionString;
            _requestTable = requestTable;
        }

        public DbSet<SqliteWebRequest> WebRequest { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SqliteWebRequest>(b => { b.ToTable(_requestTable); });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);
        }
    }
}