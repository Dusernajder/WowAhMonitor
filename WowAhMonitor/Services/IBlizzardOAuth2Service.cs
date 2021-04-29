using System;
using System.Threading.Tasks;
using QuickType;

namespace WowAhMonitor.Services
{
    public interface IBlizzardOAuth2Service
    {
        public Task<string> GetToken();
        public bool IsTokenValid();
    }
}