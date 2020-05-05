using CookingBook.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace CookingBook
{
    public class API
    {
        private HttpClient Client;
        private string Key = "24d41fab91e74391b6acb1cee3c86983";

        public API()
        {
            Client = new HttpClient();
        }

        public async System.Threading.Tasks.Task<List<Recipe>> GetRecipesAsync()
        {
            HttpResponseMessage response = await Client.GetAsync("https://api.spoonacular.com/recipes/search?apiKey=" + Key);
            string content = await response.Content.ReadAsStringAsync();
            RecipeResults results = JsonConvert.DeserializeObject<RecipeResults>(content);
            HttpResponseMessage response2 = await Client.GetAsync("https://api.spoonacular.com/recipes/informationBulk?ids=" + results.Ids + "&apiKey=" + Key);
            string content2 = await response2.Content.ReadAsStringAsync();
            List<Recipe> recipes = JsonConvert.DeserializeObject<List<Recipe>>(content2);
            return recipes;
        }
    }
}
