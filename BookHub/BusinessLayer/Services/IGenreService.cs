using BookHub.Models;

namespace BusinessLayer.Services;

public interface IGenreService
{
    Task<IEnumerable<GenreDetail>> GetGenresAsync(string? name);
    Task<GenreDetail> GetGenreByIdAsync(int id);
    Task<GenreDetail> PostGenreAsync(GenreCreate genreCreate);
    Task<GenreDetail> UpdateGenreAsync(int id, GenreUpdate genreUpdate);
    Task DeleteGenreAsync(int id);
}