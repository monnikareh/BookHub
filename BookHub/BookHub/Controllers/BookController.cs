using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer;
using DataAccessLayer.Entities;

namespace BookHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookHubDbContext _context;

        public BookController(BookHubDbContext context)
        {
            _context = context;
        }

        // GET: api/Book
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            if (_context.Books == null)
            {
                return NotFound();
            }

            return await _context.Books.ToListAsync();
        }

        // GET: api/Book/5
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Book>> GetBookById(int id)
        {
            if (_context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // GET: api/Book/name
        [HttpGet("GetByName/{name}")]
        public async Task<ActionResult<Book>> GetBookByName(string name)
        {
            if (_context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FirstOrDefaultAsync(b => b.Name == name);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }


        [HttpGet("GetByGenreName/{genreName}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBookByGenreName(string genreName)
        {
            if (_context.Books == null)
            {
                return NotFound();
            }

            var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Name == genreName);
            if (genre == null)
            {
                return NotFound();
            }

            var books = await _context.Books.Where(b => b.GenreId == genre.Id).ToListAsync();

            if (books == null)
            {
                return NotFound();
            }

            return books;
        }


        [HttpGet("GetByPublisherName/{publisherName}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBookByPublisherName(string publisherName)
        {
            if (_context.Books == null)
            {
                return NotFound();
            }

            var publisher = await _context.Publishers.FirstOrDefaultAsync(p => p.Name == publisherName);
            if (publisher == null)
            {
                return NotFound();
            }

            var books = await _context.Books.Where(b => b.PublisherId == publisher.Id).ToListAsync();

            if (books == null)
            {
                return NotFound();
            }

            return books;
        }

        [HttpGet("GetByAuthorName/{authorName}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBookByAuthorName(string authorName)
        {
            if (_context.Books == null)
            {
                return NotFound();
            }

            var books = await _context
                .Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.Name == authorName);
            if (books == null)
            {
                return NotFound();
            }

            return books.Books.ToList();
        }

        // PUT: api/Book/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Book
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            if (_context.Books == null)
            {
                return Problem("Entity set 'BookHubDbContext.Books'  is null.");
            }

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBookById", new { id = book.Id }, book);
        }

        // DELETE: api/Book/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (_context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(int id)
        {
            return (_context.Books?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}