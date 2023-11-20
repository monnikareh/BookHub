using BusinessLayer.Exceptions;
using BusinessLayer.Models;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        
        [HttpGet("GetOrders")]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetOrders(int? userId, string? username,
            DateTime? startDate, DateTime? endDate, decimal? totalPrice, int? bookId, string? bookName)
        {
            try
            {
                return Ok(await _orderService.GetOrdersAsync(userId, username, startDate, endDate, totalPrice, bookId, bookName));
            }
            catch (Exception e)
            {
                return HandleOrderException(e);
            }
        }
        
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<OrderDetail>> GetOrderById(int id)
        {
            try
            {
                return Ok(await _orderService.GetOrderByIdAsync(id));
            }
            catch (Exception e)
            {
                return HandleOrderException(e);
            }
        }
        
        [HttpPost("CreateOrder")]
        public async Task<ActionResult<OrderDetail>> CreateOrder(OrderCreate orderCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid!");
            }
            try
            {
                return Ok(await _orderService.CreateOrderAsync(orderCreate));
            }
            catch (Exception e)
            {
                return HandleOrderException(e);
            }
        }
        
        [HttpPut("UpdateOrder/{id}")]
        public async Task<ActionResult<OrderUpdate>> UpdateOrder(int id, OrderUpdate orderUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid!");
            }
            try
            {
                return Ok(await _orderService.UpdateOrderAsync(id, orderUpdate));
            }
            catch (Exception e)
            {
                return HandleOrderException(e);
            }
        }
        
        [HttpDelete("DeleteOrder/{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            try
            {
                await _orderService.DeleteOrderAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                return HandleOrderException(e);
            }
        } 
        
        private ActionResult HandleOrderException(Exception e)
        {
            return e is OrderNotFoundException or UserNotFoundException
                or BookNotFoundException or BooksEmptyException
                ? NotFound(e.Message)
                : Problem("Unknown problem occured");
        }
    } 
}
