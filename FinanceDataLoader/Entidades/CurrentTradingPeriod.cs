using Newtonsoft.Json;

public partial class CurrentTradingPeriod
{
    [JsonProperty("pre")]
    public Post Pre { get; set; }

    [JsonProperty("regular")]
    public Post Regular { get; set; }

    [JsonProperty("post")]
    public Post Post { get; set; }
}
