using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CookingBook.Models;
using CookingBook.Controller;
using System.Net.Http;
using CookingBook.Data;
using System.Linq;
using Android.Graphics;
using Android.Hardware;

namespace CookingBook.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class AddRecipePage : ContentPage
    {
        public Recipe Recipe { get; set; }
        private List<Ingredient> ingredients;
        private IngredientController ingredientController;

        public AddRecipePage()
        {
            InitializeComponent();

            Recipe = new Recipe { };
            ingredientController = new IngredientController();
            ingredients = new List<Ingredient>();
            ingredientsEntry.TextChanged += IngredientsEntry_TextChangedAsync;
            BindingContext = this;
        }


        #region INGREDIENTS 
        private async void IngredientsEntry_TextChangedAsync(object sender, TextChangedEventArgs e)
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
        private async void SuggestionButton_Clicked(object sender, EventArgs e)
        {
            string a = await DisplayPromptAsync("Enter amount (In grams)", ((Button)sender).Text, "OK", "Cancel", keyboard: Keyboard.Numeric);
            if(a != null)
            {
                double amount = Double.Parse(a);
                string suggestion = ((Button)sender).Text;

                Ingredient ingredient = new Ingredient() { Name = suggestion, Amount = amount };
                ingredients.Add(ingredient);
                Button button = new Button { Text = suggestion + " " + a + "g" };
                button.Clicked += IngredientButton_Clicked;
                Ingredients.Children.Add(button);
            }
        }

        private void IngredientButton_Clicked(object sender, EventArgs e)
        {
            string ingredient = ((Button)sender).Text;
            Ingredient ing = ingredients.Where(x => x.Name == ingredient).FirstOrDefault();
            ingredients.Remove(ing);
            Ingredients.Children.Remove((Button)sender);
        }
        #endregion

        private async void CameraButton_Clicked(object sender, EventArgs e)
        {
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });

            if (photo != null)
                PhotoImage.Source = ImageSource.FromStream(() => { return photo.GetStream(); });

            var p = PhotoImage;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            if (title.Text.Length > 0 &&
                Instructions.Text.Length > 0 &&
                summary.Text.Length > 0 &&
                preparationtime.Text.Length > 0 &&
                cookingtime.Text.Length > 0 &&
                ingredients.Count > 0)
            {
                Recipe.Title = title.Text;
                Recipe.Instructions = Instructions.Text;
                Recipe.Summary = summary.Text;
                Recipe.Ingredients = ingredients;
                Recipe.PreparationMinutes = Int32.Parse(preparationtime.Text);
                Recipe.CookingMinutes = Int32.Parse(cookingtime.Text);
                Recipe.CreditsText = "User";              
                Recipe.Vegan = vegan.IsChecked;
                Recipe.GlutenFree = gluten.IsChecked;
                Recipe.DairyFree = dairy.IsChecked;
                Recipe.UserRecipe = true;
                
                await App.Database.SaveRecipeAsync(Recipe);

                MessagingCenter.Send(this, "AddRecipe", Recipe);
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Error", "Please fill out all empty fields", "OK");
            }
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}