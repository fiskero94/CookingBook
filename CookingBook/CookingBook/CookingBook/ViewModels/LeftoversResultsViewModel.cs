using CookingBook.Controller;
using CookingBook.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CookingBook.ViewModels
{
    public class LeftoversResultsViewModel : BaseViewModel
    {
        public RecipeController RecipeController { get; set; }
        public ObservableCollection<RecipeMatch> RecipeMatches { get; set; }
        public Command LoadRecipeMatchesCommand { get; set; }
        public List<string> Ingredients { get; set; }

        public LeftoversResultsViewModel(List<string> ingredients)
        {
            Title = "Leftovers Results";
            RecipeController = new RecipeController();
            RecipeMatches = new ObservableCollection<RecipeMatch>();
            Ingredients = ingredients;
            LoadRecipeMatchesCommand = new Command(async () => await ExecuteLoadRecipeMatchesCommand());
        }

        private async Task ExecuteLoadRecipeMatchesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                RecipeMatches.Clear();
                List<RecipeMatch> recipeMatches = await RecipeController.SearchRecipesByIngredientsAsync(Ingredients, 10);

                foreach (RecipeMatch match in recipeMatches)
                {
                    RecipeMatches.Add(match);
                }
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
    }
}
