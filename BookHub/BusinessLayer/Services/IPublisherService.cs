using BookHub.Models;

namespace BusinessLayer.Services;

public interface IPublisherService
{ 
    Task<IEnumerable<PublisherDetail>> GetPublishersAsync();
    Task<PublisherDetail> GetPublisherByIdAsync(int id);
    Task<PublisherDetail> GetGenreByNameAsync(string name);
    Task<PublisherDetail> PostGenreAsync(PublisherCreate publisherCreate);
    Task UpdatePublisherAsync(int id, PublisherDetail publisherDetail);
    Task DeletePublisherAsync(int id);
}