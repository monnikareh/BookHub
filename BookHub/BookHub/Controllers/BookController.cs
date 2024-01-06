using System.Diagnostics;
using System.Security.Claims;
using BookHub.Models;
using BusinessLayer.Facades;
using BusinessLayer.Mapper;
using BusinessLayer.Models;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace BookHub.Controllers;

[Route("[controller]/[action]")]
public class BookController : Controller
{
    private readonly ILogger<BookController> _logger;
    private readonly IBookService _bookService;
    private readonly IRatingService _ratingService;
    private readonly IUserService _userService;
    private readonly BookFacade _bookFacade;

    public BookController(ILogger<BookController> logger, IBookService bookService, IRatingService ratingService, IUserService userService, BookFacade bookFacade)
    {
        _logger = logger;
        _bookService = bookService;
        _ratingService = ratingService;
        _userService = userService;
        _bookFacade = bookFacade;
    }

    public async Task<IActionResult> Index()
    {
        var books = await _bookFacade.GetAllBooks();
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
        await _bookFacade.AddNewBook(model);
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
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> ShowRatings(int id)
    {
        var reviews = await _ratingService.GetRatingsAsync(null, null, id, null);
        return PartialView("_RatingsPartial", reviews);
    }
    
    [Authorize]
    [HttpPost("{id:int}")]
    public async Task<IActionResult> AddToWishlist(int id)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        int.TryParse(userIdClaim, out int userId);

        /*
        if (userId == null)
        {
            return RedirectToAction("Login", "Account");
        } */

        var user = await _userService.GetUserByIdAsync(userId);
        var result =  await _userService.AddBookToWishlist(user.Id, id);
        if (result)
        {
            TempData["WishlistMessage"] = "Book added to Wishlist";
        }
        else
        {
            TempData["WishlistMessage"] = "Book already in Wishlist";
        }
        return RedirectToAction("Detail", new { id = id });
    }
    
    [Authorize]
    [HttpPost("{id:int}")]
    public async Task<IActionResult> AddRating(int id, int value)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        int.TryParse(userIdClaim, out int userId);

        /*
        if (userId == null)
        {
            return RedirectToAction("Login", "Account");
        } */

        var user = await _userService.GetUserByIdAsync(userId);
        var book = await _bookService.GetBookByIdAsync(id);
        //if (await _ratingService.ExistRatingForUser(user.Id, book.Id))
        //{
       //     return _PartialView;
        //}
        var newRating = new RatingCreate
        {
            User = EntityMapper.MapUserDetailToRelated(user),
            Book = EntityMapper.MapBookDetailToRelated(book),
            Value = value,
            Comment = null,
        };
        var result =  await _ratingService.CreateRatingAsync(newRating);
        await UpdateRating(id, value);
        return RedirectToAction("Detail", new { id = id });
    }
    
    [HttpPost("{id:int}")]
    public async Task UpdateRating(int id, int value)
    {
        var book = await _bookService.GetBookByIdAsync(id);
        book.OverallRating += value;
        await _bookService.UpdateBookAsync(id, EntityMapper.MapBookDetailToCreate(book));
    }
}