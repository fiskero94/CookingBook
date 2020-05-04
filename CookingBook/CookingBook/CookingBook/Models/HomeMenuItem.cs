using System;
using System.Collections.Generic;
using System.Text;

namespace CookingBook.Models
{
    public enum MenuItemType
    {
        Recipes,
        Leftovers
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
