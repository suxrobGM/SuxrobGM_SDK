using Newtonsoft.Json;

namespace SuxrobGM.Sdk.ServerAnalytics.Models
{
    public class GeoData
    {
        [JsonProperty("query")]
        public virtual string IpAddress { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string Continent { get; set; }
        public string ContinentCode { get; set; }
        public string Region { get; set; }
        public string RegionName { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public int Zip { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string Timezone { get; set; }
        public string Currency { get; set; }
        public string Isp { get; set; }
        public string Org { get; set; }
        public string As { get; set; }
        public string AsName { get; set; }
    }
}
