using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CookingBook.Models;
using CookingBook.ViewModels;

namespace CookingBook.Views
{
    [DesignTimeVisible(false)]
    public partial class RecipeDetailPage : ContentPage
    {
        private RecipeDetailViewModel _viewModel;

        public RecipeDetailPage(RecipeDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }
    }
}