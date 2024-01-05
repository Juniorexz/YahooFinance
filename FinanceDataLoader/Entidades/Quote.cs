using Newtonsoft.Json;
using System.Collections.Generic;

public partial class Quote
{
    [JsonProperty("high")]
    public double?[] High { get; set; }

    [JsonProperty("close")]
    public double?[] Close { get; set; }

    [JsonProperty("open")]
    public double?[] Open { get; set; }

    [JsonProperty("volume")]
    public long?[] Volume { get; set; }

    [JsonProperty("low")]
    public double?[] Low { get; set; }
}
