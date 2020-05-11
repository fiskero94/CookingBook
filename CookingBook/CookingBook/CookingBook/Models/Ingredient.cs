using Newtonsoft.Json;
using SQLite;

namespace CookingBook.Models
{
    public class Ingredient
    {
        [JsonProperty("id")]
        [PrimaryKey, AutoIncrement]
        public int? Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("amount")]
        public double Amount { get; set; }
        [JsonProperty("unit")]
        public string Unit { get; set; }
    }
}