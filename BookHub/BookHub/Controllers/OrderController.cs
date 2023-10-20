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
        /*
        [HttpPost]
        public async Task<ActionResult<OrderDetail>> PostOrder(OrderCreate orderCreate)
        {
            var user = await _context.Users.FindAsync(orderCreate.User.Id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var bookIds = orderCreate.Books.Select(b => b.Id).ToList();
            var books = await _context.Books.Where(b => bookIds.Contains(b.Id)).ToListAsync();
            if (books.Count != orderCreate.Books.Count)
            {
                return BadRequest("One or more books not found.");
            }

            var order = new Order
            {
                User = user,
                TotalPrice = orderCreate.TotalPrice,
                Date = DateTime.Now
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            var orderDetail = new OrderDetail
            {
                Id = order.Id,
                User = new ModelRelated { Id = user.Id, Name = user.Name },
                TotalPrice = order.TotalPrice,
                Date = order.Date,
                Books = books.Select(b => new ModelRelated
                {
                    Id = b.Id,
                    Name = b.Name,
                }).ToList()
            };

            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, orderDetail);
        }
    /*
        // PUT: api/Order/5
        [HttpPut("{id}")]
        public IActionResult PutOrder(int id, OrderDetail orderDetail)
        {
            // Implement code to update an existing order by its ID based on the provided OrderDetail object.
            // You can use _context to update the order.
            // Ensure proper error handling.
            // Return appropriate responses (e.g., NoContent, BadRequest, NotFound).
        }

        // DELETE: api/Order/5
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            // Implement code to delete an existing order by its ID from the database.
            // You can use _context to remove the order.
            // Ensure proper error handling.
            // Return appropriate responses (e.g., NoContent, NotFound).
        }*/
    } 
}
