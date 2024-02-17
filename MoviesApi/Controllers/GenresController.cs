using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Services;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreServices;
        public GenresController(IGenreService genreServices)
        {
            _genreServices = genreServices;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var genres = await _genreServices.GetAllGenres();

            return Ok(genres);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] GenreDto dto)
        {
            var genre = new Genre
            {
                Name = dto.Name,
            };

            await _genreServices.Add(genre);

            return Ok(genre);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(byte id,[FromBody] GenreDto dto)
        {
            var genre = await _genreServices.GetById(id);
            if (genre == null)
                return NotFound($"No Genre was found with id : {id}");

            genre.Name = dto.Name;
            _genreServices.Update(genre);

            return Ok(genre);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(byte id)
        {
            var genre = await _genreServices.GetById(id);

            if (genre == null)
                return NotFound($"No Genre was found with id : {id}");

            _genreServices.Delete(genre);

            return Ok(genre);
        }
    }
}
