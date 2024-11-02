using FilmFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FilmFinder
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://kinopoiskapiunofficial.tech/api/v2.2/";
        private const string ApiKey = "4552d50f-9611-42ce-b35e-33d92e6a0d49";

        public ApiClient()
        { 
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("X-API-KEY", ApiKey);
        }
        
        public async Task<List<Movie>> GetMoviesAsync()
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/films");
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var movies = JsonConvert.DeserializeObject<List<Movie>>(jsonResponse);
            return movies;
        }

        public async Task<Movie> GetMovieAsync(int movieId)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/films/{movieId}");

            
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Ошибка при получении данных: {response.StatusCode}");
            }
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
            };

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var movie = JsonConvert.DeserializeObject<Movie>(jsonResponse,settings);
            return movie;
        }
    }
}
