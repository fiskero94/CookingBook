using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using CookingBook.Controller;

namespace CookingBook.Views
{
    [DesignTimeVisible(false)]
    public partial class LeftoversPage : ContentPage
    {
        private readonly IngredientController _ingredientController;
        private readonly List<string> _ingredients;

        public LeftoversPage()
        {
            InitializeComponent();
            _ingredientController = new IngredientController();
            _ingredients = new List<string>();
            Entry.TextChanged += Entry_TextChangedAsync;
            SearchButton.Clicked += SearchButton_ClickedAsync;
        }

        private async void SearchButton_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new LeftoversResultsPage(_ingredients)));
        }

        private async void Entry_TextChangedAsync(object sender, TextChangedEventArgs e)
        {
            List<string> autocomplete = await _ingredientController.AutocompleteIngredientSearchAsync(Entry.Text, 10);
            Suggestions.Children.Clear();
            foreach (string suggestion in autocomplete)
            {
                Button button = new Button { Text = suggestion, BackgroundColor = Color.FromHex("#5577AA"), TextColor = Color.White, Padding = 0, Margin = 4, FontSize = 11, HeightRequest = 32 };
                button.Clicked += SuggestionButton_Clicked;
                Suggestions.Children.Add(button);
            }
        }

        private void SuggestionButton_Clicked(object sender, EventArgs e)
        {
            string suggestion = ((Button)sender).Text;

            if (!_ingredients.Contains(suggestion))
            {
                _ingredients.Add(suggestion);
                Button button = new Button { Text = suggestion, BackgroundColor = Color.FromHex("#77AA55"), TextColor = Color.White, Padding = 0, Margin = 4, FontSize = 11, HeightRequest = 32 };
                button.Clicked += IngredientButton_Clicked;
                Ingredients.Children.Add(button);
            }
        }

        private void IngredientButton_Clicked(object sender, EventArgs e)
        {
            string ingredient = ((Button)sender).Text;
            _ingredients.Remove(ingredient);
            Ingredients.Children.Remove((Button)sender);
        }
    }
}