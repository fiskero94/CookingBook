using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookingBook.Models
{
    public class RecipeMatch
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("usedIngredientCount")]
        public int UsedIngredientCount { get; set; }
        
        public Recipe Recipe { get; set; }

        public int IngredientCount => Recipe.Ingredients.Count;

        public double MatchPercentage => UsedIngredientCount / IngredientCount * 100;

        public string MatchPercentageText => MatchPercentage + "%";
    }
}
