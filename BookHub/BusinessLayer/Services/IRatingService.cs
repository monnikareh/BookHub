using BusinessLayer.Errors;
using BusinessLayer.Models;

namespace BusinessLayer.Services;

public interface IRatingService
{
    Task<IEnumerable<RatingDetail>> GetRatingsAsync(int? userId, string? userName,
        int? bookId, string? bookName);

    Task<Result<RatingDetail, string>> GetRatingByIdAsync(int id);
    Task<Result<RatingDetail, string>> CreateRatingAsync(RatingCreate ratingCreate);
    Task<Result<RatingDetail, string>> UpdateRatingAsync(int id, RatingUpdate ratingUpdate);
    Task<Result<bool, string>> DeleteRatingAsync(int id);
    Task<bool> ExistRatingForUser(int userId, int bookId);
}