using System.Text.RegularExpressions;
using CookingBook.Models;

namespace CookingBook.ViewModels
{
    public class RecipeDetailViewModel : BaseViewModel
    {
        public Recipe Recipe { get; set; }
        public string CreditsText => "by " + Recipe.CreditsText;
        public string ServingsReadyInMinutesText => Recipe.Servings + " servings, " + Recipe.ReadyInMinutes + " minutes";
        public string IngredientsText
        {
            get
            {
                string ingredients = string.Empty;

                foreach (Ingredient ingredient in Recipe.Ingredients)
                {
                    ingredients += ingredient.Amount + " " + ingredient.Unit + " " + ingredient.Name + ", ";
                }

                return ingredients.TrimEnd(' ').TrimEnd(',');
            }
        }
        public string SummaryText => Regex.Replace(Recipe.Summary, "<.*?>", string.Empty);

        public RecipeDetailViewModel(Recipe recipe)
        {
            Recipe = recipe;
        }
    }
}