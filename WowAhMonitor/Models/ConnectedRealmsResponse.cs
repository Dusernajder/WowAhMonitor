using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WowAhMonitor.Models
{
    public class ConnectedRealmsResponse
    {
        [JsonProperty("_links")] public Links Links { get; set; }

        [JsonProperty("connected_realms")] public List<SelfUri> ConnectedRealms { get; set; }
    }

    public partial class SelfUri
    {
        [JsonProperty("href")] public Uri Href { get; set; }
    }

    public partial class Links
    {
        [JsonProperty("self_uri")] public SelfUri Link { get; set; }
    }
}