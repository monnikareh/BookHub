using System.Diagnostics;
using BookHub.Models;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookHub.Controllers;

public class GenreController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IGenreService _genreService;
    
    public GenreController(ILogger<HomeController> logger, IGenreService genreService)
    {
        _logger = logger;
        _genreService = genreService;
    }

    public async Task<IActionResult> Index()
    {
        var genres = await _genreService.GetGenresAsync(null);
        return View(genres);    
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