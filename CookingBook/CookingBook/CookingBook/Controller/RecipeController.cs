using CookingBook.Data;
using CookingBook.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CookingBook.Controller
{
    public class RecipeController
    {
        private DataManager dataManager;

        public RecipeController()
        {
            dataManager = new DataManager();
        }

        public async System.Threading.Tasks.Task<List<Recipe>> GetRecipesAsync(int number, int offset)
        {
            return await dataManager.GetRecipesAsync(number, offset);
        }

        public async Task<List<RecipeMatch>> SearchRecipesByIngredientsAsync(List<string> ingredients, int number)
        {
            return await dataManager.SearchRecipesByIngredientsAsync(ingredients, number);
        }
    }
}
