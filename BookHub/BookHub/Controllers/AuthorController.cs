using System.Diagnostics;
using BookHub.Models;
using BusinessLayer.Mapper;
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
    
    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(AuthorCreate model)
    {
        if (!ModelState.IsValid) return View(model);
        await _authorService.CreateAuthorAsync(model);
        return RedirectToAction("Index");
    }
    
    [HttpGet("edit/{id:int}")]
    public async Task<IActionResult> Edit(int id)
    {
        var author = await _authorService.GetAuthorByIdAsync(id);
        return View(new AuthorUpdate
        {
            Name = author.Name,
            Books = author.Books
        });
    }

    
    [HttpPost("edit/{id:int}")]
    public async Task<IActionResult> Edit(int id, AuthorUpdate model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        await _authorService.UpdateAuthorAsync(id, model);
        return RedirectToAction("Index");
    }
    
    public async Task<ActionResult> Delete(int id)
    {
        await _authorService.DeleteAuthorAsync(id);
        return RedirectToAction("Index");
    }
}