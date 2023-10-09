using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookHub.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Microsoft.IdentityModel.Tokens;

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
        public async Task<ActionResult<IEnumerable<BookModel>>> GetBooks()
        {
            if (_context.Books == null)
            {
                return NotFound();
            }

            var books = await _context.Books
                .Include(g => g.Genre)
                .Include(b => b.Publisher)
                .Include(b => b.Authors)
                .Select(b => ControllerHelpers.MapBookToBookModel(b))
                .ToListAsync();
            return books;
        }

        // GET: api/Book/5
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<BookModel>> GetBookById(int id)
        {
            if (_context.Books == null)
            {
                return NotFound();
            }

            var book = await _context
                .Books
                .Include(g => g.Genre)
                .Include(b => b.Publisher)
                .Include(b => b.Authors)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return NotFound($"Book with ID:'{id}' not found");
            }

            return ControllerHelpers.MapBookToBookModel(book);
        }

        // GET: api/Book/name
        [HttpGet("GetByName/{name}")]
        public async Task<ActionResult<BookModel>> GetBookByName(string name)
        {
            if (_context.Books == null)
            {
                return NotFound();
            }

            var book = await _context
                .Books
                .Include(g => g.Genre)
                .Include(b => b.Publisher)
                .Include(b => b.Authors)
                .FirstOrDefaultAsync(b => b.Name == name);

            if (book == null)
            {
                return NotFound($"Book '{name}' not found");
            }

            return ControllerHelpers.MapBookToBookModel(book);
        }


        [HttpGet("GetByGenreName/{genreName}")]
        public async Task<ActionResult<IEnumerable<BookModel>>> GetBookByGenreName(string genreName)
        {
            if (_context.Books == null)
            {
                return NotFound();
            }

            var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Name == genreName);
            if (genre == null)
            {
                return NotFound($"Genre '{genreName}' not found found");
            }

            var books = await _context
                .Books
                .Include(g => g.Genre)
                .Include(b => b.Publisher)
                .Include(b => b.Authors)
                .Where(b => b.GenreId == genre.Id)
                .ToListAsync();

            if (books == null)
            {
                return NotFound($"No books in genre '{genreName}' found");
            }

            return books.Select(ControllerHelpers.MapBookToBookModel).ToList();
        }


        [HttpGet("GetByPublisherName/{publisherName}")]
        public async Task<ActionResult<IEnumerable<BookModel>>> GetBookByPublisherName(string publisherName)
        {
            if (_context.Books == null)
            {
                return NotFound();
            }

            var publisher = await _context.Publishers.FirstOrDefaultAsync(p => p.Name == publisherName);
            if (publisher == null)
            {
                return NotFound($"Publisher '{publisherName}' not found");
            }

            var books = await _context
                .Books
                .Include(g => g.Genre)
                .Include(b => b.Publisher)
                .Include(b => b.Authors)
                .Where(b => b.PublisherId == publisher.Id)
                .ToListAsync();

            if (books == null)
            {
                return NotFound($"No books published by '{publisherName}' found");
            }

            return books.Select(ControllerHelpers.MapBookToBookModel).ToList();
        }

        [HttpGet("GetByAuthorName/{authorName}")]
        public async Task<ActionResult<IEnumerable<BookModel>>> GetBookByAuthorName(string authorName)
        {
            if (_context.Books == null)
            {
                return NotFound();
            }

            var author = await _context
                .Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.Name == authorName);
            if (author == null)
            {
                return NotFound($"No books written by '{authorName}' found");
            }
            var books = await _context.Books
                .Include(g => g.Genre)
                .Include(b => b.Publisher)
                .Include(b => b.Authors)
                .Where(b => b.Authors.Contains(author))
                .Select(b => ControllerHelpers.MapBookToBookModel(b))
                .ToListAsync();

            return books;
        }

        [HttpPost]
        public async Task<ActionResult<BookModel>> PostBook(BookModel bookModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid!");
            }

            if (_context.Books == null)
            {
                return Problem("Entity set 'BookHubDbContext.Books'  is null.");
            }

            if (bookModel.Authors.IsNullOrEmpty())
            {
                return Problem("Field Authors is null or empty");
            }

            var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Name == bookModel.GenreName);
            if (genre == null)
            {
                return NotFound($"Genre '{bookModel.GenreName}' could not be found");
            }

            var publisher = await _context.Publishers.FirstOrDefaultAsync(p => p.Name == bookModel.PublisherName);
            if (publisher == null)
            {
                return NotFound($"Publisher '{bookModel.PublisherName}' could not be found");
            }

            var authors = new List<Author>();
            foreach (var authorModel in bookModel.Authors)
            {
                var author = await _context.Authors.FirstOrDefaultAsync(a => a.Name == authorModel.Name);
                if (author == null)
                {
                    return NotFound($"Author '{authorModel.Name}' could not be found");
                }

                authors.Add(author);
            }

            var book = new Book
            {
                Name = bookModel.Name,
                Authors = authors,
                Genre = genre,
                GenreId = genre.Id,
                Publisher = publisher,
                PublisherId = publisher.Id,
                Price = bookModel.Price,
                StockInStorage = bookModel.StockInStorage
            };
            foreach (var author in authors)
            {
                author.Books.Add(book);
            }

            publisher.Books.Add(book);
            genre.Books.Add(book);
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return ControllerHelpers.MapBookToBookModel(book);
        }

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

        // public ICollection<BookModel> MapBooksToBookModels(ICollection<Book> books)
        // {
        //     var bookModels = books.Select(b => new BookModel()
        //     {
        //         Name = b.Name,
        //         GenreName = b.Genre.Name,
        //         PublisherName = b.Publisher.Name,
        //         Authors = b.Authors.Select(a => new AuthorModel() { Name = a.Name }).ToList(),
        //         Price = b.Price,
        //         StockInStorage = b.StockInStorage
        //     }).ToList();
        //     return bookModels;
        // }
    }
}