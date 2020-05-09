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
        private int PageSize = 10;
        private Label PageNumberLabel;
        public int PageNumber = 1;
        private double TotalPages;
        private string SearchQuery;

        public RecipesViewModel(Label pageNumberLabel)
        {
            Title = "Recipes";
            Items = new ObservableCollection<Recipe>();
            RecipeController = new RecipeController();
            PageNumberLabel = pageNumberLabel;
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<AddRecipePage, Recipe>(this, "AddRecipe", (obj, item) =>
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

                if(PageNumber == 1)
                {
                    List<Recipe> UserRecipes = await App.Database.GetAllRecipesAsync();
                    foreach (var item in UserRecipes)
                    {
                        Items.Add(item);
                    }
                }

                RecipeResults recipeResults = await RecipeController.GetRecipesAsync(PageSize, GetOffset());
                foreach (var item in recipeResults.Recipes)
                {
                    Items.Add(item);
                }
                double totalPagesTemp = recipeResults.TotalResults / PageSize;
                TotalPages = Math.Ceiling(totalPagesTemp);
                UpdatePageNumberLabel();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                SearchQuery = null;
                IsBusy = false;
            }
        }

        public async Task PageBackButton_ClickedAsync()
        {
            if(PageNumber > 1)
            {
                PageNumber--;
                await NavigatePage();
            }
        }

        public async Task PageNextButton_ClickedAsync()
        {
            if(PageNumber < TotalPages)
            {
                PageNumber++;
                await NavigatePage();
            }
        }

        private async Task NavigatePage()
        {
            if (SearchQuery != null) await Searchbar_Search(SearchQuery);
            else await ExecuteLoadItemsCommand();
        }

        public async Task Searchbar_Search(string query)
        {
            if(SearchQuery == null) PageNumber = 1;
            SearchQuery = query;
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                Items.Clear();

                if(PageNumber == 1)
                {
                    List<Recipe> UserRecipes = await App.Database.GetAllRecipesAsync();
                    foreach (var item in UserRecipes)
                    {
                        if(item.Title.ToUpper().Contains(SearchQuery.ToUpper()))
                        {
                            Items.Add(item);
                        }
                    }
                }

                RecipeResults recipeResults = await RecipeController.SearchRecipesByNameAsync(PageSize, GetOffset(), SearchQuery);
                foreach (var item in recipeResults.Recipes)
                {
                    Items.Add(item);
                }
                double totalPagesTemp = recipeResults.TotalResults / PageSize;
                TotalPages = Math.Ceiling(totalPagesTemp);
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

        public void ToolbarSearch_Clicked(SearchBar searchBar)
        {
            searchBar.IsVisible = !searchBar.IsVisible;
        }

        private int GetOffset() => (PageNumber - 1) * PageSize;
        private void UpdatePageNumberLabel() => PageNumberLabel.Text = "Page " + PageNumber + " out of " + TotalPages;
    }
}