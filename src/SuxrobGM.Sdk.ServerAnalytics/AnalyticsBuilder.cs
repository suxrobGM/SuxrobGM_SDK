using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SuxrobGM.Sdk.ServerAnalytics.Models;
using SuxrobGM.Sdk.ServerAnalytics.Services;

namespace SuxrobGM.Sdk.ServerAnalytics
{
    public class AnalyticsBuilder
    {
        private readonly IAnalyticsRepository _repository;
        private IGeoDataExtractor _geoDataExtractor;
        private List<Func<HttpContext, bool>> _exclude;

        internal AnalyticsBuilder(IAnalyticsRepository analyticsRepository)
        {
            _geoDataExtractor = new IpApiService();
            _repository = analyticsRepository;
        }

        internal async Task Run(HttpContext context, Func<Task> next)
        {
            var identity = context.UserIdentity();

            //Pass the command to the next task in the pipeline
            await next.Invoke();

            //This request should be filtered out ?
            if (_exclude?.Any(x => x(context)) ?? false)
            {
                return;
            }

            var geoData = await _geoDataExtractor.RetrieveGeographicDataAsync(context.Connection.RemoteIpAddress);

            var traffic = new WebTraffic(geoData)
            {
                RemoteIpAddress = context.Connection.RemoteIpAddress.MapToIPv4().ToString(),
                Identity = identity,
                Method = context.Request.Method,
                UserAgent = context.Request.Headers["User-Agent"],
                Path = context.Request.Path.Value,               
            };

            await _repository.AddToDatabaseAsync(traffic);
        }

        public AnalyticsBuilder Exclude(Func<HttpContext, bool> filter)
        {
            if (_exclude == null)
                _exclude = new List<Func<HttpContext, bool>>();

            _exclude.Add(filter);
            return this;
        }

        public AnalyticsBuilder Exclude(IPAddress ip) => Exclude(x => Equals(x.Connection.RemoteIpAddress, ip));
        public AnalyticsBuilder LimitToPath(string path) => Exclude(x => !Equals(x.Request.Path.StartsWithSegments(path)));
        public AnalyticsBuilder ExcludePath(params string[] paths) => Exclude(x => paths.Any(path => x.Request.Path.StartsWithSegments(path)));
        public AnalyticsBuilder ExcludeExtension(params string[] extensions) => Exclude(x => extensions.Any(ext => x.Request.Path.Value.EndsWith(ext)));
        public AnalyticsBuilder ExcludeLoopBack() => Exclude(x => IPAddress.IsLoopback(x.Connection.RemoteIpAddress));
        public AnalyticsBuilder ExcludeIp(IPAddress address) => Exclude(x => x.Connection.RemoteIpAddress.Equals(address));
        public AnalyticsBuilder ExcludeStatusCodes(params HttpStatusCode[] codes) => Exclude(context => codes.Contains((HttpStatusCode)context.Response.StatusCode));
        public AnalyticsBuilder ExcludeStatusCodes(params int[] codes) => Exclude(context => codes.Contains(context.Response.StatusCode));
        public AnalyticsBuilder LimitToStatusCodes(params HttpStatusCode[] codes) => Exclude(context => !codes.Contains((HttpStatusCode)context.Response.StatusCode));
        public AnalyticsBuilder LimitToStatusCodes(params int[] codes) => Exclude(context => !codes.Contains(context.Response.StatusCode));
    }
}
