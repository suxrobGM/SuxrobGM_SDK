using System.Net;
using System.Threading.Tasks;
using SuxrobGM.Sdk.ServerAnalytics.Models;

namespace SuxrobGM.Sdk.ServerAnalytics.Services
{
    public interface IGeoDataExtractor
    {
        Task<GeoData> RetrieveGeographicDataAsync(IPAddress ipAddress);
    }
}
