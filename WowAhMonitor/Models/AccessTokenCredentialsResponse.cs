using Newtonsoft.Json;

namespace QuickType
{
    public partial class AccessTokenCredentialsResponse
    {
        [JsonProperty("access_token")] public string AccessToken { get; set; }

        [JsonProperty("token_type")] public string TokenType { get; set; }

        [JsonProperty("expires_in")] public long ExpiresIn { get; set; }

        [JsonProperty("scope")] public string Scope { get; set; }
    }
}