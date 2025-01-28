using Microsoft.EntityFrameworkCore;
using MovieAPI.Models;

namespace MovieAPI.MovieContext
{
    public class MoviDbContext: DbContext
    {

        public MoviDbContext(DbContextOptions<MoviDbContext> options) : base(options) 
        {

        }

        public DbSet<Movie> Movies { get; set; } 
    }
}
