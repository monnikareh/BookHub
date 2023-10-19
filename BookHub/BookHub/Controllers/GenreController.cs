using BookHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;

namespace BookHub.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly BookHubDbContext _context;

        public GenreController(BookHubDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreDetail>>> GetGenres()
        {
            if (_context.Genres == null)
            {
                return NotFound();
            }

            return (await _context.Genres
                    .Include(g => g.Books)
                    .ToListAsync())
                .Select(ControllerHelpers.MapGenreToGenreDetail)
                .ToList();
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<GenreDetail>> GetGenreById(int id)
        {
            if (_context.Genres == null)
            {
                return NotFound();
            }

            var genre = await _context
                .Genres
                .Include(g => g.Books)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (genre == null)
            {
                return NotFound($"Genre with ID:'{id}' not found");
            }

            return ControllerHelpers.MapGenreToGenreDetail(genre);
        }

        // GET: api/Book/name
        [HttpGet("GetByName/{name}")]
        public async Task<ActionResult<GenreDetail>> GetGenreByName(string name)
        {
            if (_context.Genres == null)
            {
                return NotFound();
            }

            var genre = await _context
                .Genres
                .Include(g => g.Books)
                .FirstOrDefaultAsync(g => g.Name == name);


            if (genre == null)
            {
                return NotFound($"Genre '{name}' not found");
            }

            return ControllerHelpers.MapGenreToGenreDetail(genre);
        }

        [HttpPost]
        public async Task<ActionResult<GenreDetail>> PostGenre(GenreCreate genreCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid!");
            }

            if (_context.Genres == null)
            {
                return Problem("Entity set 'BookHubDbContext.Genres'  is null.");
            }


            var genre = new Genre
            {
                Name = genreCreate.Name,
            };

            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
            return ControllerHelpers.MapGenreToGenreDetail(genre);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            if (_context.Genres == null)
            {
                return NotFound();
            }

            var genre = await _context.Genres.FindAsync(id);
            if (genre == null)
            {
                return NotFound();
            }

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GenreExists(int id)
        {
            return (_context.Genres?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}