using BookHub.Models;
using BusinessLayer.Models;

namespace BusinessLayer.Services;

public interface IGenreService
{
    Task<IEnumerable<GenreDetail>> GetGenresAsync(string? name);
    Task<GenreDetail> GetGenreByIdAsync(int id);
    Task<GenreDetail> CreateGenreAsync(GenreCreate genreCreate);
    Task<GenreDetail> UpdateGenreAsync(int id, GenreUpdate genreUpdate);
    Task DeleteGenreAsync(int id);
}