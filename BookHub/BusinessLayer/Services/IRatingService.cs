using BusinessLayer.Models;

namespace BusinessLayer.Services;

public interface IRatingService
{
    Task<IEnumerable<RatingDetail>> GetRatingsAsync(int? userId, string? userName,
        int? bookId, string? bookName);
    Task<RatingDetail> GetRatingByIdAsync(int id);
    Task<RatingDetail> CreateRatingAsync(RatingCreate ratingCreate);
    Task<RatingDetail> UpdateRatingAsync(int id, RatingDetail ratingDetail);
    Task DeleteRatingAsync(int id);
}