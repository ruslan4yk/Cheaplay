using Newtonsoft.Json;

namespace SharkApiHelper
{
    public class Deal
    {
        [JsonProperty("storeID")]
        public string StoreId { get; set; }

        [JsonProperty("dealID")]
        public string DealId { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("retailPrice")]
        public string RetailPrice { get; set; }

        [JsonProperty("savings")]
        public string Savings { get; set; }

        public override string ToString()
        {
            return this.StoreId + " | " + this.Price;
        }
    }
}
