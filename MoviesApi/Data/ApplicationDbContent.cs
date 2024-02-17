using Microsoft.EntityFrameworkCore;
using MoviesApi.Models;

namespace MoviesApi.Data
{
    public class ApplicationDbContent:DbContext
    {
        public ApplicationDbContent(DbContextOptions<ApplicationDbContent> options):base(options)
        {
            
        }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
    }
}
