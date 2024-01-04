using System.Diagnostics;
using BookHub.Models;
using BusinessLayer.Models;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookHub.Controllers;

[Route("[controller]/[action]")]
public class PublisherController : Controller
{
    private readonly ILogger<PublisherController> _logger;
    private readonly IPublisherService _publisherService;
    
    public PublisherController(ILogger<PublisherController> logger, IPublisherService publisherService)
    {
        _logger = logger;
        _publisherService = publisherService;
    }

    public async Task<IActionResult> Index()
    {
        var publishers = await _publisherService.GetPublishersAsync(null);
        return View(publishers);    
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
    public async Task<IActionResult> Create(PublisherCreate model)
    {
        if (!ModelState.IsValid) return View(model);
        await _publisherService.CreatePublisherAsync(model);
        return RedirectToAction("Index");
    }
    
    [Authorize(Roles = "Admin")]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Edit(int id)
    {
        var publisher = await _publisherService.GetPublisherByIdAsync(id);
        return View(new PublisherUpdate
        {
            Name = publisher.Name,
            Books = publisher.Books
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("{id:int}")]
    public async Task<IActionResult> Edit(int id, PublisherUpdate model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        await _publisherService.UpdatePublisherAsync(id, model);
        return RedirectToAction("Index");
    }
    
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Delete(int id)
    {
        await _publisherService.DeletePublisherAsync(id);
        return RedirectToAction("Index");
    }
    
    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Detail(int id)
    {
        var author = await _publisherService.GetPublisherByIdAsync(id);
        return View(author);
    }
    
}