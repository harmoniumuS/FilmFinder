using FilmFinder.Models;
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
        private Movie _movie;
        public Movie _selectedMovie;
        public ObservableCollection<Movie> Movies { get; set; } = new ObservableCollection<Movie>();
        public ObservableCollection<Movie> Favorites { get; set; } = new ObservableCollection<Movie>();

        public Movie SelectedMovie { 
            get { return _selectedMovie; }
            set 
            { 
                _selectedMovie = value;
                OnPropertyChanged(nameof(SelectedMovie));
                if (_selectedMovie!=null)
                {
                    LoadMovieDetails(_selectedMovie.KinopoiskId);
                }
            }
        }
       
        public Movie Movie 
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
            try
            {
                var movies = await _apiClient.GetMoviesAsync();
                Movies.Clear();

                foreach (var movie in movies)
                {
                    Movies.Add(movie);
                }

                if (Movies.Any())
                {
                    SelectedMovie = Movies.First(); // Установка первого фильма как выбранного
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading movies: {ex.Message}");
            }
        }
        public async Task LoadMovieDetails(int movieId)
        {
            SelectedMovie = await _apiClient.GetMovieAsync(movieId); // Получение данных о фильме по ID
            OnPropertyChanged(nameof(SelectedMovie)); // Уведомление об изменении свойства
        }


        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
