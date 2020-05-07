using CookingBook.Data;
using CookingBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            DataManager manager = new DataManager();
            RecipeResults results = await manager.GetRecipesAsync(10, 0);
            
            foreach (Recipe recipe in results.Recipes)
            {
                Console.WriteLine(recipe.Title);
            }

            Console.ReadKey();
        }
    }
}
