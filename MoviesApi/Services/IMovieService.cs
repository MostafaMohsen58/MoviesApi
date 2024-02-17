namespace MoviesApi.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetAll(byte genreId=0);
        Task<Movie> GetById(int id);
        Task Add(Movie movie);
        void Update(Movie movie);
        void Delete(Movie movie);


    }
}
