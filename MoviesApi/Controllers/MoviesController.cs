using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Services;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {

        private readonly IMovieService _MovieService;
        private readonly IGenreService _GenreService;
        private readonly IMapper _Mapper;

        private new List<string> _allowedExtensions = new List<string> {".jpg",".png" };
        private long _maxAllowedPosterSize = 1048576;
        public MoviesController(IMovieService MovieService, IGenreService GenreService, IMapper Mapper)
        {
            _MovieService = MovieService;
            _GenreService = GenreService;
            _Mapper = Mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var movies = await _MovieService.GetAll();

            var movieDetails = _Mapper.Map<IEnumerable<MovieDetailsDto>>(movies);

            return Ok(movieDetails);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var movie = await _MovieService.GetById(id);

            if (movie == null)
                return NotFound();

            var dto = _Mapper.Map<MovieDetailsDto>(movie);

            return Ok(dto);
        }

        [HttpGet("GetMoviesByGenreId")]
        public async Task<IActionResult> GetMoviesByGenreIdAsync(byte genreId)
        {
            var movies = await _MovieService.GetAll(genreId);

            var movieDetails = _Mapper.Map<IEnumerable<MovieDetailsDto>>(movies);

            return Ok(movieDetails);
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm]CreateMovieDto dto)
        {
            if (!_allowedExtensions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return BadRequest("Only .png and .jpg images are allowed");

            if (dto.Poster.Length > _maxAllowedPosterSize)
                return BadRequest("max allowed size for poster is 1MB");

            var isValidGenre = await _GenreService.IsValidGenreId(dto.GenreId);
            if (!isValidGenre)
                return BadRequest("Invalid Genre Id");

            using var dataStream = new MemoryStream();

            await dto.Poster.CopyToAsync(dataStream);

            var movie = _Mapper.Map<Movie>(dto);
            movie.Poster=dataStream.ToArray();

            await _MovieService.Add(movie);

            return Ok(movie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id ,[FromForm]UpdateMovieDto dto)
        {
            var movie= await _MovieService.GetById(id);
            if (movie == null)
                return BadRequest($"Invalod Movie ID {id}");

            var isValid = await _GenreService.IsValidGenreId(dto.GenreId);
            if (!isValid)
                return BadRequest($"Invalid Genre Id {id}");

            if (dto.Poster != null)
            {
                if (!_allowedExtensions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                    return BadRequest("Only .png and .jpg images are allowed");

                if (dto.Poster.Length > _maxAllowedPosterSize)
                    return BadRequest("max allowed size for poster is 1MB");

                using var dataStream = new MemoryStream();
                await dto.Poster.CopyToAsync(dataStream);

                movie.Poster = dataStream.ToArray();
            }
            movie.Title = dto.Title;
            movie.Year = dto.Year;
            movie.Storeline = dto.Storeline;
            movie.Rate = dto.Rate;
            movie.GenreId = dto.GenreId;

            _MovieService.Update(movie);

            return Ok(movie);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var movie = await _MovieService.GetById(id);

            if (movie == null)
                return NotFound($"No Movie was found with id : {id}");

            _MovieService.Delete(movie);

            return Ok(movie);
        }

    }
}
