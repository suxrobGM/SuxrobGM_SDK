using SuxrobGM.Sdk.ServerAnalytics.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SuxrobGM.Sdk.ServerAnalytics
{
    public interface IAnalyticsRepository
    {
        Task AddToDatabaseAsync(WebTraffic traffic);
    }
}
