using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmFinder.Models
{
    public class FavoriteFilm
    {
        public int Id { get; set; }
        public int FilmId { get; set; }
        public Film Film { get; set; }
    }
}
