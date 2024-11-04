using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilmFinder.Models;
using Microsoft.Extensions.Logging;


namespace FilmFinder.DataBase
{
    public class AppDbContext : DbContext
    {
        public DbSet<Film> Films { get; set; }
        public DbSet<Country> Countries { get; set; }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<CountryFilm> CountryFilm { get; set; }
        public DbSet<GenreFilm> GenreFilm { get; set; }
        public DbSet<FavoriteFilm> FavoriteFilms { get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbPath = @"C:\Users\Harmonium\source\repos\FilmFinder\FilmFinder\DataBase\films.db";
            optionsBuilder.UseSqlite($"Data Source={dbPath}")
                . LogTo(Console.WriteLine, LogLevel.Information); ;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CountryFilm>()
         .HasKey(cf => new { cf.CountryId, cf.FilmId });

            modelBuilder.Entity<GenreFilm>()
                .HasKey(gf => new { gf.GenreId, gf.FilmId });

            modelBuilder.Entity<CountryFilm>()
                .HasOne(cf => cf.Country)
                .WithMany()
                .HasForeignKey(cf => cf.CountryId);

            modelBuilder.Entity<GenreFilm>()
                .HasOne(gf => gf.Genre)
                .WithMany()
                .HasForeignKey(gf => gf.GenreId);
        }
    }
}
