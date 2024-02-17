namespace MoviesApi.Services
{
    public interface IGenreService
    {
        Task<IEnumerable<Genre>> GetAllGenres();
        Task<Genre> Add(Genre genre);
        Task<Genre> GetById(byte id);
        Genre Update(Genre genre);
        Genre Delete(Genre genre);
        Task<bool> IsValidGenreId(byte id);

    }
}
