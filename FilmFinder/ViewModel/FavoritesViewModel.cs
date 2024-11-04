﻿using FilmFinder.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FilmFinder.ViewModel
{
    public class FavoritesViewModel
    {
        private readonly ApiClient _apiClient;
        public ObservableCollection<Film> FavoriteFilms { get; set; } = new ObservableCollection<Film>();

        public FavoritesViewModel()
        { 
            _apiClient = new ApiClient();
            LoadFavoriteFilmsAsync();
        }

        public async Task LoadFavoriteFilmsAsync()
        {
            var favoriteFilms = await _apiClient.GetFavoriteFilmsAsync();
            FavoriteFilms.Clear();

            foreach (var film in favoriteFilms)
            { 
                FavoriteFilms.Add(film);
            }
        }

    }
}