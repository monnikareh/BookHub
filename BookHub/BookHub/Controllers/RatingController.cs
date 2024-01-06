using System.Diagnostics;
using System.Security.Claims;
using BookHub.Models;
using BusinessLayer.Models;
using BusinessLayer.Services;
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
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var rating = await _ratingService.GetRatingByIdAsync(id);
            return View(new RatingUpdate
            {
                Value = rating.Value,
                Comment = rating.Comment
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieving rating with ID {id}: {ex.Message}");
            return RedirectToAction("Error");
        }
    }

    [HttpPost("{id:int}")]
    public async Task<IActionResult> Edit(int id, RatingUpdate model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        try
        {
            await _ratingService.UpdateRatingAsync(id, model);
            _logger.LogInformation($"Rating with ID {id} updated successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error updating rating with ID {id}: {ex.Message}");
            return RedirectToAction("Error");
        }

        return RedirectToAction("Index");
    }

    
    public async Task<ActionResult> Delete(int id)
    {
        await _ratingService.DeleteRatingAsync(id);
        return RedirectToAction("Index");
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Detail(int id)
    {
        var rating = await _ratingService.GetRatingByIdAsync(id);
        return View(rating);
    }
    
}