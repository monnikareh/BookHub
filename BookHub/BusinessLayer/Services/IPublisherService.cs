using BusinessLayer.Errors;
using BusinessLayer.Models;

namespace BusinessLayer.Services;

public interface IPublisherService
{ 
    Task<IEnumerable<PublisherDetail>> GetPublishersAsync(string? name);
    Task<Result<PublisherDetail, string>> GetPublisherByIdAsync(int id);
    Task<PublisherDetail> CreatePublisherAsync(PublisherCreate publisherCreate);
    Task<Result<PublisherDetail, string>> UpdatePublisherAsync(int id, PublisherUpdate publisherUpdate);
    Task<Result<bool, string>> DeletePublisherAsync(int id);
}