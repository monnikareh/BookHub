using BookHub.Models;
using DataAccessLayer;
using DataAccessLayer.Entities;
using BusinessLayer.Mapper;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        
        // GET: api/Order
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetOrdersAsync(DateTime? startDate, DateTime? endDate)
        {
            return Ok(await _orderService.GetOrdersAsync(startDate, endDate));
        }
        /*
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
            return ControllerHelpers.MapOrderToOrderDetail(order);
        }
        
        [HttpGet("GetByName/{name}")]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetOrdersByUserName(string name)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }

            var orders = _context.Orders
                .Include(o => o.User)
                .Include(o => o.Books)
                .ThenInclude(b => b.Authors)
                .Where(o => o.User.Name == name)
                .AsQueryable();

            
            var orderList = await orders.Select(o => ControllerHelpers.MapOrderToOrderDetail(o)).ToListAsync();
            if (!orderList.Any())
            {
                return NotFound("No orders found for the specified date range.");
            }

            return orderList;
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

            if (orderDetail.Books != null && orderDetail.Books.Count != 0)
            {
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
        } */

    } 
}
