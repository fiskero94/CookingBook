using System;

using CookingBook.Models;

namespace CookingBook.ViewModels
{
    public class RecipeDetailViewModel : BaseViewModel
    {
        public Recipe Item { get; set; }
        public RecipeDetailViewModel(Recipe item = null)
        {
            //tbd
        }
    }
}
