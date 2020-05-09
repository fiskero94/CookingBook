using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CookingBook.Models;
using CookingBook.Views;
using CookingBook.ViewModels;

namespace CookingBook.Views
{
    [DesignTimeVisible(false)]
    public partial class RecipesPage : ContentPage
    {
        RecipesViewModel viewModel;

        private bool SearchingCheck = false;
        public RecipesPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new RecipesViewModel(PageNumberLabel);
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Recipe;
            if (item == null)
                return;

            await Navigation.PushAsync(new RecipeDetailPage(new RecipeDetailViewModel(item)));

            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AddRecipePage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

        private async void PageBackButton_ClickedAsync(object sender, EventArgs e) => await viewModel.PageBackButton_ClickedAsync();
        private async void PageNextButton_ClickedAsync(object sender, EventArgs e) => await viewModel.PageNextButton_ClickedAsync();
        private void ToolbarSearch_Clicked(object sender, EventArgs e) => viewModel.ToolbarSearch_Clicked(Searchbar);
        private async void Searchbar_SearchButtonPressedAsync(object sender, EventArgs e)
        {
            SearchingCheck = true;
            await viewModel.Searchbar_Search(((SearchBar)sender).Text);
        }

        private void Searchbar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (((SearchBar)sender).Text == "" && SearchingCheck == true)
            {
                SearchingCheck = false;
                viewModel.LoadItemsCommand.Execute(null);
            }
        }
    }
}