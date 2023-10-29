using BookHub.Models;
using DataAccessLayer;
using BusinessLayer.Mapper;
using BusinessLayer.Exceptions;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

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

    public async Task<PublisherDetail> PostPublisherAsync(PublisherCreate publisherCreate)
    {
        var publisher = new Publisher
        {
            Name = publisherCreate.Name,
        };
        _context.Publishers.Add(publisher);
        await _context.SaveChangesAsync();
        return EntityMapper.MapPublisherToPublisherDetail(publisher);
    }
    
    public async Task UpdatePublisherAsync(int id, PublisherUpdate publisherUpdate)
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
            publisher.Books.Clear();
            foreach (var bookRelatedModel in publisherUpdate.Books)
            {
                var book = await _context.Books.FirstOrDefaultAsync(b =>
                    b.Name == bookRelatedModel.Name || b.Id == bookRelatedModel.Id);
                if (book == null)
                {
                    throw new BookNotFoundException(
                        $"Book 'Name={bookRelatedModel.Name}' <OR> 'ID={bookRelatedModel.Id}' could not be found");
                }

                publisher.Books.Add(book);
            }
        } 
        await _context.SaveChangesAsync();
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