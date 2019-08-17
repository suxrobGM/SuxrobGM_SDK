using ServerSideAnalytics;

namespace SuxrobGM.Sdk.ServerSideAnalytics.SqLite
{
    internal class SqLiteGeoIpRange
    {
        public long Id { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public CountryCode CountryCode { get; set; }

    }
}
