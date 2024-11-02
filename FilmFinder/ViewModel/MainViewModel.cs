using FilmFinder.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmFinder.ViewModel
{
    public class MainViewModel
    {
        public ObservableCollection<Movie> Movies { get; set;}
        public ObservableCollection<Movie> Favorites { get; set; }

        public MainViewModel()
        {
            Movies = new ObservableCollection<Movie>();
            Favorites = new O

        }

    }
}
