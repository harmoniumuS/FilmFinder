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
using FilmFinder.DataBase;
using Microsoft.EntityFrameworkCore;
using System.Windows.Input;

namespace FilmFinder.ViewModel
{
    public class MovieViewModel
    {
        private readonly ApiClient _apiClient;
        private Film _movie;
        public Film _selectedMovie;
        private bool _isFavorite;
        private bool _showFavoritesOnly;
        public ObservableCollection<Film> Movies { get; set; } = new ObservableCollection<Film>();
        public ObservableCollection<Film> Favorites { get; set; } = new ObservableCollection<Film>();

        
        public bool ShowFavoritesOnly
        {
            get { return _showFavoritesOnly; }
            set
            {
                if (_showFavoritesOnly != value)
                {
                    _showFavoritesOnly = value;
                    OnPropertyChanged(nameof(ShowFavoritesOnly));
                    LoadMoviesAsync();
                }
            }
        }
       

        public Film SelectedMovie { 
            get { return _selectedMovie; }
            set 
            { 
                _selectedMovie = value;
                OnPropertyChanged(nameof(SelectedMovie));
                if (_selectedMovie!=null)
                {
                    CheckIsFavorite(_selectedMovie.Id);
                }
            }
        }
        public bool IsFavorite { get { return _isFavorite;} 
            set 
            {
                if (_isFavorite != null)
                {
                    _isFavorite = value;
                    OnPropertyChanged(nameof(IsFavorite));
                    OnPropertyChanged(nameof(FavoriteButtonText));
                }
            }
        }
        public string FavoriteButtonText => IsFavorite ? "Удалить из избранного" : "Добавить в избранное";
        public Film Movie 
        { get { return _movie; } 
            set 
            { 
                _movie = value;
                OnPropertyChanged(nameof(Movie));
            } 
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand ToggleFavoriteCommand { get; }
        public ICommand ToggleShowFavoritesCommand { get; }
        public MovieViewModel()
        {
            _apiClient = new ApiClient();
            LoadMoviesAsync();

            ToggleFavoriteCommand = new RelayCommand( async () => await ToggleFavoriteAsync(),  () => CanToggleFavorite());

            ToggleShowFavoritesCommand = new RelayCommand(() => ShowFavoritesOnly = !ShowFavoritesOnly,() => true);
        }

        public bool CanToggleFavorite()
        {
            bool canExecute = SelectedMovie != null;
            return canExecute;
        }

        public async Task LoadMoviesAsync()
        {

            try
            {
                List<Film> filmsResponse;
                if (ShowFavoritesOnly)
                {
                    filmsResponse = await _apiClient.GetFavoriteFilmsAsync();
                }
                else
                {
                    filmsResponse = await _apiClient.GetMoviesAsync();
                }
               
                Movies.Clear();
                foreach (var movie in filmsResponse)
                {
                    Movies.Add(movie);
                }

                if (Movies.Any())
                {
                    SelectedMovie = Movies.First();
                }
                else
                {
                    MessageBox.Show("Нет доступных фильмов.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке фильмов: {ex.Message}");
            }
        }
        public async Task ToggleFavoriteAsync()
        {
            if (SelectedMovie != null)
            {
                using (var db = new AppDbContext())
                {
                    if (IsFavorite)
                    {
                        var favoriteFilm = await db.FavoriteFilms.FirstOrDefaultAsync(f => f.Id == SelectedMovie.Id);
                        if (favoriteFilm != null)
                        {
                            db.FavoriteFilms.Remove(favoriteFilm);
                            await db.SaveChangesAsync();
                        }
                        IsFavorite = false;
                        MessageBox.Show("Фильм убран из избранного!");
                    }
                    else
                    {
                        var newFavorite = new FavoriteFilm { FilmId = SelectedMovie.Id };
                        db.FavoriteFilms.Add(newFavorite);
                        await db.SaveChangesAsync();
                        IsFavorite = true;
                        MessageBox.Show("Фильм добавлен в избранные!");
            }
                }
            }
            
        }
        public async Task LoadFilmDetails(int movieId)
        {
            SelectedMovie = await _apiClient.GetMovieAsync(movieId); 
            OnPropertyChanged(nameof(SelectedMovie)); 
        }
        public async Task AddToFavorites(int filmId)
        {
            await _apiClient.AddToFavorites(filmId);
            Favorites.Add(await _apiClient.GetMovieAsync(filmId));
        }

        private async void CheckIsFavorite(int filmId)
        {
            using (var db = new AppDbContext())
            {
                IsFavorite = await db.FavoriteFilms.AnyAsync(f => f.FilmId == filmId);
            }
        }
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
