using FilmFinder.DataBase;
using FilmFinder.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;
using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MovieViewModel _viewModel;
        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MovieViewModel();
            DataContext = _viewModel;
        }


        private void ShowFavorites_Click(object sender, RoutedEventArgs e)
        {
            var favoritesFilmsPage = new FavoritesFilms();
            var favoritesViewModel = new FavoritesViewModel();

            favoritesFilmsPage.DataContext = favoritesViewModel;

            MainFrame.Navigate(favoritesFilmsPage);
        }
    }
}