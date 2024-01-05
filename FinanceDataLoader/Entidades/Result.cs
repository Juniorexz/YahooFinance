using Newtonsoft.Json;
using System.Collections.Generic;

public partial class Result
{
    [JsonProperty("meta")]
    public Meta Meta { get; set; }

    [JsonProperty("timestamp")]
    public long[] Timestamp { get; set; }

    [JsonProperty("indicators")]
    public Indicators Indicators { get; set; }
}
