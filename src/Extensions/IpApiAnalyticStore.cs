using System;
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
        public static IGeoIpResolver UseIpApiFailOver(this IGeoIpResolver store)
        {
            return new IpApiGeoResolver(store);
        }
    }

    internal class IpApiGeoResolver : IGeoIpResolver
    {
        private readonly IGeoIpResolver _resolver;

        public IpApiGeoResolver(IGeoIpResolver store)
        {
            _resolver = store;
        }

        public Task PurgeGeoIpAsync() => _resolver.PurgeGeoIpAsync();

        public async Task<CountryCode> ResolveCountryCodeAsync(IPAddress address)
        {
            try
            {
                var resolved = await _resolver.ResolveCountryCodeAsync(address);

                if (resolved == CountryCode.World)
                {
                    var ipstr = address.ToString();
                    var response = await (new HttpClient()).GetStringAsync($"http://ip-api.com/json/{ipstr}");

                    var obj = JsonConvert.DeserializeObject(response) as JObject;
                    resolved = (CountryCode)Enum.Parse(typeof(CountryCode), obj["countryCode"].ToString());

                    await _resolver.StoreGeoIpRangeAsync(address, address, resolved);

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
            return _resolver.StoreGeoIpRangeAsync(from, to, countryCode);
        }
    }
}
