using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CookingBook.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Servings { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public int PreparationMinutes { get; set; }
        public int CookingMinutes { get; set; }
        public ImageSource Image { get; set; }
        public string CreditsText { get; set; }
        public string Summary { get; set; }
        public string Instructions { get; set; }
        public string SourceUrl { get; set; }
        public Dictionary<string, bool> RecipeTags = new Dictionary<string, bool>
        {
            { "vegan", false },
            { "glutenFree", false },
            { "dairyFree", false },
            { "veryPopular", false },
        };
        public int ReadyInMinutes => PreparationMinutes + CookingMinutes;
    }
}