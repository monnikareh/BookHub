using BookHub.Models;
using DataAccessLayer;
using BusinessLayer.Mapper;
using BusinessLayer.Exceptions;
using BusinessLayer.Models;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Packaging;


namespace BusinessLayer.Services
{
    public class OrderService : IOrderService
    {
        private readonly BookHubDbContext _context;

        public OrderService(BookHubDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderDetail>> GetOrdersAsync(int? userId, string? username,
            DateTime? startDate, DateTime? endDate, decimal? totalPrice, int? bookId, string? bookName)
        {
            var orders = _context.Orders
                .Include(o => o.User)
                .Include(o => o.Books)
                .ThenInclude(b => b.Authors)
                .AsQueryable();
            
            if (userId.HasValue)
            {
                orders = orders.Where(o => o.User.Id == userId.Value);
            }
            
            if (!string.IsNullOrEmpty(username))
            {
                orders = orders.Where(o => o.User.Name == username);
            }

            if (startDate.HasValue)
            {
                orders = orders.Where(o => o.Date >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                orders = orders.Where(o => o.Date <= endDate.Value);
            }

            if (totalPrice.HasValue)
            {
                orders = orders.Where(o => o.TotalPrice == totalPrice.Value);
            }

            var book = await _context.Books.FirstOrDefaultAsync(b => b.Name == bookName || b.Id == bookId);
            if (book != null)
            {
                orders = orders.Where(o => o.Books.Contains(book));
            }
            return await orders.Select(o => EntityMapper.MapOrderToOrderDetail(o)).ToListAsync();
        }
        
        public async Task<OrderDetail> GetOrderByIdAsync(int id)
        {
            var order = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.Books)
                .ThenInclude(b => b.Authors)
                .FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                throw new OrderNotFoundException($"Order 'ID={id}' could not be found");
            }
            return EntityMapper.MapOrderToOrderDetail(order);
        }

        public async Task<OrderDetail> CreateOrderAsync(OrderCreate orderCreate)
        {
            if (orderCreate.Books.IsNullOrEmpty())
            {
                throw new BooksEmptyException("Collection Books is empty");
            }
            var user = await _context.Users.FirstOrDefaultAsync(u =>
                u.Name == orderCreate.User.Name || u.Id == orderCreate.User.Id);
            if (user == null)
            {
                throw new UserNotFoundException($"User 'Name={orderCreate.User.Name}' <OR> 'ID={orderCreate.User.Id}' could not be found");
            }
            var order = new Order
            {
                User = user,
                TotalPrice = orderCreate.TotalPrice
            };
            
            foreach (var bookRelatedModel in orderCreate.Books)
            {
                var book = await _context.Books.FirstOrDefaultAsync(b =>
                    b.Name == bookRelatedModel.Name || b.Id == bookRelatedModel.Id);
                if (book == null)
                {
                    throw new BookNotFoundException($"Book 'Name={bookRelatedModel.Name}' <OR> 'ID={bookRelatedModel.Id}' could not be found");
                }
                order.Books.Add(book);
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return EntityMapper.MapOrderToOrderDetail(order);
        }

        public async Task<OrderDetail> UpdateOrderAsync(int id, OrderUpdate orderUpdate)
        {
            var order = await _context.Orders
                .Include(o => o.Books)
                .FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                throw new OrderNotFoundException($"Order 'ID={id}' could not be found");
            }
            
            order.TotalPrice = orderUpdate.TotalPrice;

            if (orderUpdate.Books.Count != 0)
            {
                var bookNames = orderUpdate.Books.Select(b => b.Name).ToHashSet();
                var bookIds = orderUpdate.Books.Select(b => b.Id).ToHashSet();

                var books = await _context.Books
                    .Where(b => bookNames.Contains(b.Name) || bookIds.Contains(b.Id))
                    .ToListAsync();

                if (books.Count != orderUpdate.Books.Count)
                {
                    throw new BookNotFoundException("One or more books could not be found");
                }

                order.Books.Clear();
                order.Books.AddRange(books);
            }
 
            await _context.SaveChangesAsync();
            return EntityMapper.MapOrderToOrderDetail(order);
         
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Books)
                .FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                throw new BookNotFoundException($"Order 'ID={id}' could not be found");
            }
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
    } 
}
