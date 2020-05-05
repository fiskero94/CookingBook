using System;
using System.Collections.Generic;
using System.Text;

namespace CookingBook.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public string Unit { get; set; }
    }
}
