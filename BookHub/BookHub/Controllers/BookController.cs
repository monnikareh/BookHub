using System.Diagnostics;
using BookHub.Models;
using BusinessLayer.Models;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookHub.Controllers;

[Route("[controller]/[action]")]
public class BookController : Controller
{
    private readonly ILogger<BookController> _logger;
    private readonly IBookService _bookService;
    
    public BookController(ILogger<BookController> logger, IBookService bookService)
    {
        _logger = logger;
        _bookService = bookService;
    }

    public async Task<IActionResult> Index()
    {
        var books = await _bookService.GetBooksAsync(null, null, null, null, null, null, null);
        return View(books);    
    }
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(BookCreate model)
    {
        if (!ModelState.IsValid) return View(model);
        await _bookService.CreateBookAsync(model);
        return RedirectToAction("Index");
    }

    
    [Authorize(Roles = "Admin")]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Edit(int id)
    {
        var book = await _bookService.GetBookByIdAsync(id);
        return View(new BookCreate()
        {
            Name = book.Name,
            PrimaryGenre = book.PrimaryGenre,
            Genres = book.Genres,
            Publisher = book.Publisher,
            StockInStorage = book.StockInStorage,
            OverallRating = 0,
            Price = book.Price,
            Authors = book.Authors
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("{id:int}")]
    public async Task<IActionResult> Edit(int id, BookCreate model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        await _bookService.UpdateBookAsync(id, model);
        return RedirectToAction("Index");
    }
    
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Delete(int id)
    {
        await _bookService.DeleteBookAsync(id);
        return RedirectToAction("Index");
    }
    
    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Detail(int id)
    {
        var book = await _bookService.GetBookByIdAsync(id);
        return View(book);
    }
}