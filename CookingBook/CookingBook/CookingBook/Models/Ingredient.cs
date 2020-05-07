using Newtonsoft.Json;

namespace CookingBook.Models
{
    public class Ingredient
    {
        [JsonProperty("id")]
        public int? Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("amount")]
        public double Amount { get; set; }
        [JsonProperty("unit")]
        public string Unit { get; set; }
    }
}