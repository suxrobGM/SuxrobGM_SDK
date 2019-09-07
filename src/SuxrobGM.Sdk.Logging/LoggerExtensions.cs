using Microsoft.Extensions.Logging;

namespace SuxrobGM.Sdk.Logging
{
    public static class LoggerExtensions
    {
        public static ILoggerFactory AddFile(this ILoggerFactory loggerFactory, string filePath)
        {
            loggerFactory.AddProvider(new FileLoggerProvider(filePath));
            return loggerFactory;
        }
    }
}
