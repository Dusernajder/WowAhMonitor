using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WowAhMonitor.Models;

namespace WowAhMonitor.Services
{
    public class BlizzardApiService : IBlizzardApiService
    {
        private readonly IHttpClientFactory _clientFactory;

        /** for development */
        private readonly string _token = "&access_token=US8gBgbylAiRXpMor3U7bXZ7UaqaesCV0e";

        public BlizzardApiService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<ConnectedRealmsResponse> GetRealmsLinksAsync()
        {
            var url =
                "https://eu.api.blizzard.com/data/wow/connected-realm/index?namespace=dynamic-eu&locale=en_US" + _token;
            var textResult = await SendRequestToBlizzardApi(url);
            var result = JsonConvert.DeserializeObject<ConnectedRealmsResponse>(textResult);
            return result;
        }

        public async Task<ConnectedRealmsResponse> GetRealmsDetails(ConnectedRealmsResponse RealmLinks)
        {
            foreach (var selfUri in RealmLinks.ConnectedRealms)
            {
                var textResult = await SendRequestToBlizzardApi(selfUri.Href + "&locale=en_US" + _token);
                Console.WriteLine(textResult);
                Console.WriteLine();
            }

            return null;
        }

        public Task<string> GetOAuthAccessToken()
        {
            throw new NotImplementedException();
        }

        private async Task<string> SendRequestToBlizzardApi(string url)
        {
            var client = _clientFactory.CreateClient();
            var uri = new Uri(url);
            var response = await client.SendAsync(new HttpRequestMessage {RequestUri = uri, Method = HttpMethod.Get});
            var textResult = await response.Content.ReadAsStringAsync();

            return textResult;
        }
    }
}