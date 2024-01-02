using System.Diagnostics;
using BookHub.Models;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookHub.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IPublisherService _publisherService;

    public HomeController(ILogger<HomeController> logger, IPublisherService publisherService)
    {
        _logger = logger;
        _publisherService = publisherService;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    public async Task<IActionResult> Publisher()
    {
        var publishers = await _publisherService.GetPublishersAsync(null);
        return PartialView("Publisher", publishers);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}