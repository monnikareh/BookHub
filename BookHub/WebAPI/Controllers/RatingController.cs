using BookHub.Models;
using DataAccessLayer;
using DataAccessLayer.Entities;
using BusinessLayer.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly BookHubDbContext _context;

        public RatingController(BookHubDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetRatings")]
        public async Task<ActionResult<IEnumerable<RatingDetail>>> GetRatings(int? userId, string? userName,
            int? bookId, string? bookName)
        {
            if (_context.Ratings == null)
            {
                return NotFound();
            }

            var ratings = _context.Ratings
                .Include(r => r.User)
                .Include(r => r.Book)
                .AsQueryable();
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == userName || u.Id == userId);
            if (user != null)
            {
                ratings = ratings.Where(r => r.User.Id == user.Id);
            }

            var book = await _context.Books.FirstOrDefaultAsync(b => b.Name == bookName || b.Id == bookId);
            if (book != null)
            {
                ratings = ratings.Where(r => r.Book.Id == book.Id);
            }

            return await ratings.Select(r => ControllerHelpers.MapRatingToRatingDetail(r)).ToListAsync();
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<RatingDetail>> GetRatingById(int id)
        {
            if (_context.Ratings == null)
            {
                return NotFound();
            }

            var rating = await _context
                .Ratings
                .Include(r => r.User)
                .Include(r => r.Book)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (rating == null)
            {
                return NotFound($"Rating with ID:'{id}' not found");
            }

            return ControllerHelpers.MapRatingToRatingDetail(rating);
        }


        [HttpPost("CreateRating")]
        public async Task<ActionResult<RatingDetail>> PostRating(RatingCreate ratingCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid!");
            }

            if (_context.Books == null)
            {
                return Problem("Entity set 'BookHubDbContext.Books'  is null.");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == ratingCreate.User.Name
                                                                     || u.Id == ratingCreate.User.Id);
            if (user == null)
            {
                return NotFound(
                    $"User Name={ratingCreate.User.Name}' <OR> 'ID={ratingCreate.User.Id}' could not be found");
            }

            var book = await _context.Books.FirstOrDefaultAsync(b => b.Name == ratingCreate.Book.Name
                                                                     || b.Id == ratingCreate.Book.Id);
            if (book == null)
            {
                return NotFound(
                    $"Book 'Name={ratingCreate.Book.Name}' <OR> 'ID={ratingCreate.Book.Id}' could not be found");
            }

            var rating = new Rating
            {
                UserId = user.Id,
                User = user,
                BookId = book.Id,
                Book = book,
                Value = ratingCreate.Value,
                Comment = ratingCreate.Comment
            };
            var bookRatings = _context.Ratings
                .Where(r => r.Book.Id == book.Id);
            book.OverallRating = (bookRatings.Sum(r => r.Value) + ratingCreate.Value) 
                / (bookRatings.Count() + 1);
            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();
            return ControllerHelpers.MapRatingToRatingDetail(rating);
        }
        
        [HttpPut("UpdateRating/{id}")]
        public async Task<ActionResult> UpdateRating(int id, RatingDetail ratingDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid!");
            }

            var rating = await _context.Ratings.FindAsync(id);
            if (rating == null)
            {
                return NotFound($"Rating with ID {id} not found");
            }

            rating.Value = ratingDetail.Value;

            if (rating.Comment != "string")
            {
                rating.Comment = ratingDetail.Comment;
            }

            if (ratingDetail.User.Name != "string")
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == ratingDetail.User.Name);
                if (user == null)
                {
                    return NotFound($"User with name '{ratingDetail.User.Name}' not found");
                }

                rating.User = user;
                rating.UserId = user.Id;
            }

            if (ratingDetail.Book.Name != "string")
            {
                var book = await _context.Books.FirstOrDefaultAsync(b => b.Name == ratingDetail.Book.Name);
                if (book == null)
                {
                    return NotFound($"Book with name '{ratingDetail.Book.Name}' not found");
                }

                rating.Book = book;
                rating.BookId = book.Id;
            }

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem($"Error updating rating: {ex.Message}");
            }
        }

        [HttpDelete("DeleteRating/{id}")]
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