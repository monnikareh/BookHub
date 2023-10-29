using BookHub.Models;
using BusinessLayer.Models;

namespace BusinessLayer.Services;

public interface IOrderService
{
    Task<IEnumerable<OrderDetail>> GetOrdersAsync(int? userId, string? username,
        DateTime? startDate, DateTime? endDate, double? totalPrice, int? bookId, string? bookName);
    Task<OrderDetail> GetOrderByIdAsync(int id);
    Task<OrderDetail> PostOrderAsync(OrderCreate orderCreate);
    Task<OrderDetail> UpdateOrderAsync(int id, OrderUpdate orderUpdate);
    Task DeleteOrderAsync(int id);
}