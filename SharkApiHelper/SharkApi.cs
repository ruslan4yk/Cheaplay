using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;

namespace SharkApiHelper
{
    public static class SharkApi
    {
        static string _key = GetRapidApiKey();
        private static async Task<string> GameLookupResp(int id)
        {
            string key = GetRapidApiKey();
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://cheapshark-game-deals.p.rapidapi.com/games?id={id}"),
                Headers =
    {
        { "x-rapidapi-host", "cheapshark-game-deals.p.rapidapi.com" },
        { "x-rapidapi-key", _key },
    },
            };

            HttpResponseMessage response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();

            return body;
        }

        private static async Task<string> ListOfGamesResp(string gameTitle)
        {
            string queryTitle = gameTitle.ToLower().Replace(" ", "%20").Replace(":", "%3A").Replace(";", "%3B").Replace(",", "%2C");

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://cheapshark-game-deals.p.rapidapi.com/games?title={queryTitle}&exact=1&limit=60"),
                Headers =
    {
        { "x-rapidapi-host", "cheapshark-game-deals.p.rapidapi.com" },
        { "x-rapidapi-key", _key },
    },
            };
            string body;
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
            }

            return body;
        }
        private static string GetRapidApiKey()
        {
            StreamReader r = new StreamReader($"D:/IDEshky/projects/Eleks_Project/Cheaplay/SharkApiHelper/MySecrets.json");
            string json = r.ReadToEnd();
            return JsonConvert.DeserializeObject<SecretsHelper>(json).RapidapiKey;
        }

        public static async Task<Game> ConvertJsonToGame(int idSharkAPI)
        {
            var json = await GameLookupResp(idSharkAPI);
            var gameLookup = JsonConvert.DeserializeObject<GameLookup>(json);

            var bestDeal = gameLookup.GetBestDeal();
            double savings = Convert.ToDouble(bestDeal.Savings, CultureInfo.InvariantCulture);
            Game game = new Game()
            {
                Title = gameLookup.Info.Title,
                Discount = savings,
                IdSharkAPI = idSharkAPI,
                Image = gameLookup.Info.Thumb,
                Price = Convert.ToDecimal(bestDeal.RetailPrice, CultureInfo.InvariantCulture),
                StoreId = Convert.ToInt32(bestDeal.StoreId, CultureInfo.InvariantCulture),
                IsOnSale = savings > 0,
                LastUpdate = DateTime.Now
            };
            return game;
        }

        private class SecretsHelper
        {
            [JsonProperty("rapidapiKey")]
            public string RapidapiKey { get; set; }            
        }
    }
}
