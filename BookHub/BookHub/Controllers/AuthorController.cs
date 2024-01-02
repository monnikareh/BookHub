using System.Diagnostics;
using BookHub.Models;
using BusinessLayer.Models;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookHub.Controllers;

public class AuthorController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IAuthorService _authorService;
    
    public AuthorController(ILogger<HomeController> logger, IAuthorService authorService)
    {
        _logger = logger;
        _authorService = authorService;
    }

    public async Task<IActionResult> Index()
    {
        var authors = await _authorService.GetAuthorsAsync(null, null, null);
        return View(authors);    
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