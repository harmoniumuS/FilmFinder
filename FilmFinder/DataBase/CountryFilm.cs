using FilmFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmFinder.DataBase
{
    public class CountryFilm
    {
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public int FilmId { get; set; }
        public Film Film { get; set; }
    }
}
