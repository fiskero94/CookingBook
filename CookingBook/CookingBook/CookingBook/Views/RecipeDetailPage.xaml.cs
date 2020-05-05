using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CookingBook.Models;
using CookingBook.ViewModels;

namespace CookingBook.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class RecipeDetailPage : ContentPage
    {
        RecipeDetailViewModel viewModel;

        public RecipeDetailPage(RecipeDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public RecipeDetailPage()
        {
            InitializeComponent();

            var item = new Recipe
            {
                Title = "Item 1"
            };

            viewModel = new RecipeDetailViewModel(item);
            BindingContext = viewModel;
        }
    }
}