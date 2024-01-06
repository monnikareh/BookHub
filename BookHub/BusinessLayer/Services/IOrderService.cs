using BusinessLayer.Errors;
using BusinessLayer.Models;
using DataAccessLayer.Entities;

namespace BusinessLayer.Services;

public interface IOrderService
{
    Task<IEnumerable<OrderDetail>> GetOrdersAsync(int? userId, string? username,
        DateTime? startDate, DateTime? endDate, decimal? totalPrice, int? bookId, string? bookName,
        PaymentStatus? paymentStatus);

    Task<Result<OrderDetail, string>> GetOrderByIdAsync(int id);
    Task<Result<OrderDetail, string>> CreateOrderAsync(OrderCreate orderCreate);
    Task<Result<OrderDetail, string>> UpdateOrderAsync(int id, OrderUpdate orderUpdate);
    Task<Result<bool, string>> DeleteOrderAsync(int id);
    Task<Result<bool, string>> AppendBook(int userId, int bookId);
    Task<Result<OrderDetail, string>> GetUnpaidOrder(int userId);

    Task<Result<bool, string>> PayOrderAsync(int id);
}