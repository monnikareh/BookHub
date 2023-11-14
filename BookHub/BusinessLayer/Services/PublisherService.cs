using BookHub.Models;
using DataAccessLayer;
using BusinessLayer.Mapper;
using BusinessLayer.Exceptions;
using BusinessLayer.Models;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;

namespace BusinessLayer.Services;

public class PublisherService : IPublisherService
{
    private readonly BookHubDbContext _context;

    public PublisherService(BookHubDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PublisherDetail>> GetPublishersAsync(string? name)
    {
        var publishers = _context.Publishers
            .Include(p => p.Books)
            .AsQueryable();
        if (!string.IsNullOrEmpty(name))
        {
            publishers = publishers.Where(p => p.Name == name);
        }
        return await publishers.Select(p => EntityMapper.MapPublisherToPublisherDetail(p)).ToListAsync();
    }

    public async Task<PublisherDetail> GetPublisherByIdAsync(int id)
    {
        var publisher = await _context
            .Publishers
            .Include(p => p.Books)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (publisher == null)
        {
            throw new PublisherNotFoundException($"Publisher with ID:'{id}' not found");
        }
        return EntityMapper.MapPublisherToPublisherDetail(publisher);
    }

    public async Task<PublisherDetail> CreatePublisherAsync(PublisherCreate publisherCreate)
    {
        var publisher = new Publisher
        {
            Name = publisherCreate.Name,
        };
        _context.Publishers.Add(publisher);
        await _context.SaveChangesAsync();
        return EntityMapper.MapPublisherToPublisherDetail(publisher);
    }
    
    public async Task<PublisherDetail> UpdatePublisherAsync(int id, PublisherUpdate publisherUpdate)
    {
        var publisher = await _context.Publishers.Include(g => g.Books)
            .FirstOrDefaultAsync(g => g.Id == id);
        
        if (publisher == null)
        {
            throw new PublisherNotFoundException($"Publisher with ID {id} not found");
        }

        publisher.Name = publisherUpdate.Name;

        if (publisherUpdate.Books.Count != 0)
        {
            var bookNames = publisherUpdate.Books.Select(b => b.Name).ToHashSet();
            var bookIds = publisherUpdate.Books.Select(b => b.Id).ToHashSet();

            var books = await _context.Books
                .Where(b => bookNames.Contains(b.Name) || bookIds.Contains(b.Id))
                .ToListAsync();

            if (books.Count != publisherUpdate.Books.Count)
            {
                throw new BookNotFoundException("One or more books could not be found");
            }

            publisher.Books.Clear();
            publisher.Books.AddRange(books);
        } 
        await _context.SaveChangesAsync();
        return EntityMapper.MapPublisherToPublisherDetail(publisher);
    }
    
    public async Task DeletePublisherAsync(int id)
    {
        var publisher = await _context.Publishers.FindAsync(id);
        if (publisher == null)
        {
            throw new PublisherNotFoundException($"Publisher with ID:'{id}' not found");
        }
        _context.Publishers.Remove(publisher);
        await _context.SaveChangesAsync();
    }

}