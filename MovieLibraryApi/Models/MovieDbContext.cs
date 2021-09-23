using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieLibraryApi.Models
{
    public class MovieDbContext:DbContext
    {
        private readonly IConfiguration _configuration;

        public MovieDbContext()
        {

        }

        public MovieDbContext(DbContextOptions<MovieDbContext> options)
            : base(options)
        {
            
        }

        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserFavouriteMovie> UserFavourtieMovies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=MoviesDatabase;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserFavouriteMovie>()
            .HasKey(t => new { t.UserId, t.MovieId });

            modelBuilder.Entity<UserFavouriteMovie>()
                .HasOne(pt => pt.User)
                .WithMany(p => p.FavMovies)
                .HasForeignKey(pt => pt.UserId);

            modelBuilder.Entity<UserFavouriteMovie>()
                .HasOne(pt => pt.Movie)
                .WithMany(t => t.UserFavouriteMovies)
                .HasForeignKey(pt => pt.MovieId);
        }

    }
}
