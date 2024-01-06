using BusinessLayer.Errors;
using BusinessLayer.Models;

namespace BusinessLayer.Services;

public interface IGenreService
{
    Task<IEnumerable<GenreDetail>> GetGenresAsync(string? name);
    Task<Result<GenreDetail, string>> GetGenreByIdAsync(int id);
    Task<GenreDetail> CreateGenreAsync(GenreCreate genreCreate);
    Task<Result<GenreDetail, string>> UpdateGenreAsync(int id, GenreCreate genreUpdate);
    Task<Result<bool, string>> DeleteGenreAsync(int id);
}