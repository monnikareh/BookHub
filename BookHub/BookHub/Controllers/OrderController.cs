using System.Diagnostics;
using BookHub.Models;
using BusinessLayer.Exceptions;
using BusinessLayer.Services;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookHub.Controllers;

[Route("[controller]/[action]")]
public class OrderController : Controller
{
    private readonly ILogger<OrderController> _logger;
    private readonly IOrderService _orderService;

    public OrderController(ILogger<OrderController> logger, IOrderService orderService)
    {
        _logger = logger;
        _orderService = orderService;
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
        return View(order);
    }
    
    
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Unpaid(int id)
    {
        try
        {
            var order = await _orderService.GetUnpaidOrder(id);
            return View(order);
        }
        catch (OrderNotFoundException e)
        {
            return View("Empty");
        }
    }
}