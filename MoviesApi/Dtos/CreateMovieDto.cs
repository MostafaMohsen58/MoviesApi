namespace MoviesApi.Dtos
{
    public class CreateMovieDto : MovieDto
    {
        public IFormFile Poster { get; set; }
    }
}
