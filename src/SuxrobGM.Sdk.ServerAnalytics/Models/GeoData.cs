using Newtonsoft.Json;

namespace SuxrobGM.Sdk.ServerAnalytics.Models
{
    public class GeoData
    {
        [JsonProperty("query")]
        public virtual string IpAddress { get; set; }
        public virtual string Status { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }        
        public string Region { get; set; }
        public string RegionName { get; set; }
        public string City { get; set; }

        [JsonProperty("lat")]
        public float Latitude { get; set; }

        [JsonProperty("lon")]
        public float Longitude { get; set; }

        public string Timezone { get; set; }
        public string Isp { get; set; }
        public string Org { get; set; }
        public string As { get; set; }
    }
}
