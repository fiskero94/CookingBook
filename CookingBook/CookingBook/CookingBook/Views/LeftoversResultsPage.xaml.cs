using CookingBook.Controller;
using CookingBook.Models;
using CookingBook.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookingBook.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LeftoversResultsPage : ContentPage
    {
        private readonly LeftoversResultsViewModel _viewModel;

        public LeftoversResultsPage(List<string> ingredients)
        {
            InitializeComponent();
            BindingContext = _viewModel = new LeftoversResultsViewModel(ingredients);
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            RecipeMatch recipeMatch = args.SelectedItem as RecipeMatch;
            
            if (recipeMatch != null)
            {
                await Navigation.PushAsync(new RecipeDetailPage(new RecipeDetailViewModel(recipeMatch.Recipe)));
                RecipeMatchesListView.SelectedItem = null;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_viewModel.RecipeMatches.Count == 0)
            {
                _viewModel.LoadRecipeMatchesCommand.Execute(null);
            }
        }
    }
}