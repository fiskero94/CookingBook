using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookingBook.Models
{
    public class Autocomplete
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
