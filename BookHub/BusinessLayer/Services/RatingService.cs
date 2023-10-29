using BookHub.Models;
using BusinessLayer.Exceptions;
using BusinessLayer.Mapper;
using BusinessLayer.Models;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Entities;


namespace BusinessLayer.Services;

public class RatingService : IRatingService
{
    private readonly BookHubDbContext _context;

    public RatingService(BookHubDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<RatingDetail>> GetRatingsAsync(int? userId, string? userName,
        int? bookId, string? bookName)
    {
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

        return await ratings.Select(r => EntityMapper.MapRatingToRatingDetail(r)).ToListAsync();
    }

    public async Task<RatingDetail> GetRatingByIdAsync(int id)
    {

        var rating = await _context
            .Ratings
            .Include(r => r.User)
            .Include(r => r.Book)
            .FirstOrDefaultAsync(r => r.Id == id);
        if (rating == null)
        {
            throw new RatingNotFoundException($"Rating 'ID={id}' could not be found");
        }

        return EntityMapper.MapRatingToRatingDetail(rating);
    }


    public async Task<RatingDetail> PostRatingAsync(RatingCreate ratingCreate)
    {


        if (_context.Books == null)
        {
            throw new ContextNotFoundException("Entity set 'BookHubDbContext.Books'  is null.");
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == ratingCreate.User.Name
                                                                 || u.Id == ratingCreate.User.Id);
        if (user == null)
        {
            throw new UserNotFoundException(
                $"User Name={ratingCreate.User.Name}' <OR> 'ID={ratingCreate.User.Id}' could not be found");
        }

        var book = await _context.Books.FirstOrDefaultAsync(b => b.Name == ratingCreate.Book.Name
                                                                 || b.Id == ratingCreate.Book.Id);
        if (book == null)
        {
            throw new BookNotFoundException(
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
        return EntityMapper.MapRatingToRatingDetail(rating);
    }
    
    public async Task UpdateRatingAsync(int id, RatingDetail ratingDetail)
    {
        var rating = await _context.Ratings.FindAsync(id);
        if (rating == null)
        {
            throw new RatingNotFoundException($"Rating with ID {id} not found");
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
                throw new UserNotFoundException($"User with name '{ratingDetail.User.Name}' not found");
            }

            rating.User = user;
            rating.UserId = user.Id;
        }

        if (ratingDetail.Book.Name != "string")
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Name == ratingDetail.Book.Name);
            if (book == null)
            {
                throw new BookNotFoundException($"Book with name '{ratingDetail.Book.Name}' not found");
            }

            rating.Book = book;
            rating.BookId = book.Id;
        }
        await _context.SaveChangesAsync();
    }

    public async Task DeleteBookAsync(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            throw new BookNotFoundException($"Book with ID {id} not found");
        }
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
    }

}