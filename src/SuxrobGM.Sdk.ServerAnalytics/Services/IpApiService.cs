using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SuxrobGM.Sdk.ServerAnalytics.Models;

namespace SuxrobGM.Sdk.ServerAnalytics.Services
{
    public class IpApiService : IGeoDataExtractor
    {
        public async Task<GeoData> RetrieveGeographicDataAsync(IPAddress ipAddress)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetStringAsync($"http://ip-api.com/json/{ipAddress}");                  
                    var geoData = JsonConvert.DeserializeObject<GeoData>(response);

                    if (geoData.Status != "success")
                        throw new Exception();

                    return geoData;
                }
                catch (Exception)
                {
                    return null;
                }
                
            }
        }
    }
}
