using BookHub.Models;

namespace BusinessLayer.Services;

public interface IPublisherService
{ 
    Task<IEnumerable<PublisherDetail>> GetPublishersAsync(string? name);
    Task<PublisherDetail> GetPublisherByIdAsync(int id);
    Task<PublisherDetail> PostPublisherAsync(PublisherCreate publisherCreate);
    Task UpdatePublisherAsync(int id, PublisherUpdate publisherUpdate);
    Task DeletePublisherAsync(int id);
}