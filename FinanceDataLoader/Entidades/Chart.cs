using Newtonsoft.Json;
using System.Collections.Generic;

public partial class Chart
{
    [JsonProperty("result")]
    public Result[] Result { get; set; }

    [JsonProperty("error")]
    public object Error { get; set; }
}
