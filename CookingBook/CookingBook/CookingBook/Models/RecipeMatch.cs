using Newtonsoft.Json;
using System;

namespace CookingBook.Models
{
    public class RecipeMatch
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("usedIngredientCount")]
        public int UsedIngredientCount { get; set; }
        [JsonProperty("missedIngredientCount")]
        public int MissedIngredientCount { get; set; }
        public string MatchPercentageText { get; private set; } = "0% match";
        public string Subtext => MatchPercentageText + ", " + Recipe.ReadyInMinutesText;

        private Recipe _recipe;
        public Recipe Recipe
        {
            get
            {
                return _recipe;
            }
            set
            {
                _recipe = value;

                if (value != null)
                {
                    Recipe.Ingredients.RemoveAll(i => i.Id == null);
                    CalculateMatchPercentage();
                }
            }
        }

        private void CalculateMatchPercentage()
        {
            int total = UsedIngredientCount + MissedIngredientCount;
            
            if (total > 0)
            {
                MatchPercentageText = Math.Round((double)UsedIngredientCount / total * 100, 0) + "% match";
                // MatchPercentageText = "Hmm"; // string.Format("{{0:N2}%}", matchPercentage);
            }
            else
            {
                MatchPercentageText = "0%";
            }
        }
    }
}