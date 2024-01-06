using System.Diagnostics;
using BookHub.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookHub.Controllers;


public class HomeController : BaseController
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();        
    }
    
}