using BookHub.Models;
using BusinessLayer.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Services;

namespace WebAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("GetBooks")]
        public async Task<ActionResult<IEnumerable<BookDetail>>> GetBooks(string? bookName, int? genreId,
            string? genreName,
            int? publisherId, string? publisherName, int? authorId, string? authorName)
        {
            try
            {
                return Ok(await _bookService.GetBooksAsync(bookName, genreId, genreName, publisherId,
                    publisherName, authorId, authorName));
            }
            catch (Exception e)
            {
                return HandleBookException(e);
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<BookDetail>> GetBookById(int id)
        {
            try
            {
                return Ok(await _bookService.GetBookByIdAsync(id));
            }
            catch (Exception e)
            {
                return HandleBookException(e);
            }
        }


        [HttpPost("CreateBook")]
        public async Task<ActionResult<BookDetail>> PostBook(BookCreate bookCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid!");
            }

            try
            {
                return Ok(await _bookService.CreateBookAsync(bookCreate));
            }
            catch (Exception e)
            {
                return HandleBookException(e);
            }
        }

        [HttpPut("UpdateBook/{id}")]
        public async Task<ActionResult> UpdateBook(int id, BookDetail bookDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid!");
            }

            try
            {
                return Ok(await _bookService.UpdateBookAsync(id, bookDetail));
            }
            catch (Exception e)
            {
                return HandleBookException(e);
            }
        }


        [HttpDelete("DeleteBook/{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                await _bookService.DeleteBookAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                return HandleBookException(e);
            }
        }

        private ActionResult HandleBookException(Exception e)
        {
            return e is PublisherNotFoundException or GenreNotFoundException or AuthorNotFoundException
                or BookNotFoundException
                ? NotFound(e.Message)
                : Problem(e is AuthorsEmptyException or EntityUpdateException
                    ? e.Message
                    : "Unknown problem occured");
        }
    }
}