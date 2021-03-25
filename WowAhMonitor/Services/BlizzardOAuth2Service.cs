using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using QuickType;
using WowAhMonitor.Models;
using WowAhMonitor.Settings;

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
            using var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(new HttpMethod("POST"), "https://eu.battle.net/oauth/token");
            var base64Authorization =
                Convert.ToBase64String(
                    Encoding.ASCII.GetBytes("8cc2844e88424060b0a928ffd1576446:MbBIJQwehWOavVOVIG3sXYE4GFHwsrTB"));
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