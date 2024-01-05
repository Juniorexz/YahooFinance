using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class YahooFinance
{
    [JsonProperty("chart")]
    public Chart Chart { get; set; }
}
