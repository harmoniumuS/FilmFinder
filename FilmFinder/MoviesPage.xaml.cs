using FilmFinder.Models;
using FilmFinder.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FilmFinder
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class MoviesPage : Page
    {
        private readonly MovieViewModel _viewModel;
        public MoviesPage(int movieId)
        {
            InitializeComponent();
            _viewModel = new MovieViewModel();
            LoadMovie(movieId);
            DataContext = _viewModel;
        }

        private async void LoadMovie(int movieId)
        {
            await _viewModel.LoadMovieDetails(movieId);
            MoviesListView.ItemsSource = _viewModel.Movies;
        }

        private void MoviesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MoviesListView.SelectedItem is Movie selectedMovie)
            {
                _viewModel.SelectedMovie = selectedMovie;
                MovieDetailsPanel.Visibility = Visibility.Visible;
                _viewModel.LoadMovieDetails(selectedMovie.KinopoiskId);
            }
            else
            {
                MovieDetailsPanel.Visibility = Visibility.Collapsed;
            }
        }
    }
}
