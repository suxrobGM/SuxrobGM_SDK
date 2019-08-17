using System.Net;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServerSideAnalytics;

namespace SuxrobGM.Sdk.ServerSideAnalytics.SqLite
{
    public class SqliteGeoIpResolver : IGeoIpResolver
    {
        private string _geoIpTable = "SSAGeoIP";
        private string _connectionString;
        private bool _firstCall;

        public SqliteGeoIpResolver() : this("Data Source = geoIp.db")
        {

        }

        public SqliteGeoIpResolver(string connectionString)
        {
            _connectionString = connectionString;
        }


        private SqLiteGeoIpContext GetContext()
        {
            var context = new SqLiteGeoIpContext(_connectionString, _geoIpTable);
            if (!_firstCall)
            {
                context.Database.EnsureCreated();
                _firstCall = true;
            }
            return context;
        }

        public SqliteGeoIpResolver GeoIpTable(string tablename)
        {
            _geoIpTable = tablename;
            return this;
        }


        public async Task StoreGeoIpRangeAsync(IPAddress from, IPAddress to, CountryCode countryCode)
        {
            using (var db = GetContext())
            {
                await db.Database.EnsureCreatedAsync();

                await db.GeoIpRange.AddAsync(new SqLiteGeoIpRange
                {
                    From = from.ToFullDecimalString(),
                    To = to.ToFullDecimalString(),
                    CountryCode = countryCode
                });

                await db.SaveChangesAsync();
            }
        }

        public async Task<CountryCode> ResolveCountryCodeAsync(IPAddress address)
        {
            var addressString = address.ToFullDecimalString();

            using (var db = GetContext())
            {
                var found = await db.GeoIpRange.FirstOrDefaultAsync(x => x.From.CompareTo(addressString) <= 0 &&
                                                                         x.To.CompareTo(addressString) >= 0);

                return found?.CountryCode ?? CountryCode.World;
            }
        }

        public async Task PurgeGeoIpAsync()
        {
            using (var db = GetContext())
            {
                await db.Database.EnsureCreatedAsync();
                db.GeoIpRange.RemoveRange(db.GeoIpRange);
                await db.SaveChangesAsync();
            }
        }
    }
}
