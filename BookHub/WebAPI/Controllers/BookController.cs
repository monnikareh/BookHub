using BusinessLayer.Exceptions;
using BusinessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [Route("[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
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

        [HttpGet("{id}")]
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


        [HttpPost]
        public async Task<ActionResult<BookDetail>> CreateBook(BookCreate bookCreate)
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

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBook(int id, BookCreate bookUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid!");
            }

            try
            {
                return Ok(await _bookService.UpdateBookAsync(id, bookUpdate));
            }
            catch (Exception e)
            {
                return HandleBookException(e);
            }
        }


        [HttpDelete("{id}")]
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
                or BookNotFoundException or AuthorsEmptyException
                ? NotFound(e.Message)
                : Problem("Unknown problem occured");
        }
    }
}