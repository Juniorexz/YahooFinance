using Newtonsoft.Json;
using System.Collections.Generic;

public partial class Indicators
{
    [JsonProperty("quote")]
    public Quote[] Quote { get; set; }
}
