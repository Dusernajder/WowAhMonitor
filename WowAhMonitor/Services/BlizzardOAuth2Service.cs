using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using QuickType;
using Settings;


namespace WowAhMonitor.Services
{
    public class BlizzardOAuth2Service : IBlizzardOAuth2Service
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly BlizzardApiSettings _blizzardApiSettings;

        public BlizzardOAuth2Service(IHttpClientFactory clientFactory, IOptions<BlizzardApiSettings> settings)
        {
            _clientFactory = clientFactory;
            _blizzardApiSettings = settings.Value;
        }

        public async Task<AccessTokenCredentialsResponse> GetToken()
        {
            var uri = String.Format(_blizzardApiSettings.Links.AccessTokenUrl, "eu");
            using var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(new HttpMethod("POST"), uri);
            var base64Authorization =
                Convert.ToBase64String(Encoding.ASCII.GetBytes(
                    $"{_blizzardApiSettings.ClientInfo.ClientId}:{_blizzardApiSettings.ClientInfo.ClientSecret}")
                );

            request.Headers.TryAddWithoutValidation("Authorization", $"Basic {base64Authorization}");
            request.Content = new StringContent("grant_type=client_credentials");
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

            var response = await client.SendAsync(request);
            var textResult = await response.Content.ReadAsStringAsync();
            var myObjResult = JsonConvert.DeserializeObject<AccessTokenCredentialsResponse>(textResult);


            return myObjResult;
        }

        public bool IsTokenValid()
        {
            throw new System.NotImplementedException();
        }
    }
}