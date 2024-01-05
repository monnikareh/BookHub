using System.Diagnostics;
using BookHub.Models;
using BusinessLayer.Models;
using BusinessLayer.Services;
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

    // public async Task<IActionResult> Index()
    // {
    //     var orders = await _orderService.GetOrdersAsync(null);
    //     return View(orders);    
    // }
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
    // [Authorize(Roles = "Admin")]
    // public IActionResult Create()
    // {
    //     return View();
    // }
    //
    // [HttpPost]
    // [ValidateAntiForgeryToken]
    // [Authorize(Roles = "Admin")]
    // public async Task<IActionResult> Create(OrderCreate model)
    // {
    //     if (!ModelState.IsValid) return View(model);
    //     await _orderService.CreateOrderAsync(model);
    //     return RedirectToAction("Index");
    // }
    //
    //
    // [Authorize(Roles = "Admin")]
    // [HttpGet("{id:int}")]
    // public async Task<IActionResult> Edit(int id)
    // {
    //     var order = await _orderService.GetOrderByIdAsync(id);
    //     return View(new OrderCreate()
    //     {
    //         Name = order.Name,
    //     });
    // }
    //
    // [Authorize(Roles = "Admin")]
    // [HttpPost("{id:int}")]
    // public async Task<IActionResult> Edit(int id, OrderCreate model)
    // {
    //     if (!ModelState.IsValid)
    //     {
    //         return View(model);
    //     }
    //     await _orderService.UpdateOrderAsync(id, model);
    //     return RedirectToAction("Index");
    // }
    //
    // [Authorize(Roles = "Admin")]
    // public async Task<ActionResult> Delete(int id)
    // {
    //     await _orderService.DeleteOrderAsync(id);
    //     return RedirectToAction("Index");
    // }
    
    [Authorize(Roles = "Admin,User")]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Detail(int id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);
        return View(order);
    }
}