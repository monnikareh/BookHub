using BusinessLayer.Models;

namespace BusinessLayer.Services;

public interface IPublisherService
{ 
    Task<IEnumerable<PublisherDetail>> GetPublishersAsync(string? name);
    Task<PublisherDetail> GetPublisherByIdAsync(int id);
    Task<PublisherDetail> CreatePublisherAsync(PublisherCreate publisherCreate);
    Task<PublisherDetail> UpdatePublisherAsync(int id, PublisherUpdate publisherUpdate);
    Task DeletePublisherAsync(int id);
}