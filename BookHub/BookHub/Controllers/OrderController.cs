using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookHub.Controllers;

[Route("[controller]/[action]")]
public class OrderController : BaseController
{
    private readonly ILogger<OrderController> _logger;
    private readonly IOrderService _orderService;

    public OrderController(ILogger<OrderController> logger, IOrderService orderService)
    {
        _logger = logger;
        _orderService = orderService;
    }
    
    [HttpGet("{userId:int}/{bookId:int}")]
    [Authorize]
    public async Task<IActionResult> Append(int userId, int bookId)
    {
        await _orderService.AppendBook(userId, bookId);
        return RedirectToAction("Detail", "Book", new { id = bookId});
    }
    
    [HttpGet("{id:int}/")]
    [Authorize]
    public async Task<IActionResult> Pay(int id)
    {
        await _orderService.PayOrderAsync(id);
        return RedirectToPage("/Account/Manage/Order", new { area = "Identity" });    }

    
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Detail(int id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);
        return order.Match(
            View,
            Error);
    }
    
    
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Basket(int id)
    {
  
            var order = await _orderService.GetUnpaidOrder(id);
            return order.Match(
                View,
                _ => View("Empty")
            );
    }
}