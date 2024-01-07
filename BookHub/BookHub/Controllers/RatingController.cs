using System.Security.Claims;
using BusinessLayer.Errors;
using BusinessLayer.Models;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Int32;

namespace BookHub.Controllers;

[Route("[controller]/[action]")]
public class RatingController : BaseController
{
    private readonly ILogger<RatingController> _logger;
    private readonly IRatingService _ratingService;
    private readonly IUserService _userService;
    private readonly IBookService _bookService;

    public RatingController(ILogger<RatingController> logger, IRatingService ratingService, IUserService userService,
        IBookService bookService)
    {
        _logger = logger;
        _ratingService = ratingService;
        _userService = userService;
        _bookService = bookService;
    }

    public async Task<IActionResult> Index()
    {
        var ret = TryParseId(out var userId);
        if (!ret)
        {
            return ErrorView((Error.UserNotFound, "User not found"));
        }
        var ratings = await _ratingService.GetRatingsAsync(userId, null, null, null);
        return View(ratings);
    }
    
    [Authorize]
    public async Task<IActionResult> Search(string query)
    {
        var ratings = await _ratingService.GetSearchRatingsAsync(query);
        var ret = TryParseId(out var userId);
        return !ret ? ErrorView((Error.UserNotFound, "User not found")) : View(ratings.Where(r => r.User.Id == userId));
    }


    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Edit(int id)
    {
        var rating = await _ratingService.GetRatingByIdAsync(id);
        return rating.Match(
            r => View(new RatingUpdate
            {
                Value = r.Value,
                Comment = r.Comment
            }),
            e =>
            {
                _logger.LogError($"Error retrieving rating with ID {id}: {e.message}");
                return ErrorView(e);
            });
    }

    [Authorize]
    [HttpPost("{id:int}")]
    public async Task<IActionResult> Edit(int id, RatingUpdate model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var rating = await _ratingService.UpdateRatingAsync(id, model);
        return rating.Match(
            r =>
            {
                _logger.LogInformation($"Rating with ID {id} updated successfully.");
                return RedirectToAction("Detail", "Book", new {id = r.Book.Id});
            },
            e =>
            {
                _logger.LogError($"Error updating rating with ID {id}: {e.message}");
                return ErrorView(e);
            }
        );
    }

    [Authorize]
    public async Task<ActionResult> Delete(int id)
    {
        await _ratingService.DeleteRatingAsync(id);
        return RedirectToAction("Index");
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Detail(int id)
    {
        var rating = await _ratingService.GetRatingByIdAsync(id);
        return rating.Match(
            r =>
            {
                var ret = TryParseId(out var userId);
                if (ret && r.User.Id == userId) return View(r);
                return RedirectToAction("Index");
            },
            ErrorView);
    }
}