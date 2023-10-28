using BookHub.Models;

namespace BusinessLayer.Services;

public interface IRatingService
{
    Task<IEnumerable<RatingDetail>> GetRatingsAsync(int? userId, string? userName,
        int? bookId, string? bookName);
    Task<RatingDetail> GetRatingByIdAsync(int id);
    Task<RatingDetail> PostRatingAsync(RatingCreate ratingCreate);
    Task UpdateRatingAsync(int id, RatingDetail ratingDetail);
    Task DeleteBookAsync(int id);
}