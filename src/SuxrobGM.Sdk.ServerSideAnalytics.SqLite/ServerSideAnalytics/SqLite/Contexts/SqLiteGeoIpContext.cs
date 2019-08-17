using Microsoft.EntityFrameworkCore;

namespace SuxrobGM.Sdk.ServerSideAnalytics.SqLite
{
    internal class SqLiteGeoIpContext : DbContext
    {
        private readonly string _geoIpTable;
        private readonly string _connectionString;

        public SqLiteGeoIpContext(string connectionString, string geoIpTable)
        {
            _connectionString = connectionString;
            _geoIpTable = geoIpTable;
        }

        public DbSet<SqliteWebRequest> WebRequest { get; set; }

        public DbSet<SqLiteGeoIpRange> GeoIpRange { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SqLiteGeoIpRange>(b => b.ToTable(_geoIpTable));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString);
        }
    }
}