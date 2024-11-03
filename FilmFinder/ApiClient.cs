﻿using FilmFinder.Models;
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
            var response = await _httpClient.GetAsync($"{BaseUrl}/films");
            response.EnsureSuccessStatusCode();
            /*
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        var films = JsonConvert.DeserializeObject<List<Film>>(jsonResponse);
                        return films;
            */
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
            return apiResponse?.Films;

        }

        public async Task<Film> GetMovieAsync(int movieId)
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
            var movie = JsonConvert.DeserializeObject<Film>(jsonResponse,settings);
            return movie;
        }
    }
}
