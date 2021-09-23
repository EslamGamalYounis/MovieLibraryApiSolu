using Microsoft.EntityFrameworkCore;
using MovieLibraryApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieLibraryApi.Repositories
{
    public class MovieRepo : IMovieRepository
    {
        public MovieDbContext DbContext { get; set; }

        public MovieRepo(MovieDbContext movieDbContext)
        {
            this.DbContext = movieDbContext;
        }
        public IEnumerable<Movie> GetAll()
        {
            var Movies = DbContext.Movies.ToList();
            return Movies;
        }

        public Movie GetById(int id)
        {
            var movie = DbContext.Movies.Find(id);
            return movie;
        }
        public void Add(Movie movie)
        {
            DbContext.Movies.Add(movie);
            DbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var movie = DbContext.Movies.Find(id);
            DbContext.Movies.Remove(movie);
            DbContext.SaveChanges();
        }

        public void Edit(Movie movie)
        {
            DbContext.Entry(movie).State = EntityState.Modified;
            DbContext.SaveChanges();
        }

    }
}
