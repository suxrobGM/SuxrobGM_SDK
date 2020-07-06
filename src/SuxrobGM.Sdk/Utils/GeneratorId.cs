using System;

namespace SuxrobGM.Sdk.Utils
{
    /// <summary>
    /// Static class to generate unique identifications
    /// </summary>
    public static class GeneratorId
    {
        public static string GenerateLong(string prefix)
        {
            return $"{prefix}_{DateTime.Now:yyyyMMddHHmmssffffff}";
        }

        public static string GenerateLong()
        {
            return $"{DateTime.Now:yyyyMMddHHmmssffffff}";
        }

        public static string GenerateShort(string prefix)
        {
            return $"{prefix}_{DateTime.Now:ffffff}";
        }

        public static string GenerateShort()
        {
            return $"{DateTime.Now:ffffff}";
        }

        public static string GenerateComplex()
        {
            return Guid.NewGuid().ToString().ToLower().Replace("-", "");
        }
    }
}
