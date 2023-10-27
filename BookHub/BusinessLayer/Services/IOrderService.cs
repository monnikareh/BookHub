using BookHub.Models;

namespace BusinessLayer.Services;

public interface IOrderService
{
    public Task<IEnumerable<OrderDetail>> GetOrdersAsync(
        DateTime? startDate, DateTime? endDate
        );
}