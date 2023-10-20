using Microsoft.AspNetCore.Mvc;
using DataAccessLayer;
using BookHub.Models; 
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Entities;

//TODO
namespace BookHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly BookHubDbContext _context;

        public OrderController(BookHubDbContext context)
        {
            _context = context;
        }
        
        // GET: api/Order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetOrders(DateTime? startDate, DateTime? endDate)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }

            var orders = _context.Orders
                .Include(o => o.User)
                .Include(o => o.Books)
                .ThenInclude(b => b.Authors)
                .AsQueryable();

            if (startDate.HasValue)
            {
                orders = orders.Where(o => o.Date >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                orders = orders.Where(o => o.Date <= endDate.Value);
            }

            var orderList = await orders.Select(o => new OrderDetail
            {
                Id = o.Id,
                User = new ModelRelated { Id = o.User.Id, Name = o.User.Name },
                TotalPrice = o.TotalPrice,
                Date = o.Date,
                Books = o.Books.Select(b => new ModelRelated
                {
                    Id = b.Id,
                    Name = b.Name,
                  //  Price = b.Price,
                  //  Authors = b.Authors.Select(a => new ModelRelated { Name = a.Name }).ToList()
                }).ToList()
            }).ToListAsync();
            if (!orderList.Any())
            {
                return NotFound("No orders found for the specified date range.");
            }

            return orderList;
        }

        // GET: api/Order/5
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<OrderDetail>> GetOrderById(int id)
        {
            var order = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.Books)
                .ThenInclude(b => b.Authors)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound("Order not found.");
            }

            var orderDetail = new OrderDetail
            {
                Id = order.Id,
                User = new ModelRelated { Id = order.User.Id, Name = order.User.Name },
                TotalPrice = order.TotalPrice,
                Date = order.Date,
                Books = order.Books.Select(b => new ModelRelated
                {
                    Id = b.Id,
                    Name = b.Name,
                }).ToList()
            };

            return orderDetail;
        }
        
        [HttpGet("GetByName/{username}")]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetOrdersByUserName(string username)
        {
            var orders = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.Books)
                .ThenInclude(b => b.Authors)
                .Where(o => o.User.Name == username)
                .ToListAsync();

            if (orders == null || !orders.Any())
            {
                return NotFound("No orders found for the specified username.");
            }

            var orderDetails = orders.Select(o => new OrderDetail
            {
                Id = o.Id,
                User = new ModelRelated { Id = o.User.Id, Name = o.User.Name },
                TotalPrice = o.TotalPrice,
                Date = o.Date,
                Books = o.Books.Select(b => new ModelRelated
                {
                    Id = b.Id,
                    Name = b.Name,
                }).ToList()
            }).ToList();

            return orderDetails;
        }
        
        [HttpPost("CreateOrder")]
        public async Task<ActionResult<OrderDetail>> PostOrder(OrderCreate orderCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid!");
            }

            if (_context.Orders == null)
            {
                return Problem("Entity set 'BookHubDbContext.Orders' is null.");
            }

            if (orderCreate.User == null)
            {
                return Problem("User is null");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u =>
                u.Name == orderCreate.User.Name || u.Id == orderCreate.User.Id);
            if (user == null)
            {
                return NotFound(
                    $"User 'Name={orderCreate.User.Name}' <OR> 'ID={orderCreate.User.Id}' could not be found");
            }

            var order = new Order
            {
                User = user,
                TotalPrice = orderCreate.TotalPrice
            };

            if (orderCreate.Books == null || orderCreate.Books.Count == 0)
            {
                return BadRequest("No books specified in the order");
            }

            foreach (var bookRelatedModel in orderCreate.Books)
            {
                var book = await _context.Books.FirstOrDefaultAsync(b =>
                    b.Name == bookRelatedModel.Name || b.Id == bookRelatedModel.Id);
                if (book == null)
                {
                    return NotFound(
                        $"Book 'Name={bookRelatedModel.Name}' <OR> 'ID={bookRelatedModel.Id}' could not be found");
                }

                order.Books.Add(book);
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return ControllerHelpers.MapOrderToOrderDetail(order);
        }
        
        [HttpPut("UpdateOrder/{id}")]
        public async Task<ActionResult> UpdateOrder(int id, OrderDetail orderDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid!");
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound($"Order with ID {id} not found");
            }

            order.TotalPrice = orderDetail.TotalPrice;

            order.Books.Clear();
            foreach (var bookRelatedModel in orderDetail.Books)
            {
                var book = await _context.Books.FirstOrDefaultAsync(b =>
                    b.Name == bookRelatedModel.Name || b.Id == bookRelatedModel.Id);
                if (book == null)
                {
                    return NotFound(
                        $"Book 'Name={bookRelatedModel.Name}' <OR> 'ID={bookRelatedModel.Id}' could not be found");
                }

                order.Books.Add(book);
            }

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem($"Error updating order: {ex.Message}");
            }
        }
        
        [HttpDelete("DeleteOrder/{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound($"Order with ID {id} not found");
            }

            try
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem($"Error deleting order: {ex.Message}");
            }
        }

    } 
}
