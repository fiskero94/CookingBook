using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookingBook.Models
{
    public class RecipeMatches
    {
        [JsonProperty("results")]
        public List<RecipeMatch> Matches { get; set; }
    }
}
