using System.Collections.Generic;
using Newtonsoft.Json;
using SQLite;

namespace CookingBook.Models
{
    public class Recipe
    {
        [JsonProperty("id")]
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("servings")]
        public int Servings { get; set; }
        [JsonProperty("extendedIngredients")]
        public List<Ingredient> Ingredients { get; set; }
        [JsonProperty("preparationMinutes")]
        public int PreparationMinutes { get; set; }
        [JsonProperty("cookingMinutes")]
        public int CookingMinutes { get; set; }
        [JsonProperty("image")]
        public string Image { get; set; }
        [JsonProperty("creditsText")]
        public string CreditsText { get; set; }
        [JsonProperty("summary")]
        public string Summary { get; set; }
        [JsonProperty("instructions")]
        public string Instructions { get; set; }
        [JsonProperty("sourceUrl")]
        public string SourceUrl { get; set; }
        [JsonProperty("vegan")]
        public bool Vegan { get; set; }
        [JsonProperty("glutenFree")]
        public bool GlutenFree { get; set; }
        [JsonProperty("dairyFree")]
        public bool DairyFree { get; set; }
        [JsonProperty("veryPopular")]
        public bool VeryPopular { get; set; }

        public int ReadyInMinutes => PreparationMinutes + CookingMinutes;
        public string PreparationMinutesText => PreparationMinutes + " minutes";
        public string CookingMinutesText => CookingMinutes + " minutes";
        public string ReadyInMinutesText => ReadyInMinutes + " minutes";
    }
}