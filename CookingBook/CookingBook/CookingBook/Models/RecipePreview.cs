using Newtonsoft.Json;

namespace CookingBook.Models
{
    public class RecipePreview
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}