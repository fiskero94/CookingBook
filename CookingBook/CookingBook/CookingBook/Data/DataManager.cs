using CookingBook.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CookingBook.Data
{
    public class DataManager
    {
        private HttpClient Client;
        private static readonly string Key = "c527d279e53e4bb3b563f2f2284bb60b";
        private static readonly string BaseUrl = "https://api.spoonacular.com/";

        public DataManager()
        {
            Client = new HttpClient();
        }

        public async System.Threading.Tasks.Task<List<Recipe>> GetRecipesAsync(int number, int offset)
        {
            HttpResponseMessage response = await Client.GetAsync("https://api.spoonacular.com/recipes/search?apiKey=" + Key + "&number=" + number + "&offset=" + offset);
            string content = await response.Content.ReadAsStringAsync();
            RecipeResults results = JsonConvert.DeserializeObject<RecipeResults>(content);
            HttpResponseMessage response2 = await Client.GetAsync("https://api.spoonacular.com/recipes/informationBulk?ids=" + results.Ids + "&apiKey=" + Key);
            string content2 = await response2.Content.ReadAsStringAsync();
            List<Recipe> recipes = JsonConvert.DeserializeObject<List<Recipe>>(content2);
            return recipes;
        }

        public async Task<List<string>> AutocompleteIngredientSearchAsync(string search, int number)
        {
            HttpResponseMessage response = await Client.GetAsync($"{BaseUrl}food/ingredients/autocomplete?query={search}&number={number}&apiKey={Key}");
            string content = await response.Content.ReadAsStringAsync();
            List<Autocomplete> results = JsonConvert.DeserializeObject<List<Autocomplete>>(content);
            return results.Select(a => a.Name).ToList();
        }

        public async Task<List<RecipeMatch>> SearchRecipesByIngredientsAsync(List<string> ingredients, int number)
        {
            string ingredientsString = IngredientsToCommaSeperatedString(ingredients);
            HttpResponseMessage response = await Client.GetAsync($"{BaseUrl}recipes/findByIngredients?ingredients={ingredientsString}&number={number}&apiKey={Key}");
            string content = await response.Content.ReadAsStringAsync();
            List<RecipeMatch> matches = JsonConvert.DeserializeObject<List<RecipeMatch>>(content);
            string ids = GetIdsFromMatches(matches);
            // HttpResponseMessage response2 = await Client.GetAsync($"{BaseUrl}recipes/informationBulk?ids={ids}&apiKey={Key}");
            HttpResponseMessage response2 = await Client.GetAsync("https://api.spoonacular.com/recipes/informationBulk?ids=" + ids + "&apiKey=" + Key);
            string content2 = await response2.Content.ReadAsStringAsync();
            List<Recipe> recipes = JsonConvert.DeserializeObject<List<Recipe>>(content2);

            for (int i = 0; i < number; i++)
            {
                matches[i].Recipe = recipes[i];
            }

            return matches;
        }

        private string IngredientsToCommaSeperatedString(List<string> ingredients)
        {
            string ingredientsString = string.Empty;

            foreach (string ingredient in ingredients)
            {
                ingredientsString += ingredient + ",";
            }

            return ingredientsString.TrimEnd(',');
        }

        private string GetIdsFromMatches(List<RecipeMatch> matches)
        {
            string ids = string.Empty;

            foreach (RecipeMatch match in matches)
            {
                ids += match.Id + ",";
            }

            return ids.TrimEnd(',');
        }
    }
}
