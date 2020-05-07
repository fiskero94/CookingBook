using System;
using System.Collections.Generic;
using SQLite;
using System.Text;
using CookingBook.Models;
using System.Threading.Tasks;

namespace CookingBook.Data
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Recipe>().Wait();
        }

        #region Database CRUD
        public Task<List<Recipe>> GetAllRecipesAsync()
        {
            return _database.Table<Recipe>().ToListAsync();
        }

        public Task<Recipe> GetRecipeAsync(int id)
        {
            return _database.Table<Recipe>()
                .Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveRecipeAsync(Recipe recipe)
        {
            if(recipe.Id != 0)
            {
                return _database.UpdateAsync(recipe);
            }
            else
            {
                return _database.InsertAsync(recipe);
            }
        }

        public Task<int> DeleteNoteAsync(Recipe recipe)
        {
            return _database.DeleteAsync(recipe);
        }

        #endregion
    }
}
