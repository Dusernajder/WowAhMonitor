using System;
using System.Threading.Tasks;
using QuickType;

namespace WowAhMonitor.Services
{
    public interface IBlizzardOAuth2Service
    {
        public Task<AccessTokenCredentialsResponse> GetToken();
        public bool IsTokenValid();
    }
}