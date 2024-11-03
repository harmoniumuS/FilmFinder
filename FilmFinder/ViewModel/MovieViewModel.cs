﻿using FilmFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;

namespace FilmFinder.ViewModel
{
    public class MovieViewModel
    {
        private readonly ApiClient _apiClient;
        private Film _movie;
        public Film _selectedMovie;
        public ObservableCollection<Film> Movies { get; set; } = new ObservableCollection<Film>();
        public ObservableCollection<Film> Favorites { get; set; } = new ObservableCollection<Film>();
        private int _requestCount = 0;
        private const int _maxRequests = 100; 
        private const int _timeFrameSeconds = 60; 

        public Film SelectedMovie { 
            get { return _selectedMovie; }
            set 
            { 
                _selectedMovie = value;
                OnPropertyChanged(nameof(SelectedMovie));
                if (_selectedMovie!=null)
                {
                    LoadFilmDetails(_selectedMovie.KinopoiskId);
                }
            }
        }
       
        public Film Movie 
        { get { return _movie; } 
            set 
            { 
                _movie = value;
                OnPropertyChanged(nameof(Movie));
            } 
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public MovieViewModel()
        {
            _apiClient = new ApiClient();
            LoadMoviesAsync();
        }

        public async Task LoadMoviesAsync()
        {
            if (_requestCount >= _maxRequests)
            {
                MessageBox.Show("Достигнут лимит запросов. Пожалуйста, подождите.");
                return;
            }
            try
            {
                var filmsResponse = await _apiClient.GetMoviesAsync();
                Movies.Clear();

                foreach (var movie in filmsResponse)
                {
                    Movies.Add(movie);
                    await Task.Delay(1000);
                }

                if (Movies.Any())
                {
                    SelectedMovie = Movies.First();
                }
                else
                {
                    MessageBox.Show("Нет доступных фильмов.");
                }
                _requestCount++;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading movies: {ex.Message}");
            }
            finally
            {
                
                await Task.Delay(TimeSpan.FromSeconds(_timeFrameSeconds));
                _requestCount = 0;
            }
        }
        public async Task LoadFilmDetails(int movieId)
        {
            SelectedMovie = await _apiClient.GetMovieAsync(movieId); 
            OnPropertyChanged(nameof(SelectedMovie)); 
        }


        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
