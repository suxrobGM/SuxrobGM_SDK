using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServerSideAnalytics;

namespace SuxrobGM.Sdk.Extensions
{
    public static class ServerSideAnalyticsExtensions
    {
        public static IAnalyticStore UseIpApiFailOver(this IAnalyticStore analyticStore)
        {
            return new IpApiAnalyticStore(analyticStore);
        }
    }

    internal class IpApiAnalyticStore : IAnalyticStore
    {
        private readonly IAnalyticStore _store;

        public IpApiAnalyticStore(IAnalyticStore store)
        {
            _store = store;
        }

        public Task<long> CountAsync(DateTime from, DateTime to) => _store.CountAsync(from, to);

        public Task<long> CountUniqueIdentitiesAsync(DateTime day) => _store.CountUniqueIdentitiesAsync(day);

        public Task<long> CountUniqueIdentitiesAsync(DateTime from, DateTime to) => _store.CountUniqueIdentitiesAsync(from, to);

        public Task<IEnumerable<string>> UniqueIdentitiesAsync(DateTime @from, DateTime to) => _store.UniqueIdentitiesAsync(from, to);

        public Task<IEnumerable<string>> UniqueIdentitiesAsync(DateTime day) => _store.UniqueIdentitiesAsync(day);

        public Task<IEnumerable<ServerSideAnalytics.WebRequest>> InTimeRange(DateTime from, DateTime to) => _store.InTimeRange(from, to);

        public Task<IEnumerable<IPAddress>> IpAddressesAsync(DateTime day) => _store.IpAddressesAsync(day);

        public Task<IEnumerable<IPAddress>> IpAddressesAsync(DateTime from, DateTime to) => _store.IpAddressesAsync(from, to);

        public Task PurgeGeoIpAsync() => _store.PurgeGeoIpAsync();

        public Task PurgeRequestAsync() => _store.PurgeRequestAsync();

        public Task<IEnumerable<ServerSideAnalytics.WebRequest>> RequestByIdentityAsync(string identity) => _store.RequestByIdentityAsync(identity);

        public async Task<CountryCode> ResolveCountryCodeAsync(IPAddress address)
        {
            try
            {
                var resolved = await _store.ResolveCountryCodeAsync(address);

                if(resolved == CountryCode.World)
                {
                    var ipstr = address.ToString();
                    var response = await (new HttpClient()).GetStringAsync($"http://ip-api.com/json/{ipstr}");

                    var obj = JsonConvert.DeserializeObject(response) as JObject;
                    resolved = (CountryCode)Enum.Parse(typeof(CountryCode), obj["countryCode"].ToString(), true);

                    await _store.StoreGeoIpRangeAsync(address, address, resolved);

                    return resolved;
                }

                return resolved;
            }
            catch (Exception)
            {
                return CountryCode.World;
            }
        }

        public Task StoreGeoIpRangeAsync(IPAddress from, IPAddress to, CountryCode countryCode)
        {
            return _store.StoreGeoIpRangeAsync(from, to, countryCode);
        }

        public Task StoreWebRequestAsync(ServerSideAnalytics.WebRequest request)
        {
            return _store.StoreWebRequestAsync(request);
        }
    }
}
