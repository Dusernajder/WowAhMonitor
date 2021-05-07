using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Settings;
using WowAhMonitor.Models;
using WowAhMonitor.Settings;
using Region = WowAhMonitor.Settings.Region;

namespace WowAhMonitor.Services
{
    public class BlizzardApiService : IBlizzardApiService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly BlizzardApiSettings _blizzardApiSettings;
        private readonly IBlizzardOAuth2Service _auth2Service;

        public BlizzardApiService(IHttpClientFactory clientFactory, IOptions<BlizzardApiSettings> settings,
            IBlizzardOAuth2Service auth2Service)
        {
            _clientFactory = clientFactory;
            _auth2Service = auth2Service;
            _blizzardApiSettings = settings.Value;
        }

        public async Task<ConnectedRealmsResponse> GetRealmsLinksAsync()
        {
            var regionUrl = _blizzardApiSettings.Links.WowApi.SetUriRegion(Region.Europe);
            var token = await _auth2Service.GetToken();
            // todo: method for build urls
            var url = $"{regionUrl}connected-realm/index?namespace=dynamic-eu&locale=en_US&access_token={token}";
            var textResult = await SendRequestToBlizzardApi(url);
            var result = JsonConvert.DeserializeObject<ConnectedRealmsResponse>(textResult);
            return result;
        }

        public async Task<ConnectedRealmsResponse> GetRealmsDetailsAsync(ConnectedRealmsResponse realmLinks)
        {
            foreach (var selfUri in realmLinks.ConnectedRealms)
            {
                var textResult = await SendRequestToBlizzardApi(selfUri.Href + "&locale=en_US");
                Console.WriteLine(textResult);
                Console.WriteLine();
            }

            return null;
        }

        private async Task<string> SendRequestToBlizzardApi(string url)
        {
            using var client = _clientFactory.CreateClient();
            var uri = new Uri(url);
            var response = await client.SendAsync(new HttpRequestMessage {RequestUri = uri, Method = HttpMethod.Get});
            var textResult = await response.Content.ReadAsStringAsync();

            return textResult;
        }
    }
}