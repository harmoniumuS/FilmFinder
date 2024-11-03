using FilmFinder.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmFinder
{
    public class ApiResponse
    {
        [JsonProperty("total")]
        public int Total { get; set; }
        [JsonProperty("items")]
        public List<Film> Films { get; set; }
        [JsonProperty("totalPages")]
        public int TotalPages { get; set; }
    }
}
