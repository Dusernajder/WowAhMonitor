using System.Collections.Generic;
using System.Threading.Tasks;
using WowAhMonitor.Models;

namespace WowAhMonitor.Services
{
    public interface IBlizzardApiService
    {
        public Task<ConnectedRealmsResponse> GetRealmsLinksAsync();
        public Task<ConnectedRealmsResponse> GetRealmsDetailsAsync(ConnectedRealmsResponse realmLinks);
    }
}