using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookingBook.Models;

namespace CookingBook.Services
{
    public class MockDataStore : IDataStore<Recipe>
    {
        readonly List<Recipe> items;

        public MockDataStore()
        {
            items = new List<Recipe>()
            {
                new Recipe { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description." },
                new Recipe { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is an item description." },
                new Recipe { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is an item description." },
                new Recipe { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is an item description." },
                new Recipe { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description="This is an item description." },
                new Recipe { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description="This is an item description." }
            };
        }

        public async Task<bool> AddItemAsync(Recipe item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Recipe item)
        {
            var oldItem = items.Where((Recipe arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Recipe arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Recipe> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Recipe>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}