using System;
using System.ComponentModel.DataAnnotations;

namespace SuxrobGM.Sdk.ServerAnalytics.Models
{
    public class WebTraffic
    {
        public WebTraffic() : this(new GeoData())
        {
        }

        public WebTraffic(GeoData geoData)
        {
            Id = Guid.NewGuid().ToString().Replace("-", "");           
            Timestamp = DateTime.Now;

            if (geoData != null)
            {
                Country = geoData.Country;
                CountryCode = geoData.CountryCode;
                Region = geoData.Region;
                RegionName = geoData.RegionName;
                City = geoData.City;
                Latitude = geoData.Latitude;
                Longitude = geoData.Longitude;
                Timezone = geoData.Timezone;
                Isp = geoData.Isp;
                Org = geoData.Org;
                As = geoData.As;
            }          
        }

        [MaxLength(32)]
        public string Id { get; set; }

        public DateTime Timestamp { get; set; }

        [MaxLength(32)]
        public string Identity { get; set; }

        [MaxLength(32)]
        public string RemoteIpAddress { get; set; }

        [MaxLength(16)]
        public string Method { get; set; }

        [MaxLength(1024)]
        public string Path { get; set; }

        [MaxLength(512)]
        public string UserAgent { get; set; }

        [MaxLength(128)]
        public string Country { get; set; }

        [MaxLength(4)]
        public string CountryCode { get; set; }

        [MaxLength(4)]
        public string Region { get; set; }

        [MaxLength(128)]
        public string RegionName { get; set; }

        [MaxLength(128)]
        public string City { get; set; }

        public float Latitude { get; set; }
        public float Longitude { get; set; }

        [MaxLength(32)]
        public string Timezone { get; set; }

        [MaxLength(128)]
        public string Isp { get; set; }

        [MaxLength(128)]
        public string Org { get; set; }

        [MaxLength(128)]
        public string As { get; set; }
    }
}
