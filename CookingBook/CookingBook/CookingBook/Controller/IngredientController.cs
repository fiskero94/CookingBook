using CookingBook.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CookingBook.Controller
{
    public class IngredientController
    {
        private DataManager dataManager;

        public IngredientController()
        {
            dataManager = new DataManager();
        }

        public async Task<List<string>> AutocompleteIngredientSearchAsync(string search, int number)
        {
            return await dataManager.AutocompleteIngredientSearchAsync(search, number);
        }
    }
}
