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
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace BookHub.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookHubDbContext _context;

        public BookController(BookHubDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetBooks")]
        public async Task<ActionResult<IEnumerable<BookDetail>>> GetBooks(int? genreId, string? genreName,
            int? publisherId, string? publisherName, int? authorId, string? authorName)
        {
            if (_context.Books == null)
            {
                return NotFound();
            }

            var books = _context.Books
                .Include(g => g.Genres)
                .Include(b => b.Publisher)
                .Include(b => b.Authors)
                .AsQueryable();
            var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Name == genreName || g.Id == genreId);
            if (genre != null)
            {
                books = books.Where(b => b.Genres.Contains(genre));
            }

            var publisher =
                await _context.Publishers.FirstOrDefaultAsync(p => p.Name == publisherName || p.Id == publisherId);
            if (publisher != null)
            {
                books = books.Where(b => b.Publisher.Id == publisher.Id);
            }

            var author = await _context
                .Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.Name == authorName || a.Id == authorId);
            if (author != null)
            {
                books = books.Where(b => b.Authors.Contains(author));
            }

            return await books.Select(b => ControllerHelpers.MapBookToBookDetail(b)).ToListAsync();
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<BookDetail>> GetBookById(int id)
        {
            if (_context.Books == null)
            {
                return NotFound();
            }

            var book = await _context
                .Books
                .Include(g => g.Genres)
                .Include(b => b.Publisher)
                .Include(b => b.Authors)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return NotFound($"Book with ID:'{id}' not found");
            }

            return ControllerHelpers.MapBookToBookDetail(book);
        }

        [HttpGet("GetByName/{name}")]
        public async Task<ActionResult<BookDetail>> GetBookByName(string name)
        {
            if (_context.Books == null)
            {
                return NotFound();
            }

            var book = await _context
                .Books
                .Include(g => g.Genres)
                .Include(b => b.Publisher)
                .Include(b => b.Authors)
                .FirstOrDefaultAsync(b => b.Name == name);

            if (book == null)
            {
                return NotFound($"Book '{name}' not found");
            }

            return ControllerHelpers.MapBookToBookDetail(book);
        }


        [HttpPost("CreateBook")]
        public async Task<ActionResult<BookDetail>> PostBook(BookCreate bookCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid!");
            }

            if (_context.Books == null)
            {
                return Problem("Entity set 'BookHubDbContext.Books'  is null.");
            }

            if (bookCreate.Authors.IsNullOrEmpty())
            {
                return Problem("Field Authors is null or empty");
            }

            // var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Name == bookCreate.Genre.Name
            //                                                            || g.Id == bookCreate.Genre.Id);
            // if (genre == null)
            // {
            //     return NotFound(
            //         $"Genre 'Name={bookCreate.Genre.Name}' <OR> 'ID={bookCreate.Genre.Id}' could not be found");
            // }

            var publisher = await _context.Publishers.FirstOrDefaultAsync(p => p.Name == bookCreate.Publisher.Name
                                                                               || p.Id == bookCreate.Publisher.Id);
            if (publisher == null)
            {
                return NotFound(
                    $"Publisher 'Name={bookCreate.Publisher.Name}' <OR> 'ID={bookCreate.Publisher.Id}' could not be found");
            }

            var genres = new List<Genre>();
            foreach (var genreRelatedModel in bookCreate.Genres)
            {
                var genre = await _context.Genres.FirstOrDefaultAsync(g =>
                    g.Name == genreRelatedModel.Name || g.Id == genreRelatedModel.Id);
                if (genre == null)
                {
                    return NotFound(
                        $"Genre 'Name={genreRelatedModel.Name}' <OR> 'ID={genreRelatedModel.Id}' could not be found");
                }

                genres.Add(genre);
            }

            var authors = new List<Author>();
            foreach (var authorRelatedModel in bookCreate.Authors)
            {
                var author = await _context.Authors.FirstOrDefaultAsync(a =>
                    a.Name == authorRelatedModel.Name || a.Id == authorRelatedModel.Id);
                if (author == null)
                {
                    return NotFound(
                        $"Author 'Name={authorRelatedModel.Name}' <OR> 'ID={authorRelatedModel.Id}' could not be found");
                }

                authors.Add(author);
            }

            var book = new Book
            {
                Name = bookCreate.Name,
                Authors = authors,
                Genres = genres,
                Publisher = publisher,
                PublisherId = publisher.Id,
                Price = bookCreate.Price,
                StockInStorage = bookCreate.StockInStorage,
                OverallRating = bookCreate.OverallRating
            };
            // APPARENTLY NOT NECESSARY
            // foreach (var author in authors)
            // {
            //     author.Books.Add(book);
            // }

            // publisher.Books.Add(book);
            // genre.Books.Add(book);
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return ControllerHelpers.MapBookToBookDetail(book);
        }

        [HttpDelete("DeleteBook/{id}")]
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