using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace FilmFinder.Models
{
    public class Country
    {
        public int Id { get; set; }
        [JsonProperty("country")]
        public string CountryName { get; set; }
    }
}
