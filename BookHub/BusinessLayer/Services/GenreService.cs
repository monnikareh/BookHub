using BusinessLayer.Errors;
using BusinessLayer.Exceptions;
using BusinessLayer.Mapper;
using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace BusinessLayer.Services;

public class GenreService : IGenreService
{
    private readonly BookHubDbContext _context;
    private readonly IMemoryCache _memoryCache;


    public GenreService(BookHubDbContext context, IMemoryCache memoryCache)
    {
        _context = context;
        _memoryCache = memoryCache;
    }

    public async Task<IEnumerable<GenreDetail>> GetGenresAsync(string? name)
    {
        var genres = _context.Genres
            .Include(g => g.Books)
            .AsQueryable();
        if (!string.IsNullOrEmpty(name))
        {
            genres = genres.Where(g => g.Name == name);
        }

        var filteredGenres = await genres.ToListAsync();
        return filteredGenres.Select(EntityMapper.MapGenreToGenreDetail);
    }

    public async Task<Result<GenreDetail, string>> GetGenreByIdAsync(int id)
    {
        var key = $"GenreById_{id}";
        if (_memoryCache.TryGetValue(key, out GenreDetail? cached) && cached is not null)
        {
            return cached;
        }

        var genre = await _context
            .Genres
            .Include(g => g.Books)
            .FirstOrDefaultAsync(g => g.Id == id);

        if (genre == null)
        {
            return ErrorMessages.GenreNotFound(id);
        }

        var mapped = EntityMapper.MapGenreToGenreDetail(genre);
        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(60));
        _memoryCache.Set(key, mapped, cacheEntryOptions);
        return mapped;
    }

    public async Task<GenreDetail> CreateGenreAsync(GenreCreate genreCreate)
    {
        var genre = new Genre
        {
            Name = genreCreate.Name,
        };

        _context.Genres.Add(genre);
        await _context.SaveChangesAsync();
        return EntityMapper.MapGenreToGenreDetail(genre);
    }

    public async Task<Result<GenreDetail, string>> UpdateGenreAsync(int id, GenreCreate genreUpdate)
    {
        var genre = await _context.Genres.Include(g => g.Books)
            .FirstOrDefaultAsync(g => g.Id == id);
        if (genre == null)
        {
            return ErrorMessages.GenreNotFound(id);
        }

        genre.Name = genreUpdate.Name;

        await _context.SaveChangesAsync();
        return EntityMapper.MapGenreToGenreDetail(genre);
    }

    public async Task<Result<bool, string>> DeleteGenreAsync(int id)
    {
        var genre = await _context.Genres.FindAsync(id);
        if (genre == null)
        {
            return ErrorMessages.GenreNotFound(id);
        }

        _context.Genres.Remove(genre);
        await _context.SaveChangesAsync();
        return true;
    }
}