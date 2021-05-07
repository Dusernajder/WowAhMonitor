using System.ComponentModel;

namespace WowAhMonitor.Settings
{
    public static class Region
    {
        public static string Russia { get; } = "ru";
        public static string Europe { get; } = "eu";
        public static string Usa { get; } = "us";

        public static string SetUriRegion(string url, string region) => string.Format(url, region);
    }
}