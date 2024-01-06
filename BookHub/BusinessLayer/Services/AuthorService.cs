using BusinessLayer.Exceptions;
using BusinessLayer.Mapper;
using BusinessLayer.Models;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NuGet.Packaging;

namespace BusinessLayer.Services;

public class AuthorService : IAuthorService
{
    private readonly BookHubDbContext _context;
    private readonly IMemoryCache _memoryCache;


    public AuthorService(BookHubDbContext context, IMemoryCache memoryCache)
    {
        _context = context;
        _memoryCache = memoryCache;
    }

    public async Task<IEnumerable<AuthorDetail>> GetAuthorsAsync(string? name, int? bookId, string? bookName)
    {
        var authors = _context.Authors
            .Include(a => a.Books)
            .AsQueryable();
        if (!string.IsNullOrEmpty(name))
        {
            authors = authors.Where(a => a.Name == name);
        }

        var book = await _context.Books.FirstOrDefaultAsync(b => b.Name == bookName || b.Id == bookId);
        if (book != null)
        {
            authors = authors.Where(o => o.Books.Contains(book));
        }

        var authorsList = await authors.ToListAsync();
        return authorsList.Select(EntityMapper.MapAuthorToAuthorDetail);
    }

    public async Task<AuthorDetail> GetAuthorByIdAsync(int id)
    {
        var key = $"AuthorById_{id}";
        if (_memoryCache.TryGetValue(key, out AuthorDetail? cached) && cached is not null)
        {
            return cached;
        }

        var author = await _context
            .Authors
            .Include(a => a.Books)
            .ThenInclude(b => b.Publisher)
            .Include(a => a.Books)
            .ThenInclude(b => b.Genres)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (author == null)
        {
            throw new AuthorNotFoundException($"Author 'ID={id}' could not be found");
        }

        var mapped = EntityMapper.MapAuthorToAuthorDetail(author);
        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(60));
        _memoryCache.Set(key, mapped, cacheEntryOptions);
        return mapped;
    }

    public async Task<AuthorDetail> CreateAuthorAsync(AuthorCreate authorCreate)
    {
        var author = new Author()
        {
            Name = authorCreate.Name,
        };
        _context.Authors.Add(author);
        await _context.SaveChangesAsync();
        return EntityMapper.MapAuthorToAuthorDetail(author);
    }

    public async Task<AuthorDetail> UpdateAuthorAsync(int id, AuthorUpdate authorUpdate)
    {
        var author = await _context.Authors.Include(o => o.Books)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (author == null)
        {
            throw new AuthorNotFoundException($"Author 'ID={id}' could not be found");
        }

        author.Name = authorUpdate.Name;

        if (authorUpdate.Books != null && authorUpdate.Books.Count != 0)
        {
            var bookNames = authorUpdate.Books.Select(a => a.Name).ToHashSet();
            var bookIds = authorUpdate.Books.Select(a => a.Id).ToHashSet();

            var books = await _context.Books
                .Where(b => bookNames.Contains(b.Name) || bookIds.Contains(b.Id))
                .ToListAsync();

            if (books.Count != authorUpdate.Books.Count)
            {
                throw new BookNotFoundException("One or more books could not be found");
            }

            author.Books.Clear();
            author.Books.AddRange(books);
        }

        await _context.SaveChangesAsync();
        return EntityMapper.MapAuthorToAuthorDetail(author);
    }

    public async Task DeleteAuthorAsync(int id)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author == null)
        {
            throw new AuthorNotFoundException($"Author 'ID={id}' could not be found");
        }

        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();
    }
}