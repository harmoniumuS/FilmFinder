using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FilmFinder.Models
{
    public class Genre
    {
        public int Id { get; set; }
        [JsonProperty("genre")]
        public string GenreName { get; set;}

    }
}
