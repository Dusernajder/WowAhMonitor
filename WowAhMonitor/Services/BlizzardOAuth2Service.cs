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
        private AccessTokenCredentialsResponse _tokenCredentialsResponse;

        public BlizzardOAuth2Service(IHttpClientFactory clientFactory, IOptions<BlizzardApiSettings> settings)
        {
            _clientFactory = clientFactory;
            _blizzardApiSettings = settings.Value;
        }

        public async Task<string> GetToken()
        {
            if (_tokenCredentialsResponse == null || !IsTokenValid())
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
                _tokenCredentialsResponse = JsonConvert.DeserializeObject<AccessTokenCredentialsResponse>(textResult);
            }

            return _tokenCredentialsResponse.AccessToken;
        }

        public bool IsTokenValid()
        {
            var tokenTimeStamp = _tokenCredentialsResponse.ExpiresIn;
            DateTime currentTime = DateTime.Now;
            var tokenTimeExpires = currentTime.AddSeconds(tokenTimeStamp).ToLocalTime();
            if (currentTime < tokenTimeExpires)
            {
                Console.WriteLine($"Expires at: {tokenTimeExpires}");
                return true;
            }

            return false;
        }
    }
}