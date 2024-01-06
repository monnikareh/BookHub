using System.Diagnostics;
using BookHub.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookHub.Controllers;

[Route("[controller]/[action]")]
public class SharedController : Controller
{
    // GET
    private readonly ILogger<SharedController> _logger;

    public SharedController(ILogger<SharedController> logger)
    {
        _logger = logger;
    }

    public IActionResult Privacy()
    {
        return View();
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(string message)
    {
        return View(new ErrorViewModel { Message = message, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}