using System;
using Newtonsoft.Json;

namespace Settings
{
    public partial class BlizzardApiSettings
    {
        [JsonProperty("ClientInfo")] public ClientInfo ClientInfo { get; set; }

        [JsonProperty("Links")] public Links Links { get; set; }
    }

    public partial class ClientInfo
    {
        [JsonProperty("ClientID")] public string ClientId { get; set; }

        [JsonProperty("ClientSecret")] public string ClientSecret { get; set; }

        [JsonProperty("OldClientSecret")] public string OldClientSecret { get; set; }
    }

    public partial class Links
    {
        [JsonProperty("AccessTokenUrl")] public Uri AccessTokenUrl { get; set; }

        [JsonProperty("WowApi")] public string WowApi { get; set; }
    }
}