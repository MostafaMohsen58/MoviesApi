using Microsoft.EntityFrameworkCore;
using MoviesApi.Models;

namespace MoviesApi.Services
{
    public class MovieService : IMovieService
    {
        private readonly ApplicationDbContent _context;
        public MovieService(ApplicationDbContent context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Movie>> GetAll(byte genreId = 0)
        {
           return await _context.Movies.OrderByDescending(m => m.Rate)
                                       .Where(m=>m.GenreId==genreId || genreId==0)
                                       .Include(m => m.Genre)
                                       .ToListAsync();
        }

        public async Task<Movie> GetById(int id)
        {
            return await _context.Movies.Include(m=>m.Genre).SingleOrDefaultAsync(m=>m.Id==id);
        }

        public async Task Add(Movie movie)
        {
            await _context.AddAsync(movie);
            _context.SaveChanges();
        }

        public void Update(Movie movie)
        {
            _context.Update(movie);
            _context.SaveChanges();
        }

        public void Delete(Movie movie)
        {
            _context.Remove(movie);
            _context.SaveChanges();
        }

        
    }
}
