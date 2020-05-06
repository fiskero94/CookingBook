using CookingBook.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookingBook.Views
{
    [DesignTimeVisible(false)]
    public partial class LeftoversPage : ContentPage
    {
        private IngredientController ingredientController;
        private List<string> ingredients;

        public LeftoversPage()
        {
            InitializeComponent();
            ingredientController = new IngredientController();
            ingredients = new List<string>();
            Entry.TextChanged += Entry_TextChangedAsync;
            SearchButton.Clicked += SearchButton_ClickedAsync;
        }

        private async void SearchButton_ClickedAsync(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new LeftoversResultsPage(ingredients)));
        }

        private async void Entry_TextChangedAsync(object sender, TextChangedEventArgs e)
        {
            List<string> autocomplete = await ingredientController.AutocompleteIngredientSearchAsync(Entry.Text, 10);
            Suggestions.Children.Clear();
            foreach (string suggestion in autocomplete)
            {
                Button button = new Button { Text = suggestion };
                button.Clicked += SuggestionButton_Clicked;
                Suggestions.Children.Add(button);
            }
        }

        private void SuggestionButton_Clicked(object sender, EventArgs e)
        {
            string suggestion = ((Button)sender).Text;
            ingredients.Add(suggestion);
            Button button = new Button { Text = suggestion };
            button.Clicked += IngredientButton_Clicked;
            Ingredients.Children.Add(button);
        }

        private void IngredientButton_Clicked(object sender, EventArgs e)
        {
            string ingredient = ((Button)sender).Text;
            ingredients.Remove(ingredient);
            Ingredients.Children.Remove((Button)sender);
        }
    }
}