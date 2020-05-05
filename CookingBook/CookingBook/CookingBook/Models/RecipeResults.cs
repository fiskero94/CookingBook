using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookingBook.Models
{
    public class RecipeResults
    {
        [JsonProperty("results")]
        public List<RecipePreview> Results { get; set; }

        public string Ids
        {
            get
            {
                string ids = string.Empty;

                foreach (RecipePreview preview in Results)
                {
                    ids += preview.Id + ",";
                }

                return ids.TrimEnd(',');
            }
        }
    }
}
