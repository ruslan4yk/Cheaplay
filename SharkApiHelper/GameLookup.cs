using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SharkApiHelper
{
    public class GameLookup
    {
        [JsonProperty("info")]
        public Info Info { get; set; }

        [JsonProperty("cheapestPriceEver")]
        public CheapestPriceEver CheapestPriceEver { get; set; }

        [JsonProperty("deals")]
        public List<Deal> Deals { get; set; }

        public Deal GetBestDeal()
        {
            decimal maxSavings = Deals.Max(p => Decimal.Parse(p.Savings, CultureInfo.InvariantCulture));
            Deal best = Deals.First(d => Decimal.Parse(d.Savings, CultureInfo.InvariantCulture) == maxSavings);
            return best;
        }
    }
}
