using Newtonsoft.Json;

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

        public int TotalIngredientCount => UsedIngredientCount + MissedIngredientCount;
        public double MatchPercentage => TotalIngredientCount > 0 ? (double)UsedIngredientCount / TotalIngredientCount * 100 : 0;
        public string MatchPercentageText => string.Format("{{0:N2}%}", MatchPercentage);

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
                }
            }
        }
    }
}