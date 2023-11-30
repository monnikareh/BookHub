using BusinessLayer.Exceptions;
using BusinessLayer.Models;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet("GetGenres")]
        public async Task<ActionResult<IEnumerable<GenreDetail>>> GetGenres(string? name)
        {
            try
            {
                return Ok(await _genreService.GetGenresAsync(name));
            }
            catch (Exception e)
            {
                return HandleGenresException(e);
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<GenreDetail>> GetGenreById(int id)
        {
            try
            {
                return Ok(await _genreService.GetGenreByIdAsync(id));
            }
            catch (Exception e)
            {
                return HandleGenresException(e);
            }
        }

        [HttpPost("CreateGenre")]
        public async Task<ActionResult<GenreDetail>> CreateGenre(GenreCreate genreCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid!");
            }
            
            try
            {
                return Ok(await _genreService.CreateGenreAsync(genreCreate));
            }
            catch (Exception e)
            {
                return HandleGenresException(e);
            }
            
        }
        
        [HttpPut("UpdateGenre/{id}")]
        public async Task<IActionResult> UpdateGenre(int id, GenreCreate genreDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid!");
            }

            try
            {
                return Ok(await _genreService.UpdateGenreAsync(id, genreDetail));
            }
            catch (Exception e)
            {
                return HandleGenresException(e);
            }
        }
        
        [HttpDelete("DeleteGenre/{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            try
            {
                await _genreService.DeleteGenreAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                return HandleGenresException(e);
            }
        }

        private ActionResult HandleGenresException(Exception e)
        {
            return e is GenreNotFoundException or BookNotFoundException
                ? NotFound(e.Message)
                : Problem("Unknown problem occured");
        }
    }
}