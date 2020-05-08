using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using CookingBook.Models;
using CookingBook.Views;
using CookingBook.Controller;
using System.Collections.Specialized;
using System.Linq;
using System.Collections.Generic;

namespace CookingBook.ViewModels
{
    public class RecipesViewModel : BaseViewModel
    {
        public ObservableCollection<Recipe> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public RecipeController RecipeController { get; set; }
        private int pageSize = 10;
        private Label pageNumberLabel;
        private int pageNumber = 1;
        private double totalPages;

        public RecipesViewModel(Label PageNumberLabel)
        {
            Title = "Recipes";
            Items = new ObservableCollection<Recipe>();
            RecipeController = new RecipeController();
            pageNumberLabel = PageNumberLabel;
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<AddRecipePage, Recipe>(this, "AddRecipe", async (obj, item) =>
            {
                var newRecipe = item as Recipe;
                Items.Insert(0, newRecipe);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                Items.Clear();

                List<Recipe> UserRecipes = await App.Database.GetAllRecipesAsync();
                foreach (var item in UserRecipes)
                {
                    Items.Add(item);
                }

                RecipeResults recipeResults = await RecipeController.GetRecipesAsync(pageSize, GetOffset());
                foreach (var item in recipeResults.Recipes)
                {
                    Items.Add(item);
                }
                double totalPagesTemp = recipeResults.TotalResults / pageSize;
                totalPages = Math.Ceiling(totalPagesTemp);
                UpdatePageNumberLabel();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private int GetOffset() => (pageNumber - 1) * pageSize;

        private void UpdatePageNumberLabel() => pageNumberLabel.Text = "Page " + pageNumber + " out of " + totalPages;

        public async Task PageBackButton_ClickedAsync()
        {
            if(pageNumber > 1)
            {
                pageNumber--;
                await ExecuteLoadItemsCommand();
            }
        }

        public async Task PageNextButton_ClickedAsync()
        {
            if(pageNumber < totalPages)
            {
                pageNumber++;
                await ExecuteLoadItemsCommand();
            }
        }
    }
}