using Newtonsoft.Json;

namespace SharkApiHelper
{
    public class Info
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("steamAppID")]
        public string SteamAppID { get; set; }

        [JsonProperty("thumb")]
        public string Thumb { get; set; }
    }
}
