using Newtonsoft.Json;

public partial class Post
{
    [JsonProperty("timezone")]
    public string Timezone { get; set; }

    [JsonProperty("start")]
    public long Start { get; set; }

    [JsonProperty("end")]
    public long End { get; set; }

    [JsonProperty("gmtoffset")]
    public long Gmtoffset { get; set; }
}
