using System.Diagnostics;
using BookHub.Models;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookHub.Controllers;

public class BookController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IBookService _bookService;
    
    public BookController(ILogger<HomeController> logger, IBookService bookService)
    {
        _logger = logger;
        _bookService = bookService;
    }

    public async Task<IActionResult> Index()
    {
        var books = await _bookService.GetBooksAsync(null, null, null, null, null, null, null);
        return View(books);    
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