using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CookingBook.Models;

namespace CookingBook.Data
{
    public class DataManager
    {
        private HttpClient _client;
        private static readonly string Key = "24d41fab91e74391b6acb1cee3c86983";
        private static readonly string BaseUrl = "https://api.spoonacular.com/";

        public DataManager()
        {
            _client = new HttpClient();
        }

        public async Task<RecipeResults> GetRecipesAsync(int number, int offset)
        {
            RecipeResults results = await Request<RecipeResults>("recipes/search?number=" + number + "&offset=" + offset + "&apiKey=" + Key);
            results.Recipes = await Request<List<Recipe>>("recipes/informationBulk?ids=" + results.Ids + "&apiKey=" + Key);
            return results;
        }

        public async Task<List<string>> AutocompleteIngredientSearchAsync(string search, int number)
        {
            List<Autocomplete> results = await Request<List<Autocomplete>>("food/ingredients/autocomplete?query=" + search + "&number=" + number + "&apiKey=" + Key);
            return results.Select(a => a.Name).ToList();
        }

        public async Task<List<RecipeMatch>> SearchRecipesByIngredientsAsync(List<string> ingredients, int number)
        {
            List<RecipeMatch> matches = await Request<List<RecipeMatch>>("recipes/findByIngredients?ingredients=" + ingredients.Separate(',') + "&number=" + number + "&apiKey=" + Key);

            if (matches.Count > 0)
            {
                string ids = matches.Select(m => m.Id).Separate(',');
                List<Recipe> recipes = await Request<List<Recipe>>("recipes/informationBulk?ids=" + ids + "&apiKey=" + Key);
                matches.ForEach(m => m.Recipe = recipes.SingleOrDefault(r => r.Id == m.Id));
            }

            return matches;
        }

        public async Task<RecipeResults> SearchRecipesByNameAsync(int number, int offset, string query)
        {
            RecipeResults results = await Request<RecipeResults>("recipes/search?number=" + number + "&offset=" + offset + "&query=" + query + "&apiKey=" + Key);
            results.Recipes = await Request<List<Recipe>>("recipes/informationBulk?ids=" + results.Ids + "&apiKey=" + Key);
            return results;
        }

        public async Task<T> Request<T>(string query)
        {
            HttpResponseMessage response = await _client.GetAsync(BaseUrl + query);

            if (!response.IsSuccessStatusCode)
            {
                throw new RequestException(response.ReasonPhrase, response);
            }

            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}