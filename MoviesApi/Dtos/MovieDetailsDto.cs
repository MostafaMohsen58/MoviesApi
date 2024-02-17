namespace MoviesApi.Dtos
{
    public class MovieDetailsDto : MovieDto
    {
        public int Id { get; set; }

        public byte[] Poster { get; set; }
        public string GenreName { get; set; }
    }
}
