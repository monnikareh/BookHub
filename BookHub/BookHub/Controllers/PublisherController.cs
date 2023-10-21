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
    public class PublisherController : ControllerBase
    {
        private readonly BookHubDbContext _context;

        public PublisherController(BookHubDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetPublishers")]
        public async Task<ActionResult<IEnumerable<PublisherDetail>>> GetPublishers()
        {
            if (_context.Publishers == null)
            {
                return NotFound();
            }

            return (await _context.Publishers
                    .Include(p => p.Books)
                    .ToListAsync())
                .Select(ControllerHelpers.MapPublisherToPublisherDetail)
                .ToList();
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<PublisherDetail>> GetPublisherById(int id)
        {
            if (_context.Publishers == null)
            {
                return NotFound();
            }

            var publisher = await _context
                .Publishers
                .Include(p => p.Books)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (publisher == null)
            {
                return NotFound($"Publisher with ID:'{id}' not found");
            }

            return ControllerHelpers.MapPublisherToPublisherDetail(publisher);
        }

        // GET: api/Book/name
        [HttpGet("GetByName/{name}")]
        public async Task<ActionResult<PublisherDetail>> GetGenreByName(string name)
        {
            if (_context.Publishers == null)
            {
                return NotFound();
            }

            var publisher = await _context
                .Publishers
                .Include(p => p.Books)
                .FirstOrDefaultAsync(p => p.Name == name);


            if (publisher == null)
            {
                return NotFound($"Publisher '{name}' not found");
            }

            return ControllerHelpers.MapPublisherToPublisherDetail(publisher);
        }

        [HttpPost("CreatePublisher")]
        public async Task<ActionResult<PublisherDetail>> PostGenre(PublisherCreate publisherCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid!");
            }

            if (_context.Publishers == null)
            {
                return Problem("Entity set 'BookHubDbContext.Publisher'  is null.");
            }


            var publisher = new Publisher
            {
                Name = publisherCreate.Name,
            };

            _context.Publishers.Add(publisher);
            await _context.SaveChangesAsync();
            return ControllerHelpers.MapPublisherToPublisherDetail(publisher);
        }
        
        [HttpPut("UpdatePublisher/{id}")]
        public async Task<IActionResult> UpdatePublisher(int id, PublisherDetail publisherDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid!");
            }

            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher == null)
            {
                return NotFound($"Publisher with ID {id} not found");
            }

            publisher.Name = publisherDetail.Name;

            if (publisherDetail.Books != null && publisherDetail.Books.Count != 0)
            {
                publisher.Books.Clear();
                foreach (var bookRelatedModel in publisherDetail.Books)
                {
                    var book = await _context.Books.FirstOrDefaultAsync(b =>
                        b.Name == bookRelatedModel.Name || b.Id == bookRelatedModel.Id);
                    if (book == null)
                    {
                        return NotFound(
                            $"Book 'Name={bookRelatedModel.Name}' <OR> 'ID={bookRelatedModel.Id}' could not be found");
                    }

                    publisher.Books.Add(book);
                }
            }
            
            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem($"Error updating publisher: {ex.Message}");
            }
        }


        [HttpDelete("DeletePublisher/{id}")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            if (_context.Publishers == null)
            {
                return NotFound();
            }

            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }

            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PublisherExists(int id)
        {
            return (_context.Publishers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
