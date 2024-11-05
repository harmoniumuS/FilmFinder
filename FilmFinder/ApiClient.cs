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
using Newtonsoft.Json.Linq;
using FilmFinder.DataBase;
using Microsoft.EntityFrameworkCore;
using System.Windows;

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
        
        
        public async Task<List<Film>> GetMoviesAsync()
        {
            using (var db = new AppDbContext())
            {
                var cathedFilms = await db.Films.ToListAsync();
                if (cathedFilms.Any())
                {
                    return cathedFilms; 
                }
            }
            var response = await _httpClient.GetAsync($"{BaseUrl}/films");
            response.EnsureSuccessStatusCode();
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
            };

            var jsonResponse = await response.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine($"Response JSON: {jsonResponse}");
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(jsonResponse,settings);
            if (apiResponse?.Films == null)
            {
                throw new Exception("Не удалось получить фильмы из ответа API.");
            }
            using (var db = new AppDbContext())
            {
                db.Films.AddRange(apiResponse.Films);
                await db.SaveChangesAsync();
            }
            return apiResponse?.Films;

        }
        public async Task AddToFavorites(int filmId)
        {
            using (var db = new AppDbContext())
            {
                var favoriteFilm = new FavoriteFilm { FilmId = filmId };
                db.FavoriteFilms.Add(favoriteFilm);
                try
                {
                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    var innerException = ex.InnerException;
                    MessageBox.Show(innerException?.Message ?? "Неизвестная ошибка");
                }
            }
        }
        public async Task<List<Film>> GetFavoriteFilmsAsync()
        {
            using (var db = new AppDbContext())
            {
                return await db.FavoriteFilms.Include(f => f.Film).Select(f => f.Film).ToListAsync();
            }
        }
        public async Task<Film> GetMovieAsync(int filmId)
        {

            using (var db = new AppDbContext())
            {
                var cathedFilm = await db.Films.FindAsync(filmId);
                if (cathedFilm!=null)
                {
                    return cathedFilm;
                }
            }
           var response = await _httpClient.GetAsync($"{BaseUrl}/films/{filmId}");

            
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Ошибка при получении данных: {response.StatusCode}");
            }
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
            };

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var film = JsonConvert.DeserializeObject<Film>(jsonResponse,settings);
            return film;
        }
    }
}
