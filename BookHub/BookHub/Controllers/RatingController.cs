using System.Diagnostics;
using System.Security.Claims;
using BookHub.Models;
using BusinessLayer.Models;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookHub.Controllers;

[Route("[controller]/[action]")]
public class RatingController : Controller
{
    private readonly ILogger<RatingController> _logger;
    private readonly IRatingService _ratingService;
    private readonly IUserService _userService;
    private readonly IBookService _bookService;

    public RatingController(ILogger<RatingController> logger, IRatingService ratingService, IUserService userService, IBookService bookService)
    {
        _logger = logger;
        _ratingService = ratingService;
        _userService = userService;
        _bookService = bookService;
    }

    public async Task<IActionResult> Index()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        int.TryParse(userIdClaim, out int userId);
        var ratings = await _ratingService.GetRatingsAsync(userId, null, null, null);
        return View(ratings);    
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
    [Authorize(Roles = "Admin")]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Edit(int id)
    {
        var rating = await _ratingService.GetRatingByIdAsync(id);
        
        return View(new RatingDetail
        {
            Id = rating.Id,
            User = rating.User,
            Book = rating.Book,
            Value = rating.Value,
            Comment = rating.Comment
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("{id:int}")]
    public async Task<IActionResult> Edit(int id, RatingDetail model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        await _ratingService.UpdateRatingAsync(id, model);
        return RedirectToAction("Index");
    }
    
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Delete(int id)
    {
        await _ratingService.DeleteRatingAsync(id);
        return RedirectToAction("Index");
    }
    
    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Detail(int id)
    {
        var rating = await _ratingService.GetRatingByIdAsync(id);
        return View(rating);
    }
    
}