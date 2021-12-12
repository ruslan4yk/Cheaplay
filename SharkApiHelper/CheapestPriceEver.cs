using Newtonsoft.Json;

namespace SharkApiHelper
{
    public class CheapestPriceEver
    {
        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("date")]
        public int Date { get; set; }
    }
}
