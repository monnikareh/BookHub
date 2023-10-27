using BookHub.Models;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly BookHubDbContext _context;

        public AuthorController(BookHubDbContext context)
        {
            _context = context;
        }

        
        [HttpGet("GetAuthors")]
        public async Task<ActionResult<IEnumerable<AuthorDetail>>> GetAuthors()
        {
            if (_context.Authors == null)
            {
                return NotFound();
            }
            return (await _context.Authors
                    .Include(a => a.Books)
                    .ToListAsync())
                .Select(ControllerHelpers.MapAuthorToAuthorDetail)
                .ToList();
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<AuthorDetail>> GetAuthorById(int id)
        {
            if (_context.Authors == null)
            {
                return NotFound();
            }

            var author = await _context
                .Authors
                .Include(a => a.Books)
                .ThenInclude(b => b.Publisher)
                .Include(a => a.Books)
                .ThenInclude(b => b.Genres)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (author == null)
            {
                return NotFound($"Author with ID:'{id}' not found");
            }

            return ControllerHelpers.MapAuthorToAuthorDetail(author);
        }

        // GET: api/Book/name
        [HttpGet("GetByName/{name}")]
        public async Task<ActionResult<AuthorDetail>> GetAuthorByName(string name)
        {
            if (_context.Authors == null)
            {
                return NotFound();
            }

            var author = await _context
                .Authors
                .Include(a => a.Books)
                .ThenInclude(b => b.Publisher)
                .Include(a => a.Books)
                .ThenInclude(b => b.Genres)
                .FirstOrDefaultAsync(b => b.Name == name);

            if (author == null)
            {
                return NotFound($"Author '{name}' not found");
            }

            return ControllerHelpers.MapAuthorToAuthorDetail(author);
        }

        [HttpPost("CreateAuthor")]
        public async Task<ActionResult<AuthorDetail>> PostAuthor(AuthorCreate authorCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid!");
            }

            if (_context.Authors == null)
            {
                return Problem("Entity set 'BookHubDbContext.Authors'  is null.");
            }


            var author = new Author()
            {
                Name = authorCreate.Name,
            };

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return ControllerHelpers.MapAuthorToAuthorDetail(author);
        }
        
        [HttpPut("UpdateAuthor/{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, AuthorDetail authorDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid!");
            }

            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound($"Author with ID {id} not found");
            }

            author.Name = authorDetail.Name;

            if (authorDetail.Books != null && authorDetail.Books.Count != 0)
            {
                author.Books.Clear();
                foreach (var bookRelatedModel in authorDetail.Books)
                {
                    var book = await _context.Books.FirstOrDefaultAsync(b =>
                        b.Name == bookRelatedModel.Name || b.Id == bookRelatedModel.Id);
                    if (book == null)
                    {
                        return NotFound(
                            $"Book 'Name={bookRelatedModel.Name}' <OR> 'ID={bookRelatedModel.Id}' could not be found");
                    }

                    author.Books.Add(book);
                }
            }
            
            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem($"Error updating author: {ex.Message}");
            }
        }
        
        [HttpDelete("DeleteAuthor/{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            if (_context.Authors == null)
            {
                return NotFound();
            }

            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuthorExists(int id)
        {
            return (_context.Authors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}