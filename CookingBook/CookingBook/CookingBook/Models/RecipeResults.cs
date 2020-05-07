using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace CookingBook.Models
{
    public class RecipeResults
    {
        [JsonProperty("results")]
        public List<RecipePreview> Results { get; set; }
        [JsonProperty("totalResults")]
        public int TotalResults { get; set; }
        public List<Recipe> Recipes { get; set; }
        public string Ids => Results.Select(r => r.Id).Separate(',');
    }
}