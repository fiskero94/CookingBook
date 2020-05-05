using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookingBook.Models
{
    public class RecipePreview
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
