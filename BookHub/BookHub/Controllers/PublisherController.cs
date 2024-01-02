using System.Diagnostics;
using BookHub.Models;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookHub.Controllers;

public class PublisherController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IPublisherService _publisherService;
    
    public PublisherController(ILogger<HomeController> logger, IPublisherService publisherService)
    {
        _logger = logger;
        _publisherService = publisherService;
    }

    public async Task<IActionResult> Index()
    {
        var publishers = await _publisherService.GetPublishersAsync(null);
        return View(publishers);    
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