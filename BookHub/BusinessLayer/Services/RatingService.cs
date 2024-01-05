using BusinessLayer.Exceptions;
using BusinessLayer.Mapper;
using BusinessLayer.Models;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Entities;
using Microsoft.Extensions.Caching.Memory;


namespace BusinessLayer.Services;

public class RatingService : IRatingService
{
    private readonly BookHubDbContext _context;
    private readonly IMemoryCache _memoryCache;


    public RatingService(BookHubDbContext context, IMemoryCache memoryCache)
    {
        _context = context;
        _memoryCache = memoryCache;
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
            ratings = ratings.Where(r => r.User != null && r.User.Id == user.Id);
        }

        var book = await _context.Books.FirstOrDefaultAsync(b => b.Name == bookName || b.Id == bookId);
        if (book != null)
        {
            ratings = ratings.Where(r => r.Book != null && r.Book.Id == book.Id);
        }

        var filteredRatings = await ratings.ToListAsync();
        return filteredRatings.Select(EntityMapper.MapRatingToRatingDetail);
    }

    public async Task<RatingDetail> GetRatingByIdAsync(int id)
    {
        var key = $"RatingById_{id}";
        if (_memoryCache.TryGetValue(key, out RatingDetail? cached) && cached is not null)
        {
            return cached;
        }

        var rating = await _context
            .Ratings
            .Include(r => r.User)
            .Include(r => r.Book)
            .FirstOrDefaultAsync(r => r.Id == id);
        if (rating == null)
        {
            throw new RatingNotFoundException($"Rating 'ID={id}' could not be found");
        }

        var mapped = EntityMapper.MapRatingToRatingDetail(rating);
        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(60));
        _memoryCache.Set(key, mapped, cacheEntryOptions);
        return mapped;
    }


    public async Task<RatingDetail> CreateRatingAsync(RatingCreate ratingCreate)
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

    public async Task<RatingDetail> UpdateRatingAsync(int id, RatingDetail ratingDetail)
    {
        var rating = await _context.Ratings.FindAsync(id);
        if (rating == null)
        {
            throw new RatingNotFoundException($"Rating with ID {id} not found");
        }

        rating.Value = ratingDetail.Value;

        if (ratingDetail.Comment != "string")
        {
            rating.Comment = ratingDetail.Comment;
        }

        if (ratingDetail.User.Name != "string")
        {
            var user = await _context.Users.FirstOrDefaultAsync(u =>
                u.Name == ratingDetail.User.Name || u.Id == ratingDetail.User.Id);
            if (user == null)
            {
                throw new UserNotFoundException(
                    $"User Name={ratingDetail.User.Name}' <OR> 'ID={ratingDetail.User.Id}' could not be found");
            }

            rating.User = user;
            rating.UserId = user.Id;
        }

        if (ratingDetail.Book.Name != "string")
        {
            var book = await _context.Books.FirstOrDefaultAsync(b =>
                b.Name == ratingDetail.Book.Name || b.Id == ratingDetail.Book.Id);
            if (book == null)
            {
                throw new BookNotFoundException(
                    $"Book 'Name={ratingDetail.Book.Name}' <OR> 'ID={ratingDetail.Book.Id}' could not be found");
            }

            rating.Book = book;
            rating.BookId = book.Id;
        }

        await _context.SaveChangesAsync();
        return EntityMapper.MapRatingToRatingDetail(rating);
    }

    public async Task DeleteRatingAsync(int id)
    {
        var rating = await _context.Ratings.FindAsync(id);
        if (rating == null)
        {
            throw new RatingNotFoundException($"Rating with ID {id} not found");
        }

        _context.Ratings.Remove(rating);
        await _context.SaveChangesAsync();
    }
}