using BookHub.Models;
using BusinessLayer.Mapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;

namespace BookHub.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly BookHubDbContext _context;

        public GenreController(BookHubDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetGenres")]
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

        [HttpPost("CreateGenre")]
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
        
        [HttpPut("UpdateGenre/{id}")]
        public async Task<IActionResult> UpdateGenre(int id, GenreDetail genreDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid!");
            }

            var genre = await _context.Genres.FindAsync(id);
            if (genre == null)
            {
                return NotFound($"Genre with ID {id} not found");
            }

            genre.Name = genreDetail.Name;

            if (genreDetail.Books != null && genreDetail.Books.Count != 0)
            {
                genre.Books.Clear();
                foreach (var bookRelatedModel in genreDetail.Books)
                {
                    var book = await _context.Books.FirstOrDefaultAsync(b =>
                        b.Name == bookRelatedModel.Name || b.Id == bookRelatedModel.Id);
                    if (book == null)
                    {
                        return NotFound(
                            $"Book 'Name={bookRelatedModel.Name}' <OR> 'ID={bookRelatedModel.Id}' could not be found");
                    }

                    genre.Books.Add(book);
                }
            }

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem($"Error updating genre: {ex.Message}");
            }
        }
        
        [HttpDelete("DeleteGenre/{id}")]
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