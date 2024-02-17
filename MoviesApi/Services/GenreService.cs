using Microsoft.EntityFrameworkCore;

namespace MoviesApi.Services
{
    public class GenreService : IGenreService
    {
        private readonly ApplicationDbContent _context;
        public GenreService(ApplicationDbContent context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Genre>> GetAllGenres()
        {
            return await _context.Genres.OrderBy(m => m.Name).ToListAsync();
        }

        public async Task<Genre> Add(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
            _context.SaveChanges();
            return genre;
        }

        public Genre Update(Genre genre)
        {
            _context.Update(genre);
            _context.SaveChanges();
            return genre;
        }

        public Genre Delete(Genre genre)
        {
            _context.Remove(genre);
            _context.SaveChanges();
            return genre;
        }

        public async Task<Genre> GetById(byte id)
        {
            return await _context.Genres.SingleOrDefaultAsync(g => g.Id == id);
        }

        public async Task<bool> IsValidGenreId(byte id)
        {
            return await _context.Genres.AnyAsync(g => g.Id == id);
        }
    }
}
