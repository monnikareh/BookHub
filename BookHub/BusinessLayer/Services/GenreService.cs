using BookHub.Models;
using BusinessLayer.Exceptions;
using BusinessLayer.Mapper;
using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;

namespace BusinessLayer.Services;

public class GenreService : IGenreService
{
    private readonly BookHubDbContext _context;

    public GenreService(BookHubDbContext context)
    {
        _context = context;
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
        return await genres.Select(g => EntityMapper.MapGenreToGenreDetail(g)).ToListAsync();
    }

    public async Task<GenreDetail> GetGenreByIdAsync(int id)
    {
        var genre = await _context
            .Genres
            .Include(g => g.Books)
            .FirstOrDefaultAsync(g => g.Id == id);

        if (genre == null)
        {
            throw new GenreNotFoundException($"Genre 'ID={id}' could not be found");
        }

        return EntityMapper.MapGenreToGenreDetail(genre);
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

    public async Task<GenreDetail> UpdateGenreAsync(int id, GenreUpdate genreUpdate)
    {
        var genre = await _context.Genres.Include(g => g.Books)
            .FirstOrDefaultAsync(g => g.Id == id);
        if (genre == null)
        {
            throw new GenreNotFoundException($"Genre 'ID={id}' could not be found");
        }
        genre.Name = genreUpdate.Name;
        
        await _context.SaveChangesAsync();   
        return EntityMapper.MapGenreToGenreDetail(genre);
    }

    public async Task DeleteGenreAsync(int id)
    {
        var genre = await _context.Genres.FindAsync(id);
        if (genre == null)
        {
            throw new GenreNotFoundException($"Genre 'ID={id}' could not be found");
        }
        _context.Genres.Remove(genre);
        await _context.SaveChangesAsync();
    }
}