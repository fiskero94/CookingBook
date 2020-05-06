using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CookingBook.Models;
using CookingBook.Controller;

namespace CookingBook.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class AddRecipePage : ContentPage
    {
        public Recipe Item { get; set; }
        private List<string> ingredients;
        private IngredientController ingredientController;

        public AddRecipePage()
        {
            InitializeComponent();

            Item = new Recipe { };
            ingredientController = new IngredientController();
            ingredients = new List<string>();
            ingredientsEntry.TextChanged += ingredientsEntry_TextChangedAsync;
            BindingContext = this;
        }

        #region INGREDIENTS 
        private async void ingredientsEntry_TextChangedAsync(object sender, TextChangedEventArgs e)
        {
            List<string> autocomplete = await ingredientController.AutocompleteIngredientSearchAsync(ingredientsEntry.Text, 10);
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
        #endregion

        async void Save_Clicked(object sender, EventArgs e)
        {
            Item.Title = title.Text;
            Item.Instructions = Instructions.Text;
            Item.Summary = summary.Text;
            Item.Ingredients = ingredients;
            Item.PreparationMinutes = Int32.Parse(preparationtime.Text);
            Item.CookingMinutes = Int32.Parse(cookingtime.Text);
            Item.CreditsText = "me";
            Item.Vegan = vegan.IsChecked;
            Item.GlutenFree = gluten.IsChecked;
            Item.DairyFree = dairy.IsChecked;

            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}